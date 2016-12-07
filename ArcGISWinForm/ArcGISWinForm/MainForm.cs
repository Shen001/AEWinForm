using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Globalization;
using System.Threading;


namespace SeanShen.ArcGISWinForm
{

    /*************************************************
     * 作者：沈鑫
     * 版本号：V1.0
     * 创建日期：2016年11月29日  13:24:11  Tuesday
     * 说明：主窗体，该cs文件主要包括界面初始化内容
     * **************************************************/
    using SeanShen.AEUtilities;
    using SeanShen.CustomControls;
    using DevExpress.XtraBars;
    using ESRI.ArcGIS.Carto;
    using ESRI.ArcGIS.Controls;
    using SeanShen.Framework;
    using System.Xml;
    using System.Collections;
    using SeanShen.AOCustomControls;
    using ESRI.ArcGIS.SystemUI;
    using ESRI.ArcGIS.Geodatabase;
    using DevExpress.LookAndFeel;
    using DevExpress.XtraLayout;
    using DevExpress.XtraEditors;
    public partial class MainForm : Form, ISeanApplication, ISeanStatusBar, ISeanLayoutManager, ISeanCommandManager, ISeanContextMenuManager
    {

        public MainForm()
        {
            CultureInfo Ci = new CultureInfo("zh-CN");
            Thread.CurrentThread.CurrentCulture = Ci;
            Thread.CurrentThread.CurrentUICulture = Ci;

            InitializeComponent();
            /*登录窗体*/

            //初始化对象

            InitialMainForm();
            InitialResource();
            if (!RegisterResources())
            {
                //记录
                return;
            }
            AddMenuBar();
            RegisterResourceInBarItem();

            LoadDefaultLayout();

            CreateDockWindow();
            CreateView();
            AddStatusBarItem();

            UserLookAndFeel.Default.UseDefaultLookAndFeel = false;
            UserLookAndFeel.Default.SetSkinStyle("Office 2013");
            this.ResumeLayout(false);
            this.PerformLayout();

            this.Load += new EventHandler(MainForm_Load);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.mMapControl.LoadMxFile(@"F:\空间数据\MXD\test_double.mxd");
                this.mMapControl.Map.Name = "图层";//加载完修改map的名称
                this.mMapControl.ActiveView.ContentsChanged();//刷新TOC

                this.tocWindow.GetControl().Refresh();
                this.mapControlView.GetControl().Refresh();
                //Application.DoEvents();

                this.mDefaultCommand = ((ISeanCommandManager)this).GetCommand("D55F27D6-B49C-41B6-9DE3-6E060BFC8B76");
                this.mDefaultCommand.Run();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //窗体关闭前
        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.barManager1.AllowCustomization)
            {
                //SaveAsDefaultLayout();
            }
        }

        # region 初始化控件

        private DevExpress.XtraBars.BarSubItem newMenu;//该item代表添加到了barmanager里面但是没有添加到界面上的item

        #endregion

        //初始化主窗体属性
        private void InitialMainForm()
        {
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 480);
            this.ShowInTaskbar = true;
            this.Text = "SeanMap";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);

            this.barManager1.Images = this.imageCollection1;
        }
        /// <summary>
        /// 初始化资源池（command）
        /// </summary>
        private void InitialResource()
        {
            string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "UIConfig.xml";
            XmlNode xmlConfig;
            XmlDocument UiConfig = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            XmlReader reader = XmlReader.Create(xmlPath, settings);
            UiConfig.Load(reader);

            if ((xmlConfig = UiConfig.SelectSingleNode("ROOT")) == null)
                return;//记录日志~

            //加载系统资源
            XmlNode poolNode = xmlConfig.SelectSingleNode("RESOURCEPOOL");
            if (poolNode != null)
            {
                string errorMsg = "";
                if (!LoadResourceFromXML(poolNode, out errorMsg))
                {
                    //记录日志
                    return;
                }
            }
        }
        //将配置资源读取成对象并添加到集合中
        private bool LoadResourceFromXML(XmlNode poolNode, out string errorMsg)
        {
            if (!poolNode.HasChildNodes)
            {
                errorMsg = string.Format("节点{0}配置文件没有子项，在LoadResourceDefinitions 行处", poolNode.Name);
                return false;
            }
            string caption = "", clsid = "", cls = "";
            XmlAttribute attr = null;
            ResourceConfigStructure resourceDefinition = null;
            foreach (XmlNode node in poolNode.ChildNodes)
            {
                attr = node.Attributes["caption"];
                if (attr != null) caption = attr.Value;
                if (string.IsNullOrEmpty(caption))
                {

                    errorMsg = string.Format("节点name不存在，在LoadResourceDefinitions 行处", poolNode.Name);
                    return false;
                }
                attr = node.Attributes["clsid"];
                if (attr != null) clsid = attr.Value.ToLower();
                if (string.IsNullOrEmpty(clsid))
                {
                    errorMsg = string.Format("节点clsid不存在，在LoadResourceDefinitions 行处", node.Name);
                    return false;
                }

                attr = node.Attributes["class"];
                if (attr != null) cls = attr.Value;
                if (string.IsNullOrEmpty(cls))
                {
                    errorMsg = string.Format("节点class不存在，在LoadResourceDefinitions 行处", node.Name);
                    return false;
                }

                resourceDefinition = new ResourceConfigStructure(caption, clsid, cls);
                this.resourceConfigStructure.Add(resourceDefinition);
            }
            errorMsg = "";
            return true;
        }

        #region 资源集合与变量
        //配置文件资源对象集合
        private List<ResourceConfigStructure> resourceConfigStructure = new List<ResourceConfigStructure>();
        //实例化command集合
        private Hashtable resourceHashtable = new Hashtable();

        private List<ISeanCommand> commandList = new List<ISeanCommand>();
        private List<ISeanDockWindow> dockWindowList = new List<ISeanDockWindow>();
        private List<ISeanView> viewList = new List<ISeanView>();

        private ISeanMapControlView mapControlView;
        private ISeanPagelayoutView pagelayoutView;
        private ISeanTocWindow tocWindow;

        BarStaticItem staticItem_Message = null;
        BarStaticItem staticItem_Coordinate = null;
        #endregion

        /// <summary>
        /// 将集合中的资源进行资源获取
        /// </summary>
        /// <returns></returns>
        private bool RegisterResources()
        {
            if (this.resourceConfigStructure.Count == 0)
                return false;

            ISeanResource resource = null;
            foreach (var definition in this.resourceConfigStructure)
            {
                try
                {
                    resource = definition.Create();//反射
                    string uid = resource.UID.ToString();

                    if (resource.Type == enumResourceType.Command)
                    {
                        ISeanCommand command = (ISeanCommand)resource;
                        this.commandList.Add(command);
                    }
                    else if (resource.Type == enumResourceType.TocWindow)
                    {
                        this.tocWindow = (ISeanTocWindow)resource;
                        this.dockWindowList.Add((ISeanDockWindow)resource);
                    }
                    else if (resource.Type == enumResourceType.MapView)
                    {
                        this.mapControlView = (ISeanMapControlView)resource;
                        this.mMapControl = this.mapControlView.GetIMapControl();
                        this.viewList.Add((ISeanView)resource);
                    }
                    else if (resource.Type == enumResourceType.PageLayoutView)
                    {
                        this.pagelayoutView = (ISeanPagelayoutView)resource;
                        this.viewList.Add((ISeanView)resource);
                    }
                    this.resourceHashtable.Add(uid, resource);
                }
                catch (Exception ex)
                {
                    string error = string.Format("注册资源{0}失败：{1}", definition.Caption, ex.Message);
                    //写入日志文件
                    return false;
                }
            }

            return true;
        }
        //为每一个命令注册按钮
        private bool RegisterResourceInBarItem()
        {
            if (this.resourceHashtable.Count == 0)
                return false;
            foreach (ISeanResource resource in this.resourceHashtable.Values)
            {
                resource.SetHook((ISeanApplication)this);

                DevExpress.XtraBars.BarItem barItem = null;
                int imageIndex = -1;
                if (resource is ISeanCommand)
                {
                    ISeanCommand command = resource as ISeanCommand;

                    RegisterCategory(command);

                    //this.imageList1.Images.Add(command.UID.ToString(), command.Bitmap);
                    //imageIndex = this.imageList1.Images.Count - 1;

                    this.imageCollection1.Images.Add(command.Bitmap, command.UID.ToString());
                    imageIndex = this.imageCollection1.Images.Count - 1;

                    if (command is SeanBaseTool)//如果是baritem
                    {
                        barItem = new BarCheckItem();
                    }
                    else if (command is SeanBaseCommand)
                    {
                        barItem = new BarButtonItem();
                    }

                    barItem.Name = command.Name;
                    barItem.Caption = command.Caption;
                    barItem.Id = command.UID.ToString().GetHashCode();
                    if (imageIndex != -1)
                        barItem.ImageIndex = imageIndex;
                    barItem.Category = this.barManager1.Categories[command.Category];
                    barItem.Hint = command.Caption;
                    barItem.Tag = command.UID.ToString().ToLower();
                    command.BindBarItem = barItem;

                    this.barManager1.Items.Add(barItem);
                    barItem.ItemClick += new ItemClickEventHandler(barItem_ItemClick);
                }
            }
            return true;
        }
        //绑定item点击事件
        void barItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            string uid = e.Item.Tag.ToString();
            ISeanCommand command = this.resourceHashtable[uid] as ISeanCommand;
            if (command == null)
                return;

            if (e.Item is BarCheckItem)
            {
                foreach (BarItem item in barManager1.Items)//设置所有不check
                {
                    if (item is BarCheckItem)
                    {
                        ((BarCheckItem)item).Checked = false;
                    }
                }
            }
            command.Run();
        }
        /// <summary>
        /// 将command分类
        /// </summary>
        /// <param name="command"></param>
        private void RegisterCategory(ISeanCommand command)
        {
            if (barManager1.Categories.IndexOf(command.Category) == -1)
            {
                this.barManager1.Categories.Add(command.Category);
            }
        }
        /// <summary>
        /// 创建停靠窗体
        /// </summary>
        private void CreateDockWindow()
        {
            if (this.dockWindowList.Count == 0)
                return;
            foreach (ISeanDockWindow dockWindow in this.dockWindowList)
            {
                DevExpress.XtraBars.Docking.DockPanel dockPanel = null;
                for (int i = 0; i < this.dockManager1.Panels.Count; i++)
                {
                    if (dockWindow.Caption == this.dockManager1.Panels[i].Text)
                        dockPanel = this.dockManager1.Panels[i];
                }
                if (dockPanel == null)
                {
                    dockPanel = this.dockManager1.AddPanel(DevExpress.XtraBars.Docking.DockingStyle.Left);
                    dockPanel.SuspendLayout();
                    dockPanel.Text = dockWindow.Caption;
                    dockPanel.ID = Guid.NewGuid();
                }
                ((Control)dockWindow).Dock = System.Windows.Forms.DockStyle.Fill;
                dockPanel.ControlContainer.Controls.Add(dockWindow.GetControl());
                dockPanel.ResumeLayout(false);
            }
        }
        /// <summary>
        /// 创建view视图,暂时是dockpanel~
        /// </summary>
        private void CreateView()
        {
            if (this.viewList.Count == 0)
                return;
            //创建mapview
            DevExpress.XtraLayout.LayoutControl layoutcontrol = new DevExpress.XtraLayout.LayoutControl();
            TabbedControlGroup controlGroup = layoutcontrol.Root.AddTabbedGroup();
            //layoutcontrol.BackColor = Color.White;

            ((System.ComponentModel.ISupportInitialize)(layoutcontrol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(controlGroup)).BeginInit();

            layoutcontrol.Dock = DockStyle.Fill;
            controlGroup.Name = "多视图布局";

            LayoutControlGroup mapview_ControlGroup = controlGroup.AddTabPage() as LayoutControlGroup;
            mapview_ControlGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.False;
            mapview_ControlGroup.Name = "mapview";
            mapview_ControlGroup.Text = "地图视图";
            LayoutControlItem mapItem = mapview_ControlGroup.AddItem();
            mapItem.Name = "mapview_Item";
            mapItem.Control = this.mapControlView.GetControl();
            mapItem.TextVisible = false;//控件默认为true

            LayoutControlGroup pagelayoutview_ControlGroup = controlGroup.AddTabPage() as LayoutControlGroup;
            pagelayoutview_ControlGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            pagelayoutview_ControlGroup.Name = "pagelayoutview";
            pagelayoutview_ControlGroup.Text = "布局视图";
            LayoutControlItem pagelayoutItem = pagelayoutview_ControlGroup.AddItem();
            pagelayoutItem.Name = "pagelayoutview_Item";
            pagelayoutItem.Control = this.pagelayoutView.GetControl();
            pagelayoutItem.TextVisible = false;

            controlGroup.SelectedTabPage = mapview_ControlGroup;

            this.Controls.Add(layoutcontrol);

            ((System.ComponentModel.ISupportInitialize)layoutcontrol).EndInit();
            ((System.ComponentModel.ISupportInitialize)controlGroup).EndInit();

            this.mMapControl = this.mapControlView.GetIMapControl();
            ((Control)this.mapControlView).Dock = System.Windows.Forms.DockStyle.Fill;

            this.tocWindow.GetITOCControl().SetBuddyControl(this.mapControlView.GetIMapControl());
        }

        /// <summary>
        /// 添加一个菜单栏bar
        /// </summary>
        private void AddMenuBar()
        {
            DevExpress.XtraBars.Bar menuBar = new Bar(this.barManager1, "菜单栏");
            menuBar.DockStyle = BarDockStyle.Top;
            menuBar.OptionsBar.UseWholeRow = true;
            menuBar.OptionsBar.AllowCollapse = false;
            menuBar.OptionsBar.AllowQuickCustomization = true;//下拉箭头

            this.barManager1.MainMenu = menuBar;
            this.barManager1.Bars.Add(menuBar);

            this.barManager1.Categories.Add(new BarManagerCategory("菜单", new System.Guid("727A147A-51FF-4EBE-BF4A-FC332280BD16")));
        }

        /// <summary>
        /// 状态栏添加项目
        /// </summary>
        private void AddStatusBarItem()
        {
            staticItem_Message = new BarStaticItem();
            staticItem_Message.Caption = "";

            staticItem_Coordinate = new BarStaticItem();
            staticItem_Coordinate.Alignment = BarItemLinkAlignment.Right;
            staticItem_Coordinate.Caption = "地理坐标";

            this.bar3.AddItems(new BarItem[] { staticItem_Message, staticItem_Coordinate });
        }

        int newMenuIndex = 0;
        /// <summary>
        /// 在菜单bar中添加了一个新的item，添加过后可以修改名称等，然后再添加一个新的subitem赋给新的item
        /// </summary>
        void menuBar_LinkAdded(object sender, LinkEventArgs e)
        {
            if (e.Link.Item == this.newMenu)//添加的是自定义的新按钮
            {
                this.newMenu.Id = Guid.NewGuid().GetHashCode();
                this.newMenu.CategoryGuid = Guid.Empty;
                this.newMenu.LinkAdded += new LinkEventHandler(menuBar_LinkAdded);//循环注册事件

                BarSubItem newMenuItem = new BarSubItem();
                newMenuItem.Id = Guid.NewGuid().GetHashCode();
                newMenuItem.CategoryGuid = new Guid("727A147A-51FF-4EBE-BF4A-FC332280BD16");
                newMenuItem.Caption = "新菜单" + ++newMenuIndex;
                this.newMenu = newMenuItem;

                this.barManager1.BeginUpdate();
                this.barManager1.Items.Add(newMenuItem);
                this.barManager1.ForceInitialize();
                this.barManager1.EndUpdate();
            }
        }
    }
}



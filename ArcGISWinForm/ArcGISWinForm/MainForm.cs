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
    public partial class MainForm : Form, ISeanApplication, ISeanStatusBar, ISeanLayoutManager, ISeanCommandManager,ISeanContextMenuManager
    {

        public MainForm()
        {
            CultureInfo Ci = new CultureInfo("zh-CN");
            Thread.CurrentThread.CurrentCulture = Ci;
            Thread.CurrentThread.CurrentUICulture = Ci;
            /*登录窗体*/

            //初始化对象
            InitializeComponent();
            InitialMainForm();
            //InitialDevControl();
            InitialResource();
            if (!RegisterResources())
            {
                //记录
                return;
            }
            RegisterResourceInBarItem();
            CreateDockWindow(); 
            CreateView();
            //LoadDefaultLayout();
            AddMenuBar();


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
                SaveLayout();//保存当前的布局
            }
        }

        # region 初始化控件

        private DevExpress.XtraBars.BarSubItem newMenu;//该item代表添加到了barmanager里面但是没有添加到界面上的item

        private DevExpress.XtraBars.BarDockControl dockTop;
        private DevExpress.XtraBars.BarDockControl dockBottom;
        private DevExpress.XtraBars.BarDockControl dockLeft;
        private DevExpress.XtraBars.BarDockControl dockRight;
        #endregion

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitialDevControl()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));

            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            //this.defaultLookAndFeel=new DevExpress.LookAndFeel.DefaultLookAndFeel (this.components);
            this.barManager1 = new BarManager(this.components);
            this.imageList1 = new ImageList(this.components);
            this.newMenu = new BarSubItem();

            this.dockTop = new BarDockControl();
            this.dockLeft = new BarDockControl();
            this.dockBottom = new BarDockControl();
            this.dockRight = new BarDockControl();

            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();

            this.SuspendLayout();

            this.dockManager1.Form = this;
            this.dockManager1.MenuManager = this.barManager1;
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"
            });

            this.barManager1.Categories.AddRange(new DevExpress.XtraBars.BarManagerCategory[]{
            new DevExpress.XtraBars.BarManagerCategory("菜单",new System.Guid("727A147A-51FF-4EBE-BF4A-FC332280BD16"))
            });
            this.barManager1.DockControls.Add(this.dockTop);
            this.barManager1.DockControls.Add(this.dockLeft);
            this.barManager1.DockControls.Add(this.dockBottom);
            this.barManager1.DockControls.Add(this.dockRight);
            this.barManager1.DockManager = this.dockManager1;
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageList1;
            this.barManager1.AllowCustomization = true;
            this.barManager1.MaxItemId = 704996564;
         

            //top
            this.dockTop.CausesValidation = false;
            this.dockTop.Dock = DockStyle.Top;
            this.dockTop.Location = new System.Drawing.Point(0, 0);
            this.dockTop.Size = new System.Drawing.Size(691, 0);

            //bottom
            this.dockBottom.CausesValidation = false;
            this.dockBottom.Dock = DockStyle.Bottom;
            this.dockBottom.Location = new System.Drawing.Point(0, 528);
            this.dockBottom.Size = new System.Drawing.Size(691, 0);

            //left
            this.dockLeft.CausesValidation = false;
            this.dockLeft.Dock = DockStyle.Left;
            this.dockLeft.Location = new System.Drawing.Point(0, 0);
            this.dockLeft.Size = new System.Drawing.Size(0, 528);

            //right
            this.dockRight.CausesValidation = false;
            this.dockRight.Dock = DockStyle.Right;
            this.dockRight.Location = new System.Drawing.Point(691, 0);
            this.dockRight.Size = new System.Drawing.Size(0, 528);
            //lookandfeel
            //this.defaultLookAndFeel.LookAndFeel.SkinName = "Office 2010 Black";
            //imagelist
            this.imageList1.ColorDepth = ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;

            //newmenu
            this.newMenu.Caption = "新菜单";
            this.newMenu.CategoryGuid = Guid.Parse("727A147A-51FF-4EBE-BF4A-FC332280BD16");//菜单目录
            this.newMenu.Id = -559393299;
            this.newMenu.Name = "newMenu";

            //MainForm
            this.Controls.Add(this.dockTop);
            this.Controls.Add(this.dockBottom);
            this.Controls.Add(this.dockLeft);
            this.Controls.Add(this.dockRight);

            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 480);
            this.ShowInTaskbar = true;
            this.Text = "SeanMap";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
        }
        //初始化主窗体属性
        private void InitialMainForm()
        {
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 480);
            this.ShowInTaskbar = true;
            this.Text = "SeanMap";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
        }
        //配置文件资源对象集合
        private List<ResourceConfigStructure> resourceConfigStructure = new List<ResourceConfigStructure>();
        //实例化command集合
        private Hashtable resourceHashtable = new Hashtable();
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
        private List<ISeanCommand> commandList = new List<ISeanCommand>();
        private List<ISeanDockWindow> dockWindowList = new List<ISeanDockWindow>();
        private List<ISeanView> viewList = new List<ISeanView>();

        private ISeanMapControlView mapControlView;
        private ISeanPagelayoutView pagelayoutView;
        private ISeanTocWindow tocWindow;
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

                    this.imageList1.Images.Add(command.UID.ToString(), command.Bitmap);
                    imageIndex = this.imageList1.Images.Count - 1;

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
            foreach (BarItem item in barManager1.Items)//设置所有不check
            {
                if (item is BarCheckItem)
                {
                    ((BarCheckItem)item).Checked = false;
                }
            }
            command.Run();
            if (e.Item is BarCheckItem)
            {
                BarCheckItem item = e.Item as BarCheckItem;
                item.Checked = command.Checked;
            }
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
            DevExpress.XtraBars.Docking.DockPanel dockPanel = null;
            for (int i = 0; i < this.dockManager1.Panels.Count; i++)
            {
                if (this.mapControlView.Caption == this.dockManager1.Panels[i].Text)
                    dockPanel = this.dockManager1.Panels[i];
            }
            if (dockPanel == null)
            {
                dockPanel = this.dockManager1.AddPanel(DevExpress.XtraBars.Docking.DockingStyle.Float);
                dockPanel.SuspendLayout();
                dockPanel.Text = this.mapControlView.Caption;
                dockPanel.ID = Guid.NewGuid();
            }
            this.mMapControl = this.mapControlView.GetIMapControl();
            ((Control)this.mapControlView).Dock = System.Windows.Forms.DockStyle.Fill;
            dockPanel.ControlContainer.Controls.Add(this.mapControlView.GetControl());
            dockPanel.ResumeLayout(false);


            this.tocWindow.GetITOCControl().SetBuddyControl(this.mapControlView.GetIMapControl());
        }

        /// <summary>
        /// 添加一个菜单栏bar
        /// </summary>
       private void AddMenuBar()
        {
            DevExpress.XtraBars.Bar menuBar = new Bar(this.barManager1, "菜单栏");
            menuBar.LinkAdded += new LinkEventHandler(menuBar_LinkAdded);
            menuBar.DockStyle = BarDockStyle.Top;
            menuBar.OptionsBar.UseWholeRow = true;
            menuBar.OptionsBar.AllowCollapse = false;
            menuBar.OptionsBar.AllowQuickCustomization=true;//下拉箭头

            this.barManager1.MainMenu = menuBar;
            this.barManager1.Bars.Add(menuBar);

            for (int i = 0; i < this.barManager1.Categories.Count; i++)
            {
                Bar subBar = new Bar(this.barManager1,this.barManager1.Categories[i].Name);
                subBar.LinkAdded += new LinkEventHandler(menuBar_LinkAdded);
                subBar.DockStyle = BarDockStyle.Top;
                subBar.OptionsBar.UseWholeRow = false;
                subBar.OptionsBar.AllowCollapse = true;
                subBar.OptionsBar.AllowQuickCustomization = true;
                subBar.OptionsBar.Reset();
                subBar.Reset();
                this.barManager1.Bars.Add(subBar);
            }
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

        //保存布局到配置文件
        private void SaveLayout()
        {
            if (System.IO.File.Exists(Default_LayoutPath))
            {
                System.IO.File.Delete(Default_LayoutPath);
            }

            this.barManager1.SaveLayoutToXml(Default_LayoutPath);

            //System.Xml.XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(Default_LayoutPath);
            //XmlNode nodeMenus = xmlDoc.CreateElement("Menus");
            //xmlDoc.FirstChild.AppendChild(nodeMenus);
            //for (int i = 0; i < this.barManager.Items.Count; i++)
            //{
            //    BarItem barItem = barManager.Items[i];
            //    if (barItem is BarSubItem)
            //    {
            //        if (barItem == this.newMenu)//如果相等，代表还没有添加到当前的界面视图中去，只是存在barmanager中
            //            continue;
            //        XmlNode nodeItem = xmlDoc.CreateElement("Item");
            //        XmlAttribute attrID = xmlDoc.CreateAttribute("ID");
            //        XmlAttribute attrName = xmlDoc.CreateAttribute("Name");
            //        XmlAttribute attrCaption = xmlDoc.CreateAttribute("Caption");

            //        attrID.Value = barItem.Id.ToString();
            //        attrName.Value = barItem.Name;
            //        attrCaption.Value = barItem.Caption;

            //        nodeItem.Attributes.Append(attrID);
            //        nodeItem.Attributes.Append(attrName);
            //        nodeItem.Attributes.Append(attrCaption);

            //        nodeMenus.AppendChild(nodeItem);
            //    }
            //}
            //xmlDoc.Save(Default_LayoutPath);
        }


        #region ISeanContextManager
        public void ShowContextMenu(string groupname)
        { }

        public void ShowContextMenu(string groupname, int x, int y)
        { }

        public void SetUserData(string groupname, object data)
        { }
        #endregion



        #region ISeanStatusBar

        private string m_StatusMessage = "准备";
        public string Message
        {
            get { return m_StatusMessage; }
            set
            {
                this.m_StatusMessage = value;
                //设置状态栏显示信息
            }
        }

        public void ShowCoordinate(double x, double y)
        {
            string coordinateMsg = string.Format("坐标：{0:0.###}，{1:0.###}{2}  比例：{3：0}", x, y, "地图单位", "地图比例");
        }

        public void ShowProgressBar(string Message, int min, int max, int Step)
        { }

        public void HideProgressBar()
        { }

        public void StepProgressBar()
        { }

        #endregion

        #region ISeanCommandManager

        public ISeanCommand GetCommand(Guid uid)
        {
            string strUid = uid.ToString();
            return this.GetCommand(strUid);
        }

        public ISeanCommand GetCommand(string uid)
        {
            uid = uid.ToLower();
            return this.commandList.Find(o => o.UID.ToString() == uid);
        }
        #endregion

        #region ISeanApplication

        private ISeanCommand mCurrentCommand;
        private IMapControlDefault mMapControl;
        private ISeanCommand mDefaultCommand;
        private IWorkspace mWorkspace;
        private ISelectionEnvironment mSelectionEnvironment;

        public string Caption
        {
            get { return this.Text; }
            set { this.Text = value; }
        }


        public int hWnd { get { return this.hWnd; } }

        public ISeanCommand CurrentCommand
        {
            get { return this.mCurrentCommand; }
            set
            {
                this.mCurrentCommand = value;
                ((ISeanStatusBar)this).Message = this.mCurrentCommand.Name;
                if (this.mCurrentCommand is ITool)
                {
                    this.mMapControl.CurrentTool = this.mCurrentCommand as ITool;
                }
                else
                {
                    if (this.mCurrentCommand.Name == "地图漫游" && ((ICommand)this.mMapControl.CurrentTool).Name != "ControlToolsMapNavigation_Pan")
                    {
                        ICommand panTool = new ControlsMapPanToolClass();
                        panTool.OnCreate(this.mMapControl);
                        this.mMapControl.CurrentTool = panTool as ITool;
                    }

                }
            }
        }

        public ISeanCommand DefaultCommand
        {
            get { return this.mDefaultCommand; }
        }

        public IWorkspace Workspace
        {
            get { return null; }
        }

        public IMap Map { get { return this.mMapControl.Map; } }

        public ISeanView CurrentView { get { return this.mapControlView; } }

        public ISelectionEnvironment SelectionEnvironment
        {
            get
            {
                if (mSelectionEnvironment != null)
                    return mSelectionEnvironment;
                else
                {
                    mSelectionEnvironment = new SelectionEnvironmentClass();
                    return mSelectionEnvironment;
                }
            }
            set 
            {
                this.mSelectionEnvironment = value;
            }
        }

        public ISeanCommandManager CommandManager { get { return (ISeanCommandManager)this; } }

        public ISeanLayoutManager LayoutManager { get { return (ISeanLayoutManager)this; } }
       
        public ISeanContextMenuManager ContextManager { get { return (ISeanContextMenuManager)this; } }



        public IMapControlDefault GetIMapControl()
        {
            return this.mapControlView.GetIMapControl();
        }

        public IPageLayoutControlDefault GetIPagelayoutControl()
        {
            return this.pagelayoutView.GetIPageLayoutControl();
        }

        public ITOCControlDefault GetITOCControl()
        {
            return this.tocWindow.GetITOCControl();
        }

        public void ShutDown()
        {
            this.Close();
        }
        #endregion
    }
}



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


namespace ArcGISWinForm
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

        private string Default_LayoutPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"layout\DefaultLayout.xml");

        public MainForm()
        {
            CultureInfo Ci = new CultureInfo("zh-CN");
            Thread.CurrentThread.CurrentCulture = Ci;
            Thread.CurrentThread.CurrentUICulture = Ci;
            /*登录窗体*/

            //初始化对象
            InitializeComponent();
            InitialDevControl();
            InitialResource();
            if (!RegisterResources())
            {
                //记录
                return;
            }
            RegisterResourceInBarItem();

            this.Load += new EventHandler(MainForm_Load);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CreateDockWindow();
            LoadDefaultLayout();
            this.mMapControl.LoadMxFile(@"F:\空间数据\MXD\test_double.mxd");
            this.mDefaultCommand = ((ISeanCommandManager)this).GetCommand("D55F27D6-B49C-41B6-9DE3-6E060BFC8B76");
            this.mDefaultCommand.Run();
        }
        //窗体关闭前
        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.barManager.AllowCustomization)
            {
                SaveCurrentLayout();//保存当前的布局
            }
        }

        # region 初始化控件
        private DevExpress.XtraBars.Docking.DockManager dockManager;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarSubItem newMenu;
        private ImageList imageList;

        private DevExpress.XtraBars.BarDockControl dockTop;
        private DevExpress.XtraBars.BarDockControl dockBottom;
        private DevExpress.XtraBars.BarDockControl dockLeft;
        private DevExpress.XtraBars.BarDockControl dockRight;

        private ucMapView ucMapView;

        #endregion

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitialDevControl()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));

            this.dockManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.barManager = new BarManager(this.components);
            this.imageList = new ImageList(this.components);
            this.newMenu = new BarSubItem();
            this.dockTop = new BarDockControl();
            this.dockLeft = new BarDockControl();
            this.dockBottom = new BarDockControl();
            this.dockRight = new BarDockControl();
            this.ucMapView = new ucMapView();

            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).BeginInit();
            //((System.ComponentModel.ISupportInitialize)(this.ucMapView)).BeginInit();

            this.SuspendLayout();

            this.dockManager.Form = this;
            this.dockManager.MenuManager = this.barManager;
            this.dockManager.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"
            });

            this.barManager.Categories.AddRange(new DevExpress.XtraBars.BarManagerCategory[]{
            new DevExpress.XtraBars.BarManagerCategory("菜单",new System.Guid("727A147A-51FF-4EBE-BF4A-FC332280BD16")),
                new DevExpress.XtraBars.BarManagerCategory("布局",new System.Guid("D330CA47-49B3-46F1-A7AB-58D6992B6DD1"))
            });
            this.barManager.DockControls.Add(this.dockTop);
            this.barManager.DockControls.Add(this.dockLeft);
            this.barManager.DockControls.Add(this.dockBottom);
            this.barManager.DockControls.Add(this.dockRight);
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] { this.newMenu });
            this.barManager.DockManager = this.dockManager;
            this.barManager.Form = this;
            this.barManager.Images = this.imageList;
            this.barManager.AllowCustomization = true;

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

            //imagelist
            this.imageList.ColorDepth = ColorDepth.Depth8Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;

            //newmenu
            this.newMenu.Caption = "新菜单";
            this.newMenu.CategoryGuid = Guid.Parse("8A756F59-4674-41D7-9695-918A705291C0");
            this.newMenu.Id = -559393299;
            this.newMenu.Name = "newMenu";

            //ucmapview
            this.ucMapView.Dock = DockStyle.Fill;
            this.ucMapView.Location = new System.Drawing.Point(0, 0);
            this.ucMapView.Name = "ucMapView";

            //MainForm
            this.Controls.Add(this.ucMapView);
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
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).EndInit();
            //((System.ComponentModel.ISupportInitialize)(this.ucMapView)).EndInit();

            this.ResumeLayout(false);
            this.PerformLayout();

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
            string name = "", clsid = "", cls = "", bitmap="";
            XmlAttribute attr = null;
            ResourceConfigStructure resourceDefinition = null;
            foreach (XmlNode node in poolNode.ChildNodes)
            {
                attr = node.Attributes["name"];
                if (attr != null) name = attr.Value;
                if (string.IsNullOrEmpty(name))
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

                attr = node.Attributes["bitmap"];
                if (attr != null) bitmap = attr.Value;
                if (string.IsNullOrEmpty(bitmap))
                {
                    errorMsg = string.Format("节点bitmap不存在，在LoadResourceDefinitions 行处", node.Name);
                    return false;
                }

                resourceDefinition = new ResourceConfigStructure(name, clsid, cls, bitmap);
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
                        Bitmap bitmap = definition.Bitmap;
                        command.Bitmap = bitmap;
                        this.commandList.Add(command);
                    }
                    else if (resource.Type == enumResourceType.View)
                        this.viewList.Add((ISeanView)resource);
                    else if (resource.Type == enumResourceType.TocWindow)
                    {
                        this.tocWindow = (ISeanTocWindow)resource;
                        this.dockWindowList.Add((ISeanDockWindow)resource);
                    }
                    else if (resource.Type == enumResourceType.MapView)
                    {
                        this.mapControlView = (ISeanMapControlView)resource;
                        this.viewList.Add((ISeanView)resource);
                    }
                    else if (resource.Type == enumResourceType.PageLayoutView)
                    {
                        this.pagelayoutView = (ISeanPagelayoutView)resource;
                        this.viewList.Add((ISeanPagelayoutView)resource);
                    }
                    this.resourceHashtable.Add(uid, resource);
                }
                catch (Exception ex)
                {
                    string error = string.Format("注册资源{0}失败：{1}", definition.Name, ex.Message);
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

                    this.imageList.Images.Add(command.UID.ToString(), command.Bitmap);
                    imageIndex = this.imageList.Images.Count - 1;

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
                    barItem.Hint = command.Caption;
                    barItem.Tag = command.UID.ToString();

                    this.barManager.Items.Add(barItem);
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
            foreach (BarItem item in barManager.Items)//设置所有不check
            {
                if (item is BarCheckItem)
                {
                    ((BarCheckItem)e.Item).Checked = false;
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
            if (barManager.Categories.IndexOf(command.Category) == -1)
            {
                this.barManager.Categories.Add(command.Category);
            }
        }
        /// <summary>
        /// 创建停靠窗体
        /// </summary>
        public void CreateDockWindow()
        {
            if (this.dockWindowList.Count == 0)
                return;
            foreach (ISeanDockWindow dockWindow in this.dockWindowList)
            {
                DevExpress.XtraBars.Docking.DockPanel dockPanel = null;
                for (int i = 0; i < this.dockManager.Panels.Count; i++)
                {
                    if (dockWindow.Caption == this.dockManager.Panels[i].Text)
                        dockPanel = this.dockManager.Panels[i];
                }
                if (dockPanel == null)
                {
                    dockPanel = this.dockManager.AddPanel(DevExpress.XtraBars.Docking.DockingStyle.Left);
                    dockPanel.SuspendLayout();
                    dockPanel.Text = dockWindow.Caption;
                    dockPanel.ID = Guid.NewGuid();
                }
                dockPanel.ControlContainer.Controls.Add(dockWindow.GetControl());
                dockPanel.ResumeLayout(false);
            }
        }
        /// <summary>
        /// 添加一个菜单栏bar
        /// </summary>
        public void AddMenuBar()
        {
            DevExpress.XtraBars.Bar menuBar = new Bar(this.barManager, "菜单栏");
            menuBar.LinkAdded += new LinkEventHandler(menuBar_LinkAdded);
            menuBar.OptionsBar.UseWholeRow = true;
            menuBar.OptionsBar.AllowCollapse = false;
            this.barManager.Bars.Add(menuBar);
        }

        int newMenuIndex = 0;
        /// <summary>
        /// 在菜单bar中添加了一个新的item
        /// </summary>
        void menuBar_LinkAdded(object sender, LinkEventArgs e)
        {
            if (e.Link.Item == this.newMenu)
            {
                this.newMenu.Id = Guid.NewGuid().GetHashCode();
                this.newMenu.CategoryGuid = Guid.Empty;
                this.newMenu.LinkAdded += new LinkEventHandler(menuBar_LinkAdded);//循环注册事件

                BarSubItem newMenuItem = new BarSubItem();
                newMenuItem.Id = Guid.NewGuid().GetHashCode();
                newMenuItem.CategoryGuid = new Guid("610DE763-3C55-409B-8278-C34F19094EA7");
                newMenuItem.Caption = "新菜单" + ++newMenuIndex;
                this.newMenu = newMenuItem;

                this.barManager.BeginUpdate();
                this.barManager.Items.Add(newMenuItem);
                this.barManager.ForceInitialize();
                this.barManager.EndUpdate();
            }
        }

        //更新baritem关联的图片
        private void UpdateBarItemLinkImage(BarItemLink itemLink)
        {
            itemLink.ImageIndex = itemLink.Item.ImageIndex;
            if (itemLink.Item is BarSubItem)
            {
                BarSubItem subItem = itemLink.Item as BarSubItem;
                foreach (BarItemLink item in subItem.ItemLinks)
                {
                    UpdateBarItemLinkImage(item);
                }
            }
        }

        //保存布局到配置文件
        private void SaveLayout()
        {
            if (System.IO.File.Exists(Default_LayoutPath))
            {
                System.IO.File.Delete(Default_LayoutPath);
            }

            this.barManager.SaveLayoutToXml(Default_LayoutPath);

            System.Xml.XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Default_LayoutPath);
            XmlNode nodeMenus = xmlDoc.CreateElement("Menus");
            xmlDoc.FirstChild.AppendChild(nodeMenus);
            for (int i = 0; i < this.barManager.Items.Count; i++)
            {
                BarItem barItem = barManager.Items[i];
                if (barItem is BarSubItem)
                {
                    if (barItem == this.newMenu)
                        continue;
                    XmlNode nodeItem = xmlDoc.CreateElement("Item");
                    XmlAttribute attrID = xmlDoc.CreateAttribute("ID");
                    XmlAttribute attrName = xmlDoc.CreateAttribute("Name");
                    XmlAttribute attrCaption = xmlDoc.CreateAttribute("Caption");

                    attrID.Value = barItem.Id.ToString();
                    attrName.Value = barItem.Name;
                    attrCaption.Value = barItem.Caption;

                    nodeItem.Attributes.Append(attrID);
                    nodeItem.Attributes.Append(attrName);
                    nodeItem.Attributes.Append(attrCaption);

                    nodeMenus.AppendChild(nodeItem);
                }
            }
            xmlDoc.Save(Default_LayoutPath);
        }

        //恢复布局
        private void RestoreDefaultLayout()
        {
            if (System.IO.File.Exists(Default_LayoutPath))
            {
                System.Xml.XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Default_LayoutPath);
                System.Xml.XmlNode xmlNode = xmlDoc.FirstChild.SelectSingleNode("Menus");
                if (xmlNode != null)
                {
                    for (int i = 0; i < xmlNode.ChildNodes.Count; i++)
                    {
                        XmlNode nodeItem = xmlNode.ChildNodes[i];
                        XmlAttribute attrID = nodeItem.Attributes["ID"];
                        XmlAttribute attrName = nodeItem.Attributes["Name"];
                        XmlAttribute attrCaption = nodeItem.Attributes["Caption"];

                        DevExpress.XtraBars.BarSubItem barSubItem = new BarSubItem();
                        barSubItem.Id = Int32.Parse(attrID.Value);
                        barSubItem.Name = attrName.Value;
                        barSubItem.Caption = attrCaption.Value;
                        barSubItem.Category = null;
                        BarItem item = barManager.Items.FindById(barSubItem.Id);
                        if (item == null)
                        {
                            barSubItem.LinkAdded += new LinkEventHandler(menuBar_LinkAdded);
                            barManager.Items.Add(barSubItem);
                        }
                    }
                }

                this.barManager.RestoreLayoutFromXml(Default_LayoutPath);

                foreach (Bar bar in this.barManager.Bars)
                {
                    bar.BeginUpdate();
                    for (int i = 0; i < bar.ItemLinks.Count; i++)
                    {
                        UpdateBarItemLinkImage(bar.ItemLinks[i]);
                    }
                    bar.EndUpdate();
                }
            }
            //允许对默认的bar进行修改和删除
            for (int i = 0; i < this.barManager.Bars.Count; i++)
            {
                this.barManager.Bars[i].OptionsBar.UseWholeRow = true;
                this.barManager.Bars[i].OptionsBar.AllowCollapse = true;
                this.barManager.Bars[i].OptionsBar.AllowRename = true;
                this.barManager.Bars[i].OptionsBar.AllowDelete = true;
                this.barManager.Bars[i].LinkAdded += new LinkEventHandler(menuBar_LinkAdded);
            }
        }

        #region ISeanContextManager
        public void ShowContextMenu(string groupname)
        { }

        public void ShowContextMenu(string groupname, int x, int y)
        { }

        public void SetUserData(string groupname, object data)
        { }
        #endregion

        #region ISeanLayoutManager
        //加载默认布局
        public void LoadDefaultLayout()
        {
            RestoreDefaultLayout();
        }
        //加载指定布局视图
        public void LoadLayout()
        {

        }
        //直接保存当前的布局
        public void SaveCurrentLayout()
        {

        }
        //另存为当前布局视图
        public void SaveAsLayout()
        {

        }
        //设置界面样式
        public void SetVisualStyle()
        {

        }
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
            return this.commandList.Find(o => o.UID.ToString() == uid.ToString());
        }

        public ISeanCommand GetCommand(string uid)
        {
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
            return this.ucMapView.GetIMapControl();
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



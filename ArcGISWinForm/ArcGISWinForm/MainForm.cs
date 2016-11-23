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

    public partial class MainForm : Form, ISeanApplication, ISeanStatusBar
    {
        public MainForm()
        {
            CultureInfo Ci = new CultureInfo("zh-CN");
            Thread.CurrentThread.CurrentCulture = Ci;
            Thread.CurrentThread.CurrentUICulture = Ci;
            //登录窗体

            //初始化对象
            InitializeComponent();
            InitialDevControl();
            InitialResource();
            if (!RegisterResources())
                return;
            RegisterResourceInBarItem();

            this.Load += new EventHandler(MainForm_Load);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {


        }


        private DevExpress.XtraBars.Docking.DockManager docManager;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarSubItem newMenu;
        private ImageList imageList;

        private DevExpress.XtraBars.BarDockControl dockTop;
        private DevExpress.XtraBars.BarDockControl dockBottom;
        private DevExpress.XtraBars.BarDockControl dockLeft;
        private DevExpress.XtraBars.BarDockControl dockRight;

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitialDevControl()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));

            this.docManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.barManager = new BarManager(this.components);
            this.imageList = new ImageList(this.components);
            this.newMenu = new BarSubItem();
            this.dockTop = new BarDockControl();
            this.dockLeft = new BarDockControl();
            this.dockBottom = new BarDockControl();
            this.dockRight = new BarDockControl();

            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.docManager)).BeginInit();


            this.SuspendLayout();

            this.docManager.Form = this;
            this.docManager.MenuManager = this.barManager;
            this.docManager.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"
            });

            this.barManager.Categories.AddRange(new DevExpress.XtraBars.BarManagerCategory[]{
            new DevExpress.XtraBars.BarManagerCategory("菜单",new System.Guid("")),
                new DevExpress.XtraBars.BarManagerCategory("布局",new System.Guid(""))
            });
            this.barManager.DockControls.Add(this.dockTop);
            this.barManager.DockControls.Add(this.dockLeft);
            this.barManager.DockControls.Add(this.dockBottom);
            this.barManager.DockControls.Add(this.dockRight);
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] { this.newMenu });
            this.barManager.DockManager = this.docManager;
            this.barManager.Form = this;
            this.barManager.Images = this.imageList;


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
            string name = "", clsid = "", cls = "";
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

                resourceDefinition = new ResourceConfigStructure(name, clsid, cls);
                this.resourceConfigStructure.Add(resourceDefinition);
            }
            errorMsg = "";
            return true;
        }

        #region 资源集合与变量
        private List<ISeanCommand> commandList = new List<ISeanCommand>();
        private List<ISeanDockWindow> dockWindowList = new List<ISeanDockWindow>();
        private List<ISeanView> viewList = new List<ISeanView>();

        private ISeanMapControlView mapView;
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
                    this.resourceHashtable.Add(uid, resource);

                    if (resource.Type == enumResourceType.Command)
                        this.commandList.Add((ISeanCommand)resource);
                    else if (resource.Type == enumResourceType.View)
                        this.viewList.Add((ISeanView)resource);
                    else if (resource.Type == enumResourceType.TocWindow)
                    {
                        this.tocWindow = (ISeanTocWindow)resource;
                        this.dockWindowList.Add((ISeanDockWindow)resource);
                    }
                    else if (resource.Type == enumResourceType.MapView)
                    {
                        this.mapView = (ISeanMapControlView)resource;
                        this.viewList.Add((ISeanView)resource);
                    }
                    else if (resource.Type == enumResourceType.PageLayoutView)
                    {
                        this.pagelayoutView = (ISeanPagelayoutView)resource;
                        this.viewList.Add((ISeanPagelayoutView)resource);
                    }
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
            foreach (ISeanResource resource in this.resourceHashtable)
            {
                resource.SetHook((ISeanApplication)this);

                DevExpress.XtraBars.BarItem barItem = null;
                int imageIndex = -1;
                if (resource is ISeanCommand)
                {
                    ISeanCommand command = resource as ISeanCommand;

                    RegisterCategory(command);

                    this.imageList.Images.Add(command.Bitmap.ToString(), command.Bitmap);
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
                    barItem.Caption = command.Name;
                    barItem.Id = command.UID.ToString().GetHashCode();
                    if (imageIndex != -1)
                        barItem.ImageIndex = imageIndex;
                    barItem.Hint = command.Name;
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using DevExpress.XtraBars;
using SeanShen.Framework;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.SystemUI;

namespace SeanShen.ArcGISWinForm
{
    /*************************************************
     * 作者：沈鑫
     * 版本号：V1.0
     * 创建日期：2016年12月1日,星期四  09:34:30  Thursday
     * 说明：实现主窗体需要实现的接口
     * **************************************************/
    public partial class MainForm
    {        
        #region ISeanLayoutManager
        //可以自定义的默认布局
        private string Default_LayoutPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"layout\DefaultLayout.xml");
        //出厂设置的默认布局
        private string Restore_Default_LayoutPath=System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory+@"layout\RestoreDefaultLayout.xml");

        //保存为当前的默认布局——程序初始化时自动加载默认布局
        public void SaveAsDefaultLayout()
        {
            if (System.IO.File.Exists(Default_LayoutPath))
            {

                this.barManager1.SaveLayoutToXml(Default_LayoutPath);
                System.Windows.Forms.MessageBox.Show("保存成功！");
            }
        }

        //恢复最原始的默认布局——该文件不会被写入
        public void RestoreDefaultLayout()
        {
            if (System.IO.File.Exists(Restore_Default_LayoutPath))
            {

                this.barManager1.RestoreLayoutFromXml(Restore_Default_LayoutPath);

            }
            ResetBar();
        }

        //加载默认布局
        public void LoadDefaultLayout()
        {
            if (System.IO.File.Exists(Default_LayoutPath))
            {

                this.barManager1.RestoreLayoutFromXml(Default_LayoutPath);
            }
            ResetBar();
        }
        //加载指定布局视图
        public void LoadLayout()
        {
            System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();
            openFile.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            openFile.Filter = "XML文件(*.xml)|*.xml";
            openFile.FilterIndex = 2;
            openFile.RestoreDirectory = true;
            openFile.Multiselect=false;
            openFile.DefaultExt = "*.xml";
            openFile.Title = "加载布局";
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.barManager1.RestoreLayoutFromXml(openFile.FileName);
            }
            ResetBar();
        }
        //另存为当前布局视图
        public void SaveAsCurrentLayout()
        {
            System.Windows.Forms.SaveFileDialog saveFile = new System.Windows.Forms.SaveFileDialog();
            saveFile.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            saveFile.Filter = "XML文件(*.xml)|*.xml";
            saveFile.FilterIndex = 2;
            saveFile.Title = "另存为当前布局";
            saveFile.OverwritePrompt = true;
            if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.barManager1.SaveLayoutToXml(saveFile.FileName);
            }
        }
        //设置界面样式
        public void SetVisualStyle()
        {
            //需要重构为一个下拉框
        }
        #endregion

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
                this.staticItem_Message.Caption = this.m_StatusMessage;
            }
        }

        public void ShowCoordinate(double x, double y)
        {
            string coordinateMsg = string.Format("坐标：{0:0.###},{1:0.###} {2}           比例：{3:0}     ", x, y, AEUtilities.MapHelper.GetMapUnit(this.mMapControl.MapUnits), this.mMapControl.MapScale);
            this.staticItem_Coordinate.Caption = coordinateMsg;
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
        private ISelectionEnvironment mSelectionEnvironment;

        public string Caption
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        public System.Windows.Forms.Form CurrentMainForm { get { return this; } }

        public ISeanCommand CurrentCommand
        {
            get { return this.mCurrentCommand; }
            set
            {
                this.mCurrentCommand = value;
                //((ISeanStatusBar)this).Message = this.mCurrentCommand.Caption;
                if (this.mCurrentCommand is ITool)//必须要，因为不是每一个tool都是调用内置tool
                {
                    this.mMapControl.CurrentTool = this.mCurrentCommand as ITool;
                }
            }
        }

        public ISeanCommand DefaultCommand
        {
            get { return this.mDefaultCommand; }
        }

        public IWorkspace Workspace
        {
            get { return AEUtilities.WorkspaceHelper.GetCurrentWorkspace(this.Map); }
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

        public ISeanStatusBar StatusBar { get { return (ISeanStatusBar)this; } }


        public IMapControlDefault GetIMapControl()
        {
            return this.mapControlView.GetIMapControl();
        }

        public ESRI.ArcGIS.Controls.AxMapControl GetAxMapControl()
        {
            return this.mapControlView.GetAxMapControl();
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
        
        #region 私有方法（只与界面操作有关）
        /// <summary>
        /// 重置image索引，每次加载布局时必须进行这个操作，否则command对应的图片就会随机分配
        /// </summary>
        /// <param name="itemLink"></param>
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
        /// <summary>
        /// 重置bar
        /// </summary>
        private void ResetBar()
        {
            //允许对默认的bar进行修改和删除
            foreach (Bar bar in this.barManager1.Bars)
            {
                bar.BeginUpdate();
                for (int i = 0; i < bar.ItemLinks.Count; i++)
                {
                    UpdateBarItemLinkImage(bar.ItemLinks[i]);
                }
                bar.EndUpdate();
            }
        }
        #endregion
    }
}

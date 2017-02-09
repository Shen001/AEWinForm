

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.Framework
{
    /*
    Time: 18/11/2016 10:09 PM 周五
    Author: shenxin
    Description: 框架应用主接口
    Modify:
    */
    public interface ISeanApplication
    {
        #region 属性
        /// <summary>
        /// 标题
        /// </summary>
        string Caption { get; set; }
        /// <summary>
        /// 窗体名称
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 主窗体
        /// </summary>
        System.Windows.Forms.Form CurrentMainForm { get; }
        /// <summary>
        /// 当前的command
        /// </summary>
        ISeanCommand CurrentCommand { get; set; }
        /// <summary>
        /// 默认command，一般为pan
        /// </summary>
        ISeanCommand DefaultCommand { get; }
        /// <summary>
        /// 当前数据的workspace
        /// </summary>
        /// <returns></returns>
        ESRI.ArcGIS.Geodatabase.IWorkspace Workspace { get; }
        /// <summary>
        /// map对象
        /// </summary>
        ESRI.ArcGIS.Carto.IMap Map { get; }
        /// <summary>
        /// 当前的Pagelayout
        /// </summary>
        ESRI.ArcGIS.Carto.IPageLayout PageLayout { get; }
        /// <summary>
        /// 当前展示的view，mapcontrol或者是pagelayoutcontrol
        /// </summary>
        ISeanView CurrentView { get; set; }
        /// <summary>
        /// 选择集环境设置
        /// </summary>
        ESRI.ArcGIS.Carto.ISelectionEnvironment SelectionEnvironment { get; set; }
        /// <summary>
        /// mapcontrol和pagelayoutcontrol同步器
        /// </summary>
        //ControlsSynchronizer Synchronizer { get; }
        /// <summary>
        /// 当前打开mxd文档路径
        /// </summary>
        string DocumentFileName { get; set; }

        //管理器
        ISeanCommandManager CommandManager { get; }

        ISeanLayoutManager LayoutManager { get; }

        ISeanContextMenuManager ContextManager { get; }

        ISeanStatusBar StatusBar { get; }
        #endregion


        #region 方法
        /// <summary>
        /// 得到IMapControlDefault
        /// </summary>
        /// <returns></returns>
        ESRI.ArcGIS.Controls.IMapControlDefault GetIMapControl();
        /// <summary>
        /// 得到AxMapControl
        /// </summary>
        /// <returns></returns>
        ESRI.ArcGIS.Controls.AxMapControl GetAxMapControl();
        /// <summary>
        /// 得到IPagelayoutControl
        /// </summary>
        /// <returns></returns>
        ESRI.ArcGIS.Controls.IPageLayoutControlDefault GetIPagelayoutControl();
        /// <summary>
        /// 得到AxPageLayoutControl
        /// </summary>
        /// <returns></returns>
        ESRI.ArcGIS.Controls.AxPageLayoutControl GetAxPagelayoutControl();
        /// <summary>
        /// 获得ITOCControl
        /// </summary>
        /// <returns></returns>
        ESRI.ArcGIS.Controls.ITOCControlDefault GetITOCControl();
        /// <summary>
        /// 获得AxTOCControl
        /// </summary>
        /// <returns></returns>
        ESRI.ArcGIS.Controls.AxTOCControl GetAxTOCControl();
        /// <summary>
        /// 关闭窗体
        /// </summary>
        void ShutDown();
        #endregion
    }
}



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
        /// 应用程序句柄
        /// </summary>
        int hWnd { get; }
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
        /// 当前展示的view，mapcontrol或者是pagelayoutcontrol
        /// </summary>
        ISeanView CurrentView { get; }
        /// <summary>
        /// 选择集环境设置
        /// </summary>
        ESRI.ArcGIS.Carto.ISelectionEnvironment SelectionEnvironment { get; set; }

        ISeanCommandManager CommandManager { get; }

        ISeanLayoutManager LayoutManager { get; }

        ISeanContextMenuManager ContextManager { get; }

        ISeanStatusBar StatusBar { get; }
        #endregion


        #region 方法
        /// <summary>
        /// 得到MapControl
        /// </summary>
        /// <returns></returns>
        ESRI.ArcGIS.Controls.IMapControlDefault GetIMapControl();
        /// <summary>
        /// 得到PagelayoutControl
        /// </summary>
        /// <returns></returns>
        ESRI.ArcGIS.Controls.IPageLayoutControlDefault GetIPagelayoutControl();
        /// <summary>
        /// 获得TOC
        /// </summary>
        /// <returns></returns>
        ESRI.ArcGIS.Controls.ITOCControlDefault GetITOCControl();
        /// <summary>
        /// 关闭窗体
        /// </summary>
        void ShutDown();
        #endregion
    }
}

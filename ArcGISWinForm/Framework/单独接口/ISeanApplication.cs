

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
        /// <summary>
        /// 标题
        /// </summary>
         string Caption { get; set; }
        /// <summary>
        /// 窗体名称
        /// </summary>
         string Name { get; set; }
        /// <summary>
        /// 应用程序句柄
        /// </summary>
         int hWnd { get; set; }
        /// <summary>
        /// 当前的command
        /// </summary>
         ISeanCommand CurrentCommand { get; set; }

         ISeanCommand DefaultCommand { get; }

        /// <summary>
        /// 得到MapControl
        /// </summary>
        /// <returns></returns>
         ESRI.ArcGIS.Controls.IMapControlDefault GetMapControl();

        /// <summary>
        /// 得到PagelayoutControl
        /// </summary>
        /// <returns></returns>
         ESRI.ArcGIS.Controls.IPageLayoutControlDefault GetPagelayoutControl();
    }
}

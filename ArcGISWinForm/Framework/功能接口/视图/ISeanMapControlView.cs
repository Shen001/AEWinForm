using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.Framework
{           
    /*
    Time: 18/11/2016 10:09 PM 周五
    Author: shenxin
    Description: map视图接口
    Modify: 增加定位到AxMapControl的方法
    */
    public interface ISeanMapControlView:ISeanView
    {
        /// <summary>
        /// 获取IMapControlDefault
        /// </summary>
        /// <returns></returns>
        ESRI.ArcGIS.Controls.IMapControlDefault GetIMapControl();
        /// <summary>
        /// 获取AxMapControl
        /// </summary>
        /// <returns></returns>
        ESRI.ArcGIS.Controls.AxMapControl GetAxMapControl();
    }
}

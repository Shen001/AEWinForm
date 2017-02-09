using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.Framework
{
    /*
    Time: 18/11/2016 10:09 PM 周五
    Author: shenxin
    Description: pagelayout视图接口
    Modify:
    */
    public interface ISeanPagelayoutView : ISeanView
    {
        ESRI.ArcGIS.Controls.IPageLayoutControlDefault GetIPageLayoutControl();

        ESRI.ArcGIS.Controls.AxPageLayoutControl GetAxPagelayouControl();
    }
}

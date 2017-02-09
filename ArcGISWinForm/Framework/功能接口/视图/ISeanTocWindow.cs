using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.Framework
{
    /*
    Time: 18/11/2016 10:09 PM 周五
    Author: shenxin
    Description: toc容器
    Modify:
    */
    public interface ISeanTocWindow:ISeanDockWindow
    {
        ESRI.ArcGIS.Controls.ITOCControlDefault GetITOCControl();

        ESRI.ArcGIS.Controls.AxTOCControl GetAxTOCControl();
    }
}

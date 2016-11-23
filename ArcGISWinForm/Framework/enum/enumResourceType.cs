using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.Framework
{
    /*
    Time: 18/11/2016 10:09 PM 周五
    Author: shenxin
    Description: 资源枚举类型，实现了接口ISeanResource的成员拥有该枚举
    Modify:
    */
    public enum enumResourceType
    {
        Unkown,

        Command,//所有实现了ISeanCommand接口的类

        DockableWindow,

        View,

        TocWindow,

        MapView,

        PageLayoutView
    }
}

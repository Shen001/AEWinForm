using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.Framework
{
        /*
    Time: 18/11/2016 10:09 PM 周五
    Author: shenxin
    Description: 容器接口
    Modify:
    */
    public interface ISeanDockWindow:ISeanResource
    {
        System.Windows.Forms.Control GetControl();

        void Show();

        void Hide();
    }
}

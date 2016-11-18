using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.Framework
{
    /*
    Time: 18/11/2016 10:09 PM 周五
    Author: shenxin
    Description: 所有视图接口
    Modify:
    */
    public interface ISeanView
    {
        System.Windows.Forms.Control GetControl();

        void Show();
    }
}

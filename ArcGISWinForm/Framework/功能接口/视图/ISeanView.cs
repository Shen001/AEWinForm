using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace SeanShen.Framework
{
    /*
    Time: 18/11/2016 10:09 PM 周五
    Author: shenxin
    Description: 所有视图接口
    Modify:
    */
    public interface ISeanView:ISeanResource
    {
        Control GetControl();

        void Show();
    }
}

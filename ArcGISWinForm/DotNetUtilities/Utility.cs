using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.DotNetUtilities
{
    /*
    Time: 21/11/2016 15:30 PM 周一
    Author: shenxin
    Description:暂时作为通用的工具类
    Modify:
    */
			
    public class Utility
    {
        /// <summary>
        /// 根据窗体句柄获取当前的活动窗体
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        public static System.Windows.Forms.Form GetFormByHandle(int hWnd)
        {
            IntPtr ptr = new IntPtr(hWnd);

            System.Windows.Forms.Form form = System.Windows.Forms.Control.FromHandle(ptr) as System.Windows.Forms.Form;

            return form;
        }
    }
}

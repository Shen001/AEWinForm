using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.Framework
{
    /*
    Time: 21/11/2016 11:17 AM 周一
    Author: shenxin
    Description:窗体底部状态栏，类似于ArcMap
    Modify:
    */		
    public interface ISeanStatusBar
    {
        /// <summary>
        /// 当前状态栏的信息，主要显示当前的command
        /// </summary>
        string Message { get; set; }
        /// <summary>
        /// 显示当前坐标
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void ShowCoordinate(double x, double y);

        void ShowProgressBar(string Message, int min, int max, int Step);

        void HideProgressBar();

        void StepProgressBar();
    }
}

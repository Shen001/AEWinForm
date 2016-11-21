using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.Framework
{
    /*
    Time: 18/11/2016 10:09 PM 周五
    Author: shenxin
    Description: 主要的command接口
    Modify:
    */
    public interface ISeanCommand : ISeanResource
    {
        System.Drawing.Bitmap Bitmap { get; }
        /// <summary>
        /// 分类
        /// </summary>
        string Category { get;}

        bool Enabled { get; }

        bool Checked { get; }

        string Tooltip { get; }
        /// <summary>
        /// 运行命令
        /// </summary>
        void Run();
    }
}

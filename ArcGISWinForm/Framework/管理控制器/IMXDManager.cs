using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.Framework
{
    /*
    Time: 21/11/2016 14:18 PM 周一
    Author: shenxin
    Description: 管理MXD操作
    Modify:
    */
			
    public interface IMXDManager
    {
        /// <summary>
        /// 加载mxd
        /// </summary>
        void LoadMXDDocument(string mxdPath);
        /// <summary>
        /// 保存当前的Document
        /// </summary>
        void SaveMXDDocument();
        /// <summary>
        /// 另存为
        /// </summary>
        /// <param name="path"></param>
        void SaveAs(string path);
    }
}

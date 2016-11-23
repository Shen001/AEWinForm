using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.Framework
{
    /*
    Time: 21/11/2016 14:49 PM 周一
    Author: shenxin
    Description: 右键菜单管理
    Modify:
    */
			
    public interface ISeanContextMenuManager
    {
        /// <summary>
        /// 在默认位置显示右键菜单
        /// </summary>
        /// <param name="groupname"></param>
        void ShowContextMenu(string groupname);

        /// <summary>
        /// 在指定点显示右键菜单
        /// </summary>
        /// <param name="groupname"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void ShowContextMenu(string groupname, int x, int y);

        /// <summary>
        /// 给右键菜单中所有项设置值
        /// </summary>
        /// <param name="groupname"></param>
        /// <param name="data"></param>
        void SetUserData(string groupname, object data);
    }
}

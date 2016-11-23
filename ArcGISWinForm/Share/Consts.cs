using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Share
{
    /*
    Time: 22/11/2016 10:23 AM 周二
    Author: shenxin
    Description: 全局静态类，编译时确定；主要设置全局的静态参数
    Modify:
    */
			
    public static class Consts
    {
        /// <summary>
        /// cs客户端的名称
        /// </summary>
        public const string APP_NAME_CS = "SeanShen_CS";

        /// <summary>
        /// 定义权限数据库连接字符串名称
        /// </summary>
        public const string AUTH_DATABASE_CONNECTION_STRING = "SeanShen_AuthDatabaseConnetionString";

        /// <summary>
        /// 定义业务数据库连接字符串名称
        /// </summary>
        public const string BIZ_DATABASE_CONNECTION_STRING = "SeanShen_BizDatabaseConnetionString";

        /// <summary>
        /// 定义空间数据库连接字符串名称
        /// </summary>
        public const string SPATIAL_DATABASE_CONNECTION_STRING = "SeanShen_SpatialDatabaseConnetionString";

        /// <summary>
        /// 定义系统日志分类名称
        /// </summary>
        public const string LOG_LOGIN = "登录";

        public const string LOG_LOGOUT = "登出";

        public const string LOG_LEVEL = "权限等级";

        /// <summary>
        /// 添加注记事件
        /// </summary>
        public const string EVENT_ADDLABEL = "WxEventAddLabel";
    }
}

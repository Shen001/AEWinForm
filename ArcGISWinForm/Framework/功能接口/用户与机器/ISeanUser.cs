using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.Framework
{
    /// <summary>
    /// 定义用户接口
    /// </summary>
    public interface ISeanUser
    {

        /// <summary>
        /// 获取或设置用户编号
        /// </summary>
        int UserId { get; set; }

        /// <summary>
        /// 获取或设置用户名称
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// 获取或设置登陆用户姓名
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 获取或设置登陆用户工号
        /// </summary>
        string JobNumber { get; set; }

        /// <summary>
        /// 获取或设置一个值用于确定是否登陆成功
        /// </summary>
        bool IsSuccess { get; set; }

        /// <summary>
        /// 获取或设置用户登陆唯一识别号
        /// </summary>
        string Token { get; set; }

        /// <summary>
        /// 获取或设置用户登陆时间
        /// </summary>
        DateTime LoginTime { get; set; }
    }

    public class SeanUser : ISeanUser
    {
        #region IWxUser 成员

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string JobNumber { get; set; }

        public string Token { get; set; }

        public bool IsSuccess { get; set; }

        public DateTime LoginTime { get; set; }

        #endregion
    }
}

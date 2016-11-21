using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.Framework
{
    /*
    Time: 18/11/2016 10:09 PM 周五
    Author: shenxin
    Description: 全局环境变量绑定到Environment的GlobalData上
    Modify:
    */


    /// <summary>
    /// 定义系统应用环境接口
    /// </summary>
    public interface ISeanEnviroment
    {
        /// <summary>
        /// 系统应用用户信息
        /// </summary>
        ISeanUser User { get; }

        /// <summary>
        /// 系统应用主机信息
        /// </summary>
        ISeanMachine Machine { get; }

        /// <summary>
        /// 系统全局参数hashtable
        /// </summary>
        System.Collections.Hashtable GlobalData { get; }
    }

    public class SeanEnviroment : ISeanEnviroment
    {
        private static ISeanEnviroment m_enviroment;
        private SeanEnviroment() { }

        public static ISeanEnviroment GetInstance()
        {
            if (m_enviroment == null)
                m_enviroment = new SeanEnviroment();
            return m_enviroment;
        }


        #region IWxEnviroment 成员

        private ISeanUser m_user = new SeanUser();
        public ISeanUser User
        {
            get
            {
                return m_user;
            }

        }
        //单例全局环境对象
        private ISeanMachine m_machine = new SeanMachine();
        public ISeanMachine Machine
        {
            get
            {
                return m_machine;
            }
        }

        private System.Collections.Hashtable m_hashtable = new System.Collections.Hashtable();
        public System.Collections.Hashtable GlobalData
        {
            get
            {
                return m_hashtable;
            }
        }

        #endregion
    }
}

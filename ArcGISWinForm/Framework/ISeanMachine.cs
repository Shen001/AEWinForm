using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.Framework
{
    /// <summary>
    /// 定义当前操作主机接口
    /// </summary>
    public interface ISeanMachine
    {
        /// <summary>
        /// 主机名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 主机IP
        /// </summary>
        string IP { get; }
    }

    public class SeanMachine : ISeanMachine
    {
        private string m_host = System.Net.Dns.GetHostName();

        #region IWxMachine 成员

        public string Name
        {
            get { return m_host; }
        }

        public string IP
        {
            get
            {
                System.Net.IPHostEntry ip = System.Net.Dns.GetHostEntry(m_host);
                return ip.AddressList[0].ToString();
            }
        }

        #endregion
    }
}

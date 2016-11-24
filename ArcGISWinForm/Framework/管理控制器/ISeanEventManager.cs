using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.Framework
{
    /*
    Time: 18/11/2016 10:09 PM 周五
    Author: shenxin
    Description: 用于对注册事件管理，可以和全局的globaldata搭配使用
    Modify:
    */


    /// <summary>
    /// 事件参数类
    /// </summary>
    public sealed class SeanEventArgs : EventArgs
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public string Type { get; internal set; }

        /// <summary>
        /// 事件参数
        /// </summary>
        public object[] Params { get; set; }
    }

    /// <summary>
    /// 事件管理器接口
    /// </summary>
    public interface ISeanEventManager
    {
        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="handler">事件监听</param>
        void Register(string type, SeanEventHandler handler);

        /// <summary>
        /// 取消注册事件
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="handler">事件监听</param>
        void UnRegister(string type, SeanEventHandler handler);

        /// <summary>
        /// 取消所有注册的事件
        /// </summary>
        /// <param name="type">事件类型</param>
        void Remove(string type);
        /// <summary>
        /// 根据类型找到注册事件类型
        /// </summary>
        /// <param name="type">事件类型</param>
        bool FindType(string type);
        /// <summary>
        /// 激活事件
        /// </summary>
        /// <param name="type">事件类型</param>
        /// <param name="sender">事件对象</param>
        /// <param name="e">事件参数</param>
        void FireEvent(string type, object sender, SeanEventArgs e);

    }

    /// <summary>
    /// 定义事件委托
    /// </summary>
    /// <param name="sender">事件对象</param>
    /// <param name="e">事件参数</param>
    public delegate void SeanEventHandler(object sender, SeanEventArgs e);

    /// <summary>
    /// 事件管理器
    /// </summary>
    public class SeanEventManager : ISeanEventManager
    {
        private static ISeanEventManager m_instance;
        private IDictionary<string, IList<SeanEventHandler>> m_eventHandlers = null;
        private SeanEventManager()
        {
            m_eventHandlers = new Dictionary<string, IList<SeanEventHandler>>();
        }

        public static ISeanEventManager GetInstance()
        {
            if (m_instance == null)
                m_instance = new SeanEventManager();
            return m_instance;
        }

        public void Register(string type, SeanEventHandler handler)
        {
            if (m_eventHandlers.ContainsKey(type))
            {
                m_eventHandlers[type].Add(handler);
            }
            else
            {
                m_eventHandlers.Add(type, new List<SeanEventHandler>() { handler });
            }
        }

        public void UnRegister(string type, SeanEventHandler handler)
        {
            IList<SeanEventHandler> handlers = this.m_eventHandlers[type];
            if (handlers != null)
            {
                foreach (var hdl in handlers)
                {
                    if (hdl == handler)
                        handlers.Remove(hdl);
                }
            }
        }

        public void Remove(string type)
        {
            if (!FindType(type))
                return;
            IList<SeanEventHandler> handlers = this.m_eventHandlers[type];
            if (handlers != null)
            {
                this.m_eventHandlers.Remove(type);
            }
        }

        public bool FindType(string type)
        {
            foreach (KeyValuePair<string, IList<SeanEventHandler>> item in this.m_eventHandlers)
            {
                if (item.Key == type)
                    return true;
            }
            return false;
        }
        public void FireEvent(string type, object sender, SeanEventArgs e)
        {
            if (!string.IsNullOrEmpty(type))
            {
                if (sender == null) sender = this;
                if (e == null) e = new SeanEventArgs();
                e.Type = type;
                IList<SeanEventHandler> handlers = null;
                this.m_eventHandlers.TryGetValue(type, out handlers);
                if (handlers != null)
                {
                    foreach (var hdl in handlers)
                        hdl(sender, e);
                }
            }
        }

    }
}

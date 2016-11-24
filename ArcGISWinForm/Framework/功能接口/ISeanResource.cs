using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.Framework
{
    /*
    Time: 18/11/2016 10:09 PM 周五
    Author: shenxin
    Description: 所有系统资源的接口
    Modify:
    */
    public interface ISeanResource
    {
        /// <summary>
        /// guid唯一标示符
        /// </summary>
        Guid UID { get; }
        /// <summary>
        /// 资源名称，一般作为中文
        /// </summary>
        string Caption { get; }
        //资源名称-一般作为英文（类名）
        string Name { get; }
        /// <summary>
        /// 资源类型
        /// </summary>
        enumResourceType Type { get; }
        /// <summary>
        /// 每个资源的自定义的配置数据
        /// </summary>
        //object UserData { get; set; }
        /// <summary>
        /// 为每个资源绑定当前的application
        /// </summary>
        /// <param name="application"></param>
        void SetHook(ISeanApplication application);
    }
}

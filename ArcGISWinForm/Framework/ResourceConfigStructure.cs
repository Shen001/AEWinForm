using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.Framework
{
    /*
    Time: 22/11/2016 10:56 AM 周二
    Author: shenxin
    Description:command的配置文件XML中的数据结构
    Modify:
    */
    public class ResourceConfigStructure
    {
        private string m_Name;
        private string m_ClassName;
        private string m_UID;

        public string Name { get { return this.m_Name; } }

        public string ClassName { get { return this.m_ClassName; } }

        public string UID
        {
            get
            {
                return this.m_UID.ToLower();
            }
        }

        public ResourceConfigStructure(string name, string uid, string cls)
        {
            this.m_Name = name;
            this.m_UID = uid;
            this.m_ClassName = cls;
        }

        public ISeanResource Create()
        {
            System.Type t = System.Type.GetType(this.m_ClassName);
            return (ISeanResource)Activator.CreateInstance(t);
        }
    }

}

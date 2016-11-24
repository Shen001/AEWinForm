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
        private string m_Bitmap;

        public string Name { get { return this.m_Name; } }

        public string ClassName { get { return this.m_ClassName; } }

        public string UID
        {
            get
            {
                return this.m_UID.ToLower();
            }
        }

        public System.Drawing.Bitmap Bitmap
        {
            get
            {
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"images\UI\small_16x16\",m_Bitmap);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(path);

                return bitmap;
            }
        }

        public ResourceConfigStructure(string name, string uid, string cls,string bitmap)
        {
            this.m_Name = name;
            this.m_UID = uid;
            this.m_ClassName = cls;
            this.m_Bitmap = bitmap;
        }

        public ISeanResource Create()
        {
            System.Type t = System.Type.GetType(this.m_ClassName);
            return (ISeanResource)Activator.CreateInstance(t);
        }
    }

}

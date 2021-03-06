﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.Framework
{
    /*
    Time: 18/11/2016 10:09 PM 周五
    Author: shenxin
    Description: 所有与AO无关的command
    Modify:
    */
    public abstract class SeanBaseCommand:ISeanCommand
    {
        protected ISeanApplication m_Application = null;//保护成员继承给子类
        protected ESRI.ArcGIS.Controls.IMapControlDefault m_MapControl = null;
        protected ESRI.ArcGIS.Controls.IPageLayoutControlDefault m_PageLayoutControl = null;

        #region ISeanCommand成员
        public abstract System.Drawing.Bitmap Bitmap { get; }
        /// <summary>
        /// 分类
        /// </summary>
        public abstract string Category { get; }//必须设置分类信息

        public virtual bool Enabled { get { return true; } }//如有需要可以修改

        //public virtual bool Checked { get { return false; } }//command不需要check

        public abstract DevExpress.XtraBars.BarItem BindBarItem { get; set; }

        public virtual string Tooltip { get { return null; } }

        public virtual void Run()
        {
            this.m_Application.CurrentCommand = this;//设置当前的command
        }
        # endregion

        #region ISeanResource成员
        public abstract Guid UID { get; }//抽象成员一定要重实现

        public virtual string Name { get { return null; } }

        public abstract string Caption { get; }

        public virtual enumResourceType Type//virtual不一定重实现
        {
            get { return enumResourceType.Command; }
        }
        //每一个Command都需要设置该hook获得与当前的MapControl的联系
        public virtual void SetHook(ISeanApplication application)
        {
            if (application == null)
                return;
            this.m_Application = application;
            this.m_MapControl = application.GetIMapControl();
            this.m_PageLayoutControl = application.GetIPagelayoutControl();
        }
        #endregion
    }
}

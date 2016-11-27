using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.Framework
{
    /*
    Time: 18/11/2016 10:09 PM 周五
    Author: shenxin
    Description: 所有与AO相关的command
    Modify:
    */
    public abstract class SeanBaseTool : ESRI.ArcGIS.ADF.BaseClasses.BaseTool, ISeanCommand//这里只继承BaseTool，因为它实现了 BaseCommand, ESRI.ArcGIS.SystemUI.ICommand, ESRI.ArcGIS.SystemUI.ITool三者
    {
        protected ISeanApplication m_Application = null;
        protected ESRI.ArcGIS.Controls.IMapControlDefault m_MapControl = null;

        #region  Override BaseTool

        public override void OnCreate(object hook)
        {
            
        }

        #endregion
        
        # region ISeanCommand
        public new abstract System.Drawing.Bitmap Bitmap { get; }
        /// <summary>
        /// 分类
        /// </summary>
        public abstract override string Category { get; }//必须设置分类信息

        public new virtual bool Enabled { get { return true; } }//如有需要可以修改

        public new virtual bool Checked
        {
            get
            {
                if (this.m_Application.CurrentCommand == this)
                    return true;
                return false;
            }
        }

        public virtual void Run()
        {
            this.OnCreate(null);
            if (!this.Checked)
                this.m_Application.CurrentCommand = this;
            else
                this.m_Application.DefaultCommand.Run();//如果当前的command选中状态，那么设置当前的command为默认command
        }
        #endregion

        #region ISeanResource成员
        public abstract Guid UID { get; }//抽象成员一定要重实现

        public new virtual string Name { get { return null; } }

        public new abstract string Caption { get; }

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
        }
        #endregion
    }
}

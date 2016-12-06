using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.UserSetting
{
    using SeanShen.Framework;
    public class RestoreDefaultLayoutCommand:SeanBaseCommand
    {
        private System.Drawing.Bitmap m_Bitmap;
        private DevExpress.XtraBars.BarItem m_BindBarItem;
        /// <summary>
        /// 构造函数设置ui图片
        /// </summary>
        public RestoreDefaultLayoutCommand()
        {
            try
            {
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"images\UI\small_16x16\ZoomFullExtent16.png");
                this.m_Bitmap = new System.Drawing.Bitmap(path);
            }
            catch
            {
                this.m_Bitmap = null;
            }
        }
        #region ISeanCommand成员

        public override System.Drawing.Bitmap Bitmap
        {
            get { return this.m_Bitmap; }
        }
        /// <summary>
        /// 分类
        /// </summary>
        public override string Category { get { return "用户设置"; } }//必须设置分类信息

        public override DevExpress.XtraBars.BarItem BindBarItem { get { return this.m_BindBarItem; } set { this.m_BindBarItem = value; } }

        public override string Tooltip { get { return "恢复出厂的默认布局"; } }
        public override void Run()
        {
            base.Run();
            this.m_Application.LayoutManager.RestoreDefaultLayout();
        }
        # endregion


        #region ISeanResource成员
        public override Guid UID { get { return Guid.Parse("97E19791-C605-4159-839C-E9339F2F7489"); } }//抽象成员一定要重实现

        public override string Name { get { return "RestoreDefaultLayoutCommand"; } }

        public override string Caption
        {
            get { return "恢复出厂默认布局"; }
        }
        #endregion
    }
}

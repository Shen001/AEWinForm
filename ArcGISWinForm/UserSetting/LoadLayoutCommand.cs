using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.UserSetting
{
    using SeanShen.Framework;
    public class LoadLayoutCommand:SeanBaseCommand
    {
        private System.Drawing.Bitmap m_Bitmap;
        /// <summary>
        /// 构造函数设置ui图片
        /// </summary>
        public LoadLayoutCommand()
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

        public override string Tooltip { get { return "加载其他布局文件"; } }
        public override void Run()
        {
            base.Run();
            this.m_Application.LayoutManager.LoadLayout();
        }
        # endregion


        #region ISeanResource成员
        public override Guid UID { get { return Guid.Parse("B95488F8-2779-47FF-960E-91372A60A484"); } }//抽象成员一定要重实现

        public override string Name { get { return "LoadLayoutCommand"; } }

        public override string Caption
        {
            get { return "加载布局文件"; }
        }
        #endregion
    }
}

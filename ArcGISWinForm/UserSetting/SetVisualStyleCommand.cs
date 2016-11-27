using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.UserSetting
{
    using SeanShen.Framework;
    public class SetVisualStyleCommand:SeanBaseCommand
    {
        private System.Drawing.Bitmap m_Bitmap;
        /// <summary>
        /// 构造函数设置ui图片
        /// </summary>
        public SetVisualStyleCommand()
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

        public override string Tooltip { get { return "设置界面样式"; } }
        public override void Run()
        {
            base.Run();
            this.m_Application.LayoutManager.SetVisualStyle();
        }
        # endregion


        #region ISeanResource成员
        public override Guid UID { get { return Guid.Parse("51BE3B9F-29E0-42F5-9A64-0D9A05CC1A4E"); } }//抽象成员一定要重实现

        public override string Name { get { return "SetVisualStyleCommand"; } }

        public override string Caption
        {
            get { return "设置界面样式"; }
        }
        #endregion

    }
}

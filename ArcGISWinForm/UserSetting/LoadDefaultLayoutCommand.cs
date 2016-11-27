using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.UserSetting
{
    using SeanShen.Framework;
    public class LoadDefaultLayoutCommand:SeanBaseCommand
    {
        private System.Drawing.Bitmap m_Bitmap;
        /// <summary>
        /// 构造函数设置ui图片
        /// </summary>
        public LoadDefaultLayoutCommand()
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

        public override string Tooltip { get { return "加载默认布局"; } }
        public override void Run()
        {
            base.Run();
            this.m_Application.LayoutManager.LoadDefaultLayout();
        }
        # endregion


        #region ISeanResource成员
        public override Guid UID { get { return Guid.Parse("19F3B18C-F8D8-40A3-9C62-01B42E4D6B20"); } }//抽象成员一定要重实现

        public override string Name { get { return "LoadDefaultLayoutCommand"; } }

        public override string Caption
        {
            get { return "加载默认布局"; }
        }
        #endregion
    }
}

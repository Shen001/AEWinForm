using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.UserSetting
{

    /*
    Time: 26/11/2016 10:24  周六
    Author: shenxin
    Description: 保存为默认布局
    Modify:
 */
    using SeanShen.Framework;
    public class SaveDefaultLayoutCommand : SeanBaseCommand
    {
        private System.Drawing.Bitmap m_Bitmap;
        /// <summary>
        /// 构造函数设置ui图片
        /// </summary>
        public SaveDefaultLayoutCommand()
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

        public override string Tooltip { get { return "将当前布局保存到默认布局"; } }
        public override void Run()
        {
            base.Run();
            this.m_Application.LayoutManager.SaveAsDefaultLayout();
        }
        # endregion


        #region ISeanResource成员
        public override Guid UID { get { return Guid.Parse("429BC4C8-A698-4847-B263-6AA19F602B3B"); } }//抽象成员一定要重实现

        public override string Name { get { return "SaveDefaultLayoutCommand"; } }

        public override string Caption
        {
            get { return "保存为默认布局"; }
        }
        #endregion
    }
}

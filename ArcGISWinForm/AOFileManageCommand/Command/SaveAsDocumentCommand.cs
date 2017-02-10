using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.AOFileManageCommand
{
    /*******************************
    ** 作者： shenxin
    ** 时间： 2016/12/07,周三 16:41:49
    ** 版本:  V1.0.0
    ** CLR:	  4.0.30319.18408	
    ** GUID:  f2f713e9-d6e5-4f62-85eb-86df25e5749a
    ** 描述： 另存为文档
    *******************************/
    using System.Windows.Forms;
    using ESRI.ArcGIS.Carto;
    public class SaveAsDocumentCommand:SeanShen.Framework.SeanBaseCommand
    {
        private System.Drawing.Bitmap m_Bitmap;
        private DevExpress.XtraBars.BarItem m_BindBarItem;
        /// <summary>
        /// 构造函数设置ui图片
        /// </summary>
        public SaveAsDocumentCommand()
        {
            try
            {
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,Share.Consts.SMALLIMAGE_16 ,"SaveAsDoc.png");
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

        public override DevExpress.XtraBars.BarItem BindBarItem { get { return this.m_BindBarItem; } set { this.m_BindBarItem = value; } }

        /// <summary>
        /// 分类
        /// </summary>
        public override string Category { get { return "AO文件工具"; } }//必须设置分类信息

        public override string Tooltip { get { return "另存为文档"; } }
        public override void Run()
        {
            base.Run();
            SaveAsDoc();

            //ESRI.ArcGIS.SystemUI.ICommand command = new ESRI.ArcGIS.Controls.ControlsSaveAsDocCommand();
            //command.OnCreate(this.m_MapControl);
            //command.OnClick();
        }
        # endregion

        private void SaveAsDoc()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = ".mxd";
            saveDialog.Filter = "地图文档(*.mxd)|*.mxd";
            saveDialog.OverwritePrompt = true;
            saveDialog.RestoreDirectory = true;
            saveDialog.Title = "另存为文档";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                AEUtilities.MapHelper.SaveAsMxd(saveDialog.FileName,m_MapControl, true);
            }
        }

        #region ISeanResource成员
        public override Guid UID { get { return Guid.Parse("f2f713e9-d6e5-4f62-85eb-86df25e5749a"); } }//抽象成员一定要重实现

        public override string Name { get { return "SaveAsDocumentCommand"; } }

        public override string Caption
        {
            get { return "另存为文档"; }
        }
        #endregion
    }
}

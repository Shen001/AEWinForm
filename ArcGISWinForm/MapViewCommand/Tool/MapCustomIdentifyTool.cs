using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.MapViewCommand
{
    /*******************************
    ** 作者： shenxin
    ** 时间： 2016/12/12,周一 09:17:54
    ** 版本:  V1.0.0
    ** CLR:	  4.0.30319.18408	
    ** GUID:  0ea03a1c-e133-4843-bd34-29cae93cf4da
    ** 描述： 自定义Identify工具
    *******************************/
    using SeanShen.Framework;
    using SeanShen.MapViewCommand.UIForm;
    using ESRI.ArcGIS.Geometry;
    public class MapCustomIdentifyTool : SeanShen.Framework.SeanBaseTool
    {
        #region 私有成员
        private System.Drawing.Bitmap m_Bitmap;
        private DevExpress.XtraBars.BarItem m_BindBarItem;
        private CustomIdentifyToolDialog identifyDialog;
        # endregion

        #region 私有函数
        /// <summary>
        /// 显示窗体
        /// </summary>
        private void InitialIdentifyDialog()
        {
            //新建属性查询对象
            identifyDialog = CustomIdentifyToolDialog.CreateInstance(this.m_Application.GetAxMapControl());
            identifyDialog.Owner = this.m_Application.CurrentMainForm;
        }
        #endregion
        /// <summary>
        /// 构造函数设置ui图片
        /// </summary>
        public MapCustomIdentifyTool()
        {
            try
            {
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Share.Consts.SMALLIMAGE_16, "CustomIdentifyTool.png");
                this.m_Bitmap = new System.Drawing.Bitmap(path);
            }
            catch
            {
                this.m_Bitmap = null;
            }
        }

        #region ITool成员
        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="Button"></param>
        /// <param name="Shift"></param>
        /// <param name="X">X is the X coordinate, in device units, where the mouse button was released.</param>
        /// <param name="Y">Y is the Y coordinate, in device units, where the mouse button was released.</param>
        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            if (1 == Button)
            {
                //如果已经释放窗体
                if (identifyDialog.IsDisposed)
                {//显示窗体
                    InitialIdentifyDialog();
                }
                identifyDialog.OnMouseDown(Button, this.m_MapControl.ToMapPoint(X,Y).X, this.m_MapControl.ToMapPoint(X,Y).Y);
            }
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            if (identifyDialog == null || identifyDialog.IsDisposed)
                return;
            identifyDialog.Show();
            identifyDialog.OnMouseUp(this.m_MapControl.ToMapPoint(X, Y).X, this.m_MapControl.ToMapPoint(X, Y).Y);
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            if (identifyDialog == null || identifyDialog.IsDisposed)
                return;
            identifyDialog.OnMouseMove(this.m_MapControl.ToMapPoint(X, Y).X, this.m_MapControl.ToMapPoint(X, Y).Y);

        }
        #endregion




        #region ISeanCommand成员
        public override System.Drawing.Bitmap Bitmap { get { return this.m_Bitmap; } }
        /// <summary>
        /// 分类
        /// </summary>
        public override string Category { get { return "地图视图工具"; } }//必须设置分类信息

        public override string Tooltip { get { return "自定义地图对象识别"; } }

        public override DevExpress.XtraBars.BarItem BindBarItem { get { return this.m_BindBarItem; } set { this.m_BindBarItem = value; } }

        public override void Run()
        {
            base.Run();
            InitialIdentifyDialog();
            identifyDialog.Show();
        }
        # endregion


        #region ISeanResource成员
        public override Guid UID { get { return Guid.Parse("0ea03a1c-e133-4843-bd34-29cae93cf4da"); } }//抽象成员一定要重实现

        public override string Name { get { return "MapCustomIdentifyTool"; } }

        public override string Caption
        {
            get { return "自定义地图对象识别"; }
        }
        #endregion
    }
}

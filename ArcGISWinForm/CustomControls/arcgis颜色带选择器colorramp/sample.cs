/*
Time: 17/11/2016 14:14 PM 周四
Author: shenxin
Description:
Modify:
*/
			
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.CustomControls.arcgis颜色带选择器colorramp
{
    /// <summary>
    /// 该类只是基本的调用方法，并不能直接使用,主要的使用控件方法的例子在UseControl方法中
    /// </summary>
    internal class sample
    {
        private SeanShen.CustomControls.ucColorRampSelector ucColorRampSelector1;
        public sample()
        {
            initialcomponent();
        }
        void initialcomponent()
        {
            this.ucColorRampSelector1 = new SeanShen.CustomControls.ucColorRampSelector();
        }

        void UseControl()
        {
            if (this.ucColorRampSelector1.Custom_ColorRampCBB != null)
            {
                ESRI.ArcGIS.Display.IStyleGalleryItem item = this.ucColorRampSelector1.Custom_ColorRampCBB.SelectSymbol;//获取当前的选择的样式item（也可设置）

                ESRI.ArcGIS.Display.IStyleGalleryItem item1 = this.ucColorRampSelector1.Custom_ColorRampCBB.GetStyleItemByName("itemname");//根据名称获取item
            }
        }
    }
}

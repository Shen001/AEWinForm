/* ============================================================
* Filename:		Custom_ColorRampCombobox
* Created:		30/01/2016 12:09 AM
* MachineName:  
* Author:		Shenxin
* Description:  自定义颜色颜色带选择combobox
* Modify:       
* ============================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
namespace SeanShen.CustomControls
{
    public class Custom_ColorRampCombobox : ComboBox//内部组件则不会出现在其他的命名空间中
    {                                               //命名空间下元素不能显示申明为private、protected与protected internal
        public Custom_ColorRampCombobox()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            this.MaxDropDownItems = 10;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            try
            {
                Image image = (Image)this.Items[e.Index];
                Rectangle rec = e.Bounds;
                rec.Height -= 2;
                rec.Width -= 2;
                rec.X += 1;
                rec.Y += 1;
                e.Graphics.DrawImage(image, rec);
            }
            catch
            {
                if (e.Index != -1)
                {
                    //不是图片则显示字符
                    e.Graphics.DrawString(Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds.X, e.Bounds.Y);
                }
            }
            finally
            {
                base.OnDrawItem(e);
            }
        }

        private ISymbologyStyleClass m_SymbologyStyleClass = null;
        private List<IStyleGalleryItem> m_SymbolArray = new List<IStyleGalleryItem>();

        public ISymbologyStyleClass SymbologyStyleClass
        {
            get
            {
                return m_SymbologyStyleClass;
            }
            set
            {
                m_SymbologyStyleClass = value;

                if (m_SymbologyStyleClass != null)
                {
                    this.m_SymbolArray.Clear();
                    this.BeginUpdate();
                    this.Items.Clear();
                    for (int i = 0; i < m_SymbologyStyleClass.ItemCount; i++)
                    {
                        IStyleGalleryItem item = m_SymbologyStyleClass.GetItem(i);
                        stdole.IPictureDisp picture = m_SymbologyStyleClass.PreviewItem(item, this.Width, this.Height);
                        Image image = Image.FromHbitmap(new IntPtr(picture.Handle));
                        image = new Bitmap(image, new Size(image.Width - 10, image.Height));
                        this.Items.Add(image);
                        this.m_SymbolArray.Add(item);
                    }
                    this.EndUpdate();
                }
            }
        }
        //根据IStyleGalleryItem的Name返回指定的item
        public  IStyleGalleryItem GetStyleItemByName(string name)
        {
            return m_SymbolArray.FirstOrDefault(o => o.Name == name);
        }
        //选择的样式
        public IStyleGalleryItem SelectSymbol
        {
            get
            {
                if (this.SelectedIndex >= 0 && m_SymbolArray.Count > this.SelectedIndex)
                    return m_SymbolArray[this.SelectedIndex];
                else
                    return null;
            }
            set
            {
                int index = m_SymbolArray.IndexOf(value);
                if (index >= 0 && m_SymbolArray.Count > index)
                    this.SelectedIndex = index;
            }
        }
    }
}

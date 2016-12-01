using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using DevExpress.XtraBars;

namespace SeanShen.ArcGISWinForm
{
        
/*
    Time: 26/11/2016 12:02  周六
    Author: shenxin
    Description: 用来实现MainForm需要实现的接口
    Modify:
*/
    public partial class MainForm
    {        
        #region ISeanLayoutManager
        //可以自定义的默认布局
        private string Default_LayoutPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"layout\DefaultLayout.xml");
        //出厂设置的默认布局
        private string Restore_Default_LayoutPath=System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory+@"layout\RestoreDefaultLayout.xml");

        //保存为当前的默认布局——程序初始化时自动加载默认布局
        public void SaveAsDefaultLayout()
        {
            if (System.IO.File.Exists(Default_LayoutPath))
            {

                this.barManager1.SaveLayoutToXml(Default_LayoutPath);
            }
        }

        //恢复最原始的默认布局——该文件不会被写入
        public void RestoreDefaultLayout()
        {
            if (System.IO.File.Exists(Restore_Default_LayoutPath))
            {

                this.barManager1.RestoreLayoutFromXml(Restore_Default_LayoutPath);

            }
            ResetBar();
        }

        //加载默认布局
        public void LoadDefaultLayout()
        {
            if (System.IO.File.Exists(Default_LayoutPath))
            {

                this.barManager1.RestoreLayoutFromXml(Default_LayoutPath);
            }
        }
        //加载指定布局视图
        public void LoadLayout()
        {
            System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();
            openFile.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            openFile.Filter = "XML文件(*.xml)|*.xml";
            openFile.FilterIndex = 2;
            openFile.RestoreDirectory = true;
            openFile.Multiselect=false;
            openFile.DefaultExt = "*.xml";
            openFile.Title = "加载布局";
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.barManager1.RestoreLayoutFromXml(openFile.FileName);
            }
            ResetBar();
        }
        //另存为当前布局视图
        public void SaveAsCurrentLayout()
        {
            System.Windows.Forms.SaveFileDialog saveFile = new System.Windows.Forms.SaveFileDialog();
            saveFile.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            saveFile.Filter = "XML文件(*.xml)|*.xml";
            saveFile.FilterIndex = 2;
            saveFile.Title = "另存为当前布局";
            saveFile.OverwritePrompt = true;
            if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.barManager1.SaveLayoutToXml(saveFile.FileName);
            }
        }
        //设置界面样式
        public void SetVisualStyle()
        {

        }
        #endregion
        
        #region 私有方法（只与界面操作有关）
        //更新baritem关联的图片
        private void UpdateBarItemLinkImage(BarItemLink itemLink)
        {
            itemLink.ImageIndex = itemLink.Item.ImageIndex;
            if (itemLink.Item is BarSubItem)
            {
                BarSubItem subItem = itemLink.Item as BarSubItem;
                foreach (BarItemLink item in subItem.ItemLinks)
                {
                    UpdateBarItemLinkImage(item);
                }
            }
        }
        /// <summary>
        /// 重置bar
        /// </summary>
        private void ResetBar()
        {
            return;
            //允许对默认的bar进行修改和删除
            foreach (Bar bar in this.barManager1.Bars)
            {
                bar.BeginUpdate();
                for (int i = 0; i < bar.ItemLinks.Count; i++)
                {
                    UpdateBarItemLinkImage(bar.ItemLinks[i]);
                }
                bar.OptionsBar.UseWholeRow = false;
                bar.OptionsBar.AllowRename = true;
                bar.OptionsBar.AllowDelete = true;
                bar.EndUpdate();
            }
        }
        #endregion
    }
}

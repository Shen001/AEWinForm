using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.CustomControls.每个节点是否有checkbox的treeview
{
    /// <summary>
    /// 该类只是基本的调用方法，并不能直接使用,主要的使用控件方法的例子在UseControl方法中
    /// </summary>
    internal class sample
    {
        private SeanShen.CustomControls.Custom_MixedCheckBoxesTreeView mixedCheckBoxesTreeView1;

        public sample()
        {
            initialcomponent();

            UseControl();
        }

        private void UseControl()
        {
            this.mixedCheckBoxesTreeView1.CheckBoxes = true;//treeview属性，设置为全部显示checkbox

            Custom_HiddenCheckBoxTreeNode hideNode = new Custom_HiddenCheckBoxTreeNode("隐藏checkbox");
            this.mixedCheckBoxesTreeView1.Nodes.Add(hideNode);
            System.Windows.Forms.TreeNode node = new System.Windows.Forms.TreeNode("不隐藏checkbox");
            this.mixedCheckBoxesTreeView1.Nodes[0].Nodes.Add(node);
        }


        void initialcomponent()
        {
            this.mixedCheckBoxesTreeView1 = new SeanShen.CustomControls.Custom_MixedCheckBoxesTreeView();

            this.mixedCheckBoxesTreeView1.Location = new System.Drawing.Point(542, 88);
            this.mixedCheckBoxesTreeView1.Name = "mixedCheckBoxesTreeView1";
            this.mixedCheckBoxesTreeView1.Size = new System.Drawing.Size(121, 97);
            this.mixedCheckBoxesTreeView1.TabIndex = 6;
        }
    }
}

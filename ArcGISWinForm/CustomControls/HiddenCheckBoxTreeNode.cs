using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;

namespace SeanShen.CustomControls
{
    public class HiddenCheckBoxTreeNode : TreeNode
    {
        internal bool CheckboxHidden { set; get; }

        public HiddenCheckBoxTreeNode() { }
        public HiddenCheckBoxTreeNode(string text) : base(text) { }
        public HiddenCheckBoxTreeNode(string text, TreeNode[] children) : base(text, children) { }
        public HiddenCheckBoxTreeNode(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex) { }
        public HiddenCheckBoxTreeNode(string text, int imageIndex, int selectedImageIndex, TreeNode[] children) : base(text, imageIndex, selectedImageIndex, children) { }
        protected HiddenCheckBoxTreeNode(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context) { }
    }
}

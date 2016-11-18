using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;

namespace SeanShen.CustomControls
{
    public class Custom_HiddenCheckBoxTreeNode : TreeNode
    {
        internal bool CheckboxHidden { set; get; }

        public Custom_HiddenCheckBoxTreeNode() { }
        public Custom_HiddenCheckBoxTreeNode(string text) : base(text) { }
        public Custom_HiddenCheckBoxTreeNode(string text, TreeNode[] children) : base(text, children) { }
        public Custom_HiddenCheckBoxTreeNode(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex) { }
        public Custom_HiddenCheckBoxTreeNode(string text, int imageIndex, int selectedImageIndex, TreeNode[] children) : base(text, imageIndex, selectedImageIndex, children) { }
        protected Custom_HiddenCheckBoxTreeNode(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context) { }
    }
}

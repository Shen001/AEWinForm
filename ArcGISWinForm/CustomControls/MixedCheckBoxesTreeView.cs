using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;

namespace SeanShen.CustomControls
{
    public class MixedCheckBoxesTreeView : TreeView
    {
        /// <summary>
        /// Specifies or receives attributes of a node
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct TV_ITEM
        {
            public int Mask;
            public IntPtr ItemHandle;
            public int State;
            public int StateMask;
            public IntPtr TextPtr;
            public int TextMax;
            public int Image;
            public int SelectedImage;
            public int Children;
            public IntPtr LParam;
        }

        public const int TVIF_STATE = 0x8;
        public const int TVIS_STATEIMAGEMASK = 0xF000;

        public const int TVM_SETITEMA = 0x110d;
        public const int TVM_SETITEM = 0x110d;
        public const int TVM_SETITEMW = 0x113f;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref TV_ITEM lParam);

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // trap TVM_SETITEM message
            if (m.Msg == TVM_SETITEM || m.Msg == TVM_SETITEMA || m.Msg == TVM_SETITEMW)
                // check if CheckBoxes are turned on
                if (CheckBoxes)
                {
                    // get information about the node
                    TV_ITEM tv_item = (TV_ITEM)m.GetLParam(typeof(TV_ITEM));
                    HideCheckBox(tv_item);
                }
        }

        protected void HideCheckBox(TV_ITEM tv_item)
        {
            if (tv_item.ItemHandle != IntPtr.Zero)
            {
                // get TreeNode-object, that corresponds to TV_ITEM-object
                TreeNode currentTN = TreeNode.FromHandle(this, tv_item.ItemHandle);

                HiddenCheckBoxTreeNode hiddenCheckBoxTreeNode = currentTN as HiddenCheckBoxTreeNode;
                // check if it's HiddenCheckBoxTreeNode and
                // if its checkbox already has been hidden
                if (hiddenCheckBoxTreeNode != null && !hiddenCheckBoxTreeNode.CheckboxHidden)
                {
                    // to evade repeat hiding, we set CheckboxHidden to  true
                    hiddenCheckBoxTreeNode.CheckboxHidden = true;

                    // specify attributes to update
                    TV_ITEM updatedTvItem = new TV_ITEM();
                    updatedTvItem.ItemHandle = tv_item.ItemHandle;
                    updatedTvItem.Mask = TVIF_STATE;
                    updatedTvItem.StateMask = TVIS_STATEIMAGEMASK;
                    updatedTvItem.State = 0;

                    // send TVM_SETITEM message
                    SendMessage(new HandleRef(this, Handle), TVM_SETITEM, 0, ref updatedTvItem);
                }
            }
        }

        protected override void OnBeforeCheck(TreeViewCancelEventArgs e)
        {
            base.OnBeforeCheck(e);
            // prevent checking/unchecking of HiddenCheckBoxTreeNode,
            // otherwise, we will have to repeat checkbox hiding
            if (e.Node is HiddenCheckBoxTreeNode)
                e.Cancel = true;
        }
    }
}

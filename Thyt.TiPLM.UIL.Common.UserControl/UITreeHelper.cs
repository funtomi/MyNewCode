namespace Thyt.TiPLM.UIL.Common.UserControl
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public sealed class UITreeHelper
    {
        public static UITreeHelper Instance = new UITreeHelper();
        private static int nIndex = 0;

        public string GetLocationPath(TreeView tree)
        {
            string str = "";
            List<string> listText = new List<string>();
            TreeNode selectedNode = tree.SelectedNode;
            if (selectedNode == null)
            {
                return "";
            }
            listText.Add(selectedNode.Text);
            this.GetParentText(selectedNode, listText);
            for (int i = listText.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    str = str + listText[i];
                }
                else
                {
                    str = str + listText[i] + "###";
                }
            }
            return str;
        }

        private void GetParentText(TreeNode node, List<string> ListText)
        {
            TreeNode parent = node.Parent;
            if (parent != null)
            {
                ListText.Add(parent.Text);
                this.GetParentText(parent, ListText);
            }
        }

        private void SetLocation(TreeView tree, TreeNodeCollection collection, string[] PathArray)
        {
            if ((collection != null) && (collection.Count > 0))
            {
                foreach (TreeNode node in collection)
                {
                    if (node.Text == PathArray[nIndex])
                    {
                        node.Expand();
                        if (PathArray.Length == (nIndex + 1))
                        {
                            tree.SelectedNode = node;
                        }
                        else
                        {
                            nIndex++;
                            this.SetLocation(tree, node.Nodes, PathArray);
                        }
                        break;
                    }
                }
            }
        }

        public void SetLocationPath(TreeView tree, string LocationPath)
        {
            string[] pathArray = Regex.Split(LocationPath, "###");
            TreeNodeCollection collection = tree.Nodes;
            nIndex = 0;
            this.SetLocation(tree, collection, pathArray);
        }
    }
}


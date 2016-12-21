using System;
using System.Windows.Forms;
using Microsoft.SharePoint;

namespace SharePoint_Hierachie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tvStructure.Nodes.Clear();
            var col = new SPSite(txtURL.Text);
            SPWeb root = col.RootWeb;

            var rootNode = new TreeNode(root.Title, 0, 0);
            rootNode.Tag = root;
            tvStructure.Nodes.Add(rootNode);

            AddChilds(root, rootNode);
            AddLists(root, rootNode);

            tvStructure.ExpandAll();
        }

        private void AddChilds(SPWeb web, TreeNode parent)
        {
            TreeNode child = null;
            foreach (SPWeb w in web.Webs)
            {
                child = new TreeNode(w.Title, 0, 0);
                child.Tag = w;
                parent.Nodes.Add(child);

                AddChilds(w, child);
                AddLists(w, child);
            }
        }

        private void AddLists(SPWeb web, TreeNode parent)
        {
            TreeNode listnode = null;
            foreach (SPList list in web.Lists)
            {
                listnode = new TreeNode(list.Title, 1, 1);
                listnode.Tag = list;

                parent.Nodes.Add(listnode);
            }
        }

        private void NodeSelected(object sender, TreeViewEventArgs e)
        {
            object item = tvStructure.SelectedNode.Tag;

            cbViews.Items.Clear();

            if (item is SPList)
            {
                foreach (SPView v in ((SPList) item).Views)
                {
                    cbViews.Items.Add(v);
                }

                if (((SPList) item).Views.Count > 0)
                {
                    cbViews.SelectedIndex = 0;
                }
            }
        }

        private void GetCAML(object sender, EventArgs e)
        {
            if (cbViews.SelectedItem != null)
            {
                Clipboard.SetText(((SPView) cbViews.SelectedItem).Query);
            }
        }
    }
}
using System;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace CopyTo2010.Layouts.CopyTo2010
{
    public partial class CopyTo : LayoutsPageBase
    {
        protected void PopulateTree(object sender, EventArgs e)
        {
            tvSite.Nodes.Clear();
            SPSite col = new SPSite(txtSiteCol.Text);

            TreeNode rootNode = new TreeNode(col.RootWeb.Title,
                                             new TreeNodeContext(txtSiteCol.Text, col.RootWeb.ID).ToString());
            AddVisibleLists(rootNode, col.RootWeb);
            AddChildWebs(rootNode, col.RootWeb);
            tvSite.Nodes.Add(rootNode);
        }

        protected void AddChildWebs(TreeNode pn, SPWeb web)
        {
            foreach (SPWeb chw in web.GetSubwebsForCurrentUser())
            {
                TreeNode chn = new TreeNode(chw.Title, new TreeNodeContext(txtSiteCol.Text, chw.ID).ToString());
                AddChildWebs(chn, chw);
                AddVisibleLists(chn, chw);
                pn.ChildNodes.Add(chn);
            }
        }

        protected void AddVisibleLists(TreeNode pn, SPWeb web)
        {
            foreach (SPList list in web.GetListsOfType(SPBaseType.DocumentLibrary))
            {
                if (list.OnQuickLaunch)
                {
                    TreeNode chn = new TreeNode(list.Title,
                                                new TreeNodeContext(txtSiteCol.Text, web.ID, list.ID).ToString());
                    pn.ChildNodes.Add(chn);
                }
            }
        }

        protected void NodeSelected(object sender, EventArgs e)
        {
            TreeView tv = (TreeView)sender;
            TreeNodeContext ctx = new TreeNodeContext(tv.SelectedValue);

            SPSite col = new SPSite(ctx.SiteCollection);
            SPWeb sw = col.AllWebs[ctx.WebID];

            if (ctx.IsList)
            {
                SPList sl = sw.Lists[ctx.ListID];
                lblList.Text = sl.Title;
            }
            else
            {
                lblList.Text = string.Empty;
            }
        }

        protected void CopyItem(object sender, EventArgs e)
        {
            int itemid = Convert.ToInt32(Request.QueryString["ItemId"]);
            Guid listid = new Guid(Request.QueryString["ListId"]);

            SPWeb sourceweb = SPControl.GetContextWeb(HttpContext.Current);
            SPList sourcelist = sourceweb.Lists[listid];
            SPListItem sourceitem = sourcelist.GetItemById(itemid);

            lblListItem.Text = sourceitem.Name;

            TreeNodeContext ctx = new TreeNodeContext(tvSite.SelectedValue);

            SPSite destcol = new SPSite(ctx.SiteCollection);
            SPWeb destweb = destcol.AllWebs[ctx.WebID];
            if (ctx.IsList)
            {
                SPList destlist = destweb.Lists[ctx.ListID];
                SPListItem destitem = destlist.Items.Add();

                SPFolder folder = destlist.RootFolder;

                try
                {
                    folder.Files.Add(sourceitem.Name, sourceitem.File.OpenBinary());
                }
                catch (Exception ex)
                {
                    lblStatus.Text = string.Format("Error uploading File {0}", sourceitem.Name);
                }

                lblStatus.Text = string.Format("{0} copied successfully to {1}", sourceitem.Name, destlist.Title);
            }
        }
    }
}

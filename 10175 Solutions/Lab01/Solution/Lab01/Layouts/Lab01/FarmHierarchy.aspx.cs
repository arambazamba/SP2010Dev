using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
//4. Add using statements:
using System.Web.UI.WebControls;
using Microsoft.SharePoint.Administration;

namespace Lab01.Layouts.Lab01
{
    public partial class FarmHierarchy : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Get reference to local farm
            SPFarm thisFarm = SPFarm.Local;
            TreeNode node;
            //Clear existing nodes in TreeView
            farmHierarchyViewer.Nodes.Clear();
            //Enumerate each service in the local farm
            foreach (SPService svc in thisFarm.Services)
            {
                //Add info about each service to the tree
                node = new TreeNode();
                node.Text = "Farm Service (Type=" + svc.TypeName + "; Status=" + svc.Status + ")";
                farmHierarchyViewer.Nodes.Add(node);
                TreeNode svcNode = node;
                //See of service is type of SPWebService, so we will know whether to start enumerating WebApps
                if (svc is SPWebService)
                {
                    //Cast service to SPWebService
                    SPWebService webSvc = (SPWebService)svc;
                    //Enumerate WebApps and add them to the tree
                    foreach (SPWebApplication webApp in webSvc.WebApplications)
                    {
                        node = new TreeNode();
                        node.Text = webApp.DisplayName;
                        svcNode.ChildNodes.Add(node);
                        TreeNode webAppNode = node;
                        //Only show site collections for WebApps that are NOT the central admin app
                        if (!webApp.IsAdministrationWebApplication)
                        {
                            //Enumerate site collections and add theem to the tree
                            foreach (SPSite site in webApp.Sites)
                            {
                                //Ensure we catch our own exceptions
                                //We will not add nodes to the tree if the user does not have permissions, 
                                //so we are effectively security trimming
                                site.CatchAccessDeniedException = false;
                                try
                                {
                                   //Add a node for the site collection
                                    node = new TreeNode();
                                    node.Text = site.Url;
                                    webAppNode.ChildNodes.Add(node);
                                    //Add a node for the RootWeb SPWeb.
                                    //Set the navigateURL to our custom app page that accepts querystrings for type and object ID
                                    // For SPWebs, we pass in the value 'web' as the first querystring
                                    TreeNode siteNode = node;
                                    node = new TreeNode(site.RootWeb.Title, null, null,
                                        site.RootWeb.Url + "/_layouts/lab01/PropertyChanger.aspx?type=web&objectID=" + site.RootWeb.ID,
                                        "_self");
                                    siteNode.ChildNodes.Add(node);
                                    TreeNode parentNode = node;
                                    //Enumerate and add a node for each list in the RootWeb.
                                    //Set the navigateURL to our custom app page that accepts querystrings for type and object ID
                                    // For SPLists, we pass in the value 'list' as the first querystring
                                    foreach (SPList list in site.RootWeb.Lists)
                                    {
                                        node = new TreeNode(list.Title, null, null,
                                            site.RootWeb.Url + "/_layouts/lab01/PropertyChanger.aspx?type=list&objectID=" + list.ID,
                                            "_self");
                                        parentNode.ChildNodes.Add(node);
                                    }
                                    //enumerate all 1st-level Webs, and call our own functions of addWebs
                                    //NOTE: addWebs is a recursive functions, so it will effectively walk thr tree 
                                    //for each nested SPWeb for each of these 1st-level ones
                                    foreach (SPWeb childWeb in site.RootWeb.Webs)
                                    {
                                        try
                                        {
                                            addWebs(childWeb, parentNode);
                                        }
                                        finally
                                        {
                                            //Ensure proper disposal of these webs, since we instantiated them and they would not be used by any other component
                                            childWeb.Dispose();
                                        }
                                    }
                                    //Turn SharePoint access denied handlers back on for each site now that we've finished with it
                                    site.CatchAccessDeniedException = false;
                                }
                                finally
                                {
                                    //Ensure proper disposal of each SPSite, since we instantiated them and they would not be used by any other component
                                    site.Dispose();
                                }
                            }
                        }
                    }
                }
            }
            //Show the tree structure expanded by default
            farmHierarchyViewer.ExpandAll();
        }
        //Our recursive function. This performs the same sort of operation as the main body of code, but calls itself for each subweb, 
        //so we will effectively 'walk the hieraerchy' until there are no more subwebs in each branch
        void addWebs(SPWeb web, TreeNode parentNode)
        {
            TreeNode node;
            node = new TreeNode(web.Title, null, null,
                web.Url + "/_layouts/lab01/PropertyChanger.aspx?type=web&objectID=" + web.ID, 
                "_self");
            
            parentNode.ChildNodes.Add(node);
            parentNode = node;
            foreach (SPList list in web.Lists)
            {
                node = new TreeNode(list.Title, null, null, 
                    web.Url + "/_layouts/lab01/PropertyChanger.aspx?type=list&objectID=" + list.ID, 
                    "_self");
                parentNode.ChildNodes.Add(node);
            }
            foreach (SPWeb childWeb in web.Webs)
            {
                try
                {
                    addWebs(childWeb, parentNode);
                }
                finally
                {
                    //Ensure proper disposal of these webs, since we instantiated them and they would not be used by any other component
                    childWeb.Dispose();
                }
            }
        }
    }
}

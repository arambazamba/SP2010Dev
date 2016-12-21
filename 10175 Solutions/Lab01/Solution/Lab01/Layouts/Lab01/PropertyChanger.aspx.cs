using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace Lab01.Layouts.Lab01
{
    public partial class PropertyChanger : LayoutsPageBase
    {
        //Add class vars
        SPWeb thisWeb = null;
        SPList thisList = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Wire up event handlers
            webTitleUpdate.Click += new EventHandler(webTitleUpdate_Click);
            listPropertiesUpdate.Click += new EventHandler(listPropertiesUpdate_Click);
            webCancel.Click += new EventHandler(allCancel_Click);
            listCancel.Click += new EventHandler(allCancel_Click);

            try
            {
                string objectType = string.Empty;
                string objectID = string.Empty;
                string objectUrl = string.Empty;
               //This page gets loaded from hyperlinks in the treeview control on FarmHierarchy.aspx in this project
                //Ensure both 'type' and 'objectid' are passed in through the querystring
                if (this.Page.Request["type"] != null)
                {
                    objectType = this.Page.Request["type"].ToString();
                }
                else
                {
                    objectName.Text = "Malformed URL";
                    listProperties.Visible = false;
                    webProperties.Visible = false;
                    return;
                }
                if (this.Page.Request["objectID"] != null)
                {
                    objectID = this.Page.Request["objectID"].ToString();
                }
                else
                {
                    objectName.Text = "Malformed URL";
                    listProperties.Visible = false;
                    webProperties.Visible = false;
                    return;
                }
                //At this point, we know we got QueryString values for type and object ID
                if (objectType == "web")
                {
                    //Show an populate controls in the web panel and hide the list panel
                    listProperties.Visible = false;
                    webProperties.Visible = true;
                    SPSite thisSite = SPContext.Current.Site;
                    //Web Guid got passed in through querystring, so use it here
                    thisWeb = thisSite.OpenWeb(new Guid(objectID));
                    objectName.Text = "Web: " + thisWeb.Title;
                    if (!Page.IsPostBack)
                    {
                        //Put the Web title in the box so user's can see what it currently is
                        webTitle.Text = thisWeb.Title;
                        //Dispose of thisWeb, since we opened it ourselves, 
                        //but do NOT dispose of thisSite, since it's the global current context site
                        thisWeb.Dispose();
                    }
                }
                if (objectType == "list")
                {
                    //Show an populate controls in the list panel and hide the web panel
                    webProperties.Visible = false;
                    listProperties.Visible = true;
                    thisWeb = SPContext.Current.Web;
                    //List Guid got passed in through querystring, so use it here
                    thisList = thisWeb.Lists[new Guid(objectID)];
                    objectName.Text = "List: " + thisList.Title;
                    if (!Page.IsPostBack)
                    {
                        //Refelect the current properties of the list in the checkboxes
                        listVersioning.Checked = thisList.EnableVersioning;
                        listContentTypes.Checked = thisList.ContentTypesEnabled;
                        //Do NOT dispose of thisWeb, since it's now the global current context Web
                    }
                }
            }
            catch (Exception ex)
            {
                objectName.Text = ex.Message;
            }
        }
        //13. Update Web and return to the farmhierarchy page
        void webTitleUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                thisWeb.AllowUnsafeUpdates = true;
                thisWeb.Title = webTitle.Text;
                thisWeb.Update();
                thisWeb.AllowUnsafeUpdates = false;
                this.Page.Response.Redirect(thisWeb.Url + "/_layouts/lab01/FarmHierarchy.aspx");
            }
            catch(Exception ex)
            {
                objectName.Text = ex.Message;
                listProperties.Visible = false;
                webProperties.Visible = false;
            }
        }
        //Update List and return to the farmhierarchy page
        void listPropertiesUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                thisWeb.AllowUnsafeUpdates = true;
                thisList.EnableVersioning = listVersioning.Checked;
                thisList.ContentTypesEnabled = listContentTypes.Checked;
                thisList.Update();
                thisWeb.AllowUnsafeUpdates = false;
                this.Page.Response.Redirect(thisWeb.Url + "/_layouts/lab01/FarmHierarchy.aspx");
            }
            catch (Exception ex)
            {
                objectName.Text = ex.Message;
                listProperties.Visible = false;
                webProperties.Visible = false;
            }
        }
        //Deal with cancel
        void allCancel_Click(object sender, EventArgs e)
        {
            this.Page.Response.Redirect(thisWeb.Url + "/_layouts/lab01/FarmHierarchy.aspx");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace Integrations.Layouts.DocumentLibraryExtensions
{
    public partial class AllowedFileTypes : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack==false && Request.QueryString["ListId"] != null &&
                SPContext.Current.Web.Properties["aft_" + new Guid(Request.QueryString["ListId"])] != null)
            {
                string allowed = SPContext.Current.Web.Properties["aft_" + new Guid(Request.QueryString["ListId"])];
                txtFiletypes.Text = allowed;
            }
        }

        protected void SaveSettings(object sender, EventArgs e)
        {
            string id = string.Empty;
            string val = string.IsNullOrEmpty(txtFiletypes.Text) ? string.Empty : txtFiletypes.Text;
            
            if (Request.QueryString["ListId"]!=null)
            {
                id = "aft_" + new Guid( Request.QueryString["ListId"]);
                if (SPContext.Current.Web.Properties.ContainsKey(id))
                {
                    SPContext.Current.Web.Properties.Remove(id);
                }
                SPContext.Current.Web.Properties.Add(id, val);
                SPContext.Current.Web.Properties.Update();
            }
        }
    }
}

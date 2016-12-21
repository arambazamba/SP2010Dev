using System;
using System.Web;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace Integrations.DocumentLibraryExtensions.Layouts.DocumentLibraryExtensions
{
    public partial class UploadError : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack==false)
            {
                if (Request.QueryString["errormsg"]!=null)
                {
                    lblMessage.Text = Request.QueryString["errormsg"];
                }

                if (Request.QueryString["returnurl"] != null)
                {
                    //hlReturn.NavigateUrl = Request.QueryString["returnurl"];
                }
            }
        }
    }
}

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;

namespace DemoWebParts.VisualWebPart
{
    public partial class VisualWebPartUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack==false)
            {
                SPWeb web = SPContext.Current.Web;
                try
                {
                    SPList list = web.Lists["Customers"];
                    if (list != null)
                    {
                        gvCustomers.DataSource = list.Items.GetDataTable();
                        gvCustomers.DataBind();
                    }

                }
                catch (Exception)
                {
                }
            }
        }
    }
}

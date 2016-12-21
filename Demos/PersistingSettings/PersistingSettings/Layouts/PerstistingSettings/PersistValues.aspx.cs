using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace Demo.Layouts.Demo
{
    public partial class PersistValues : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PMain.Controls.Add(Page.LoadControl("~/_Controltemplates/PerstistingSettings/PersistValues.ascx"));

        }
    }
}

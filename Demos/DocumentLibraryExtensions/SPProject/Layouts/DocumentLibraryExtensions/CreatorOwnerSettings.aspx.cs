using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace Integrations.Layouts.DocumentLibraryExtensions
{
    public partial class CreatorOwnerSettings : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pMain.Controls.Add(Page.LoadControl("~/_Controltemplates/DocumentLibraryExtensions/CreatorOwnerSaveSettingsDialog.ascx"));
        }
    }
}

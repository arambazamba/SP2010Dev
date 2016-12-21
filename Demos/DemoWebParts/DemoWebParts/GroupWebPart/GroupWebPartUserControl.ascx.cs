using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using DemoWebParts.GroupMembershipWebpart;

namespace DemoWebParts.GroupWebPart
{
    public partial class GroupWebPartUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            odsUsers.SelectMethod = "GetUserForSiteGroups";
            odsUsers.TypeName = typeof(UserInfo).AssemblyQualifiedName;
        }
    }
}

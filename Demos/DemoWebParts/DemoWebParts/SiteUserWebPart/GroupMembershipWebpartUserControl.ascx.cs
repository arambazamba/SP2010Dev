using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using DemoWebParts.GroupMembershipWebpart;

namespace DemoWebParts.GroupMembershipWebpart
{
    public partial class GroupMembershipWebpartUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            odsUsers.TypeName = typeof(UserInfo).AssemblyQualifiedName;
        }
    }
}

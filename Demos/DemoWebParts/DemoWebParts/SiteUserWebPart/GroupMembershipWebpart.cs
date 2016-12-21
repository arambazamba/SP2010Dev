using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;

namespace DemoWebParts.GroupMembershipWebpart
{
    [ToolboxItemAttribute(false)]
    public class GroupMembershipWebpart : WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/GroupMembershipWebpart/GroupMembershipWebpartUserControl.ascx";

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            Controls.Add(control);
        }
    }
}

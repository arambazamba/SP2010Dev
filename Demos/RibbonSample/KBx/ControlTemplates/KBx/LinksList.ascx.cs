using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace KBx.ControlTemplates.KBx
{
    public partial class LinksList : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var Links = new List<FollowedLink>
                                           {
                                               new FollowedLink
                                                   {
                                                       Owner = "Hans Huber",
                                                       URL = "http://localhost/abteilung1/test/pages/",
                                                       LastAccess = DateTime.Now.AddDays(-2).ToShortDateString(),
                                                       Type = "/_layouts/kbx/images/document.png",
                                                       ToolTip = "Document",
                                                       Reads = 56
                                                   },
                                               new FollowedLink
                                                   {
                                                       Owner = "Sebine Klein",
                                                       URL = "http://localhost/team3/",
                                                       LastAccess = DateTime.Now.AddDays(-15).ToShortDateString(),
                                                       Type = "/_layouts/kbx/images/menu.png",
                                                       ToolTip = "Quick Launch",
                                                       Reads = 101
                                                   },
                                               new FollowedLink
                                                   {
                                                       Owner = "Clementine Clebhuber",
                                                       URL = "http://localhost/team4/",
                                                       LastAccess = DateTime.Now.AddDays(-7).ToShortDateString(),
                                                       Type = "/_layouts/kbx/images/menu.png",
                                                       ToolTip = "Quick Launch",
                                                       Reads = 72
                                                   },
                                               new FollowedLink
                                                   {
                                                       Owner = "Franz Ratzfatz",
                                                       URL = "http://localhost/team4/handbook/pages/",
                                                       LastAccess = DateTime.Now.AddDays(-1).ToShortDateString(),
                                                       Type = "/_layouts/kbx/images/document.png",
                                                       ToolTip = "Dokument",
                                                       Reads = 22
                                                   }
                                           };

            GridView1.DataSource = Links;
            GridView1.DataBind();


        }
    }
}

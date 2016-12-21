using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace JQueryWebparts.RefreshWebPart
{
    public partial class RefreshWebPartUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void TimerTicked(object sender, EventArgs e)
        {
            lblStatus.Text = string.Format("Last Refresh at {0}", DateTime.Now.ToShortTimeString());
        }
    }
}

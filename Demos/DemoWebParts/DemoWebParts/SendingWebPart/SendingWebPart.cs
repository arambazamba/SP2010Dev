using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Integrations;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace DemoWebParts.SendingWebPart
{
    [ToolboxItemAttribute(false)]
    public class SendingWebPart : WebPart , IStringContent
    {
        // define the controls used in the webpart
        protected TextBox tb;
        protected LinkButton lb;

        protected override void CreateChildControls()
        {
            tb = new TextBox { Text = "Sample Value" };
            Controls.Add(tb);

            lb = new LinkButton { Text = "Click me" };
            lb.Click += lb_Click;

            Controls.Add(lb);
        }

       // an internal field to store the values

        public SendingWebPart()
        {
            StringValue = string.Empty;
        }

        [ConnectionProvider("String Provider")]
        public IStringContent SharedData()
        {
            return (IStringContent)this;
        }

        public string StringValue { get; set; }

        void lb_Click(object sender, EventArgs e)
        {
            StringValue = tb.Text;
        }
    }
}

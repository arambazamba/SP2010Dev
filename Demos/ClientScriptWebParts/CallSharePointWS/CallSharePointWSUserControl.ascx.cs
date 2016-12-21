using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using DummyWebLib.Publisher.WebForms;

namespace JQueryWebparts.CallSharePointWS
{
    public partial class CallSharePointWSUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.RegisterScript("/_layouts/ClientScriptWebParts/jquery-1.6.2.min.js");
            Page.RegisterScript("/_layouts/sp.js");
            Page.RegisterScript("/_layouts/ClientScriptWebParts/CallSharePointWSUserControl.ascx.js");
            Page.RegisterCSS("/_layouts/styles/1033/demo.css");
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            if (ScriptManager.GetCurrent(Page) == null)
            {
                var scriptManager = new ScriptManager { ID = "ucScriptManager" };
                scriptManager.Services.Add(new ServiceReference("http://chiron/_vti_bin/webs.asmx"));
                Page.Form.Controls.AddAt(0, scriptManager);
            }
        } 
    }
}

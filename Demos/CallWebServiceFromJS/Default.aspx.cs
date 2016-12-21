using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DummyWebLib.Publisher.WebForms;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.RegisterScript("jquery-1.6.2.min.js");
        this.RegisterScript("Default.aspx.js");
        this.RegisterCSS("Dummy.css");
    }
}
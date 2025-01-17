﻿using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace SytleWebpart.StyleWebpart
{
    [ToolboxItemAttribute(false)]
    public class StyleWebpart : WebPart
    {
        protected Label lblMessage;

        protected override void CreateChildControls()
        {
            CssLink cssLink = new CssLink {DefaultUrl = "/_layouts/1033/styles/mystyle.css"};
            Page.Header.Controls.Add(cssLink);
            
            lblMessage = new Label { Text = "I am using CSS", CssClass = "lblStatus" };
            Controls.Add(lblMessage);
        }       
    }
}

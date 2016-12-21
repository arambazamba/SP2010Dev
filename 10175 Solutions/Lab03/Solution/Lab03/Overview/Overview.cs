using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace Lab03.Overview
{
    [ToolboxItemAttribute(false)]
    public class Overview : WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/Lab03/Overview/OverviewUserControl.ascx";

        public Overview()
        {
        }

        protected override void CreateChildControls()
        {
            Control control = this.Page.LoadControl(_ascxPath);
            Controls.Add(control);
            base.CreateChildControls();
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            base.RenderContents(writer);
        }
    }
}

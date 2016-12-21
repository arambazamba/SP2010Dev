using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Administration;

namespace Lab11.BonnevileTestBed
{
    [ToolboxItemAttribute(false)]
    public class BonnevileTestBed : WebPart
    {
        public BonnevileTestBed()
        {
        }
        LiteralControl output = new LiteralControl();
        protected override void CreateChildControls()
        {
            //SPFarm thisFarm = SPFarm.Local;
            //output.Text = thisFarm.Name;
            //this.Controls.Add(output);
            Button renderWebInfo = new Button();
            renderWebInfo.Text = "Access data in this site";
            renderWebInfo.Click += new EventHandler(renderWebInfo_Click);
            Button renderWebInfoElevated = new Button();
            renderWebInfoElevated.Text = "Use elevated privileges";
            renderWebInfoElevated.Click += new EventHandler(renderWebInfoElevated_Click);
            Button accessProhibitedNamespace = new Button();
            accessProhibitedNamespace.Text = "Create an HTTP connection ";
            accessProhibitedNamespace.Click += new EventHandler(accessProhibitedNamespace_Click);
            this.Controls.Add(output);
            this.Controls.Add(new LiteralControl("<br />"));
            this.Controls.Add(renderWebInfo);
            this.Controls.Add(renderWebInfoElevated);
            this.Controls.Add(accessProhibitedNamespace);
        }
        void renderWebInfo_Click(object sender, EventArgs e)
        {
            try
            {
                SPWeb thisWeb = SPContext.Current.Web;
                string message = string.Format("This web contains {0} subwebs", thisWeb.Webs.Count);
                output.Text = message;
            }
            catch (Exception ex)
            {
                output.Text = ex.Message;
            }
        }
        void renderWebInfoElevated_Click(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(showWebCount);
            }
            catch (Exception ex)
            {
                output.Text = "Error caught by my code: " + ex.Message;
            }
        }
        void showWebCount()
        {
            SPWeb thisWeb = SPContext.Current.Web;
            string message = string.Format("This web contains {0} subwebs", thisWeb.Webs.Count);
            output.Text = message;
        }
        void accessProhibitedNamespace_Click(object sender, EventArgs e)
        {
            try
            {
                System.Net.HttpWebRequest.Create("http://intranet.contoso.com");
                output.Text = "Success";
            }
            catch (System.Security.SecurityException secEx)
            {
                output.Text = "Security Violation! Error caught by my code: " + secEx.Message;
            }
            catch (Exception ex)
            {
                output.Text = "Generic Exception. Error caught by my code: " + ex.Message;
            }
        }
        
        protected override void RenderContents(HtmlTextWriter writer)
        {
            base.RenderContents(writer);
        }
    }
}

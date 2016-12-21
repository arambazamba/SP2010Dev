using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;


namespace Lab04.JobDefinitions
{
    //C. Create Interface
    public interface IJobDef
    {
        string JobDef
        {
            get;
            set;
        }
    }
    [ToolboxItemAttribute(false)]
    public class JobDefinitions : WebPart, IJobDef
    {//D. Above: Add Interface
        public JobDefinitions()
        {
        }
        private DropDownList allJobs;
        private string _JobDef;
        //E. Implement JobDef from Interface
        public string JobDef
        {
            get
            {
                return (_JobDef);
            }
            set 
            {
                _JobDef = value;
            }
        }
        //F. Create PROVIDER connection point
        [ConnectionProvider("Job")]
        public IJobDef SendJobName()
        {
            return (this);
        }

        protected override void CreateChildControls()
        {
            allJobs = new DropDownList();
            allJobs.AutoPostBack = true;
            allJobs.EnableViewState = true;
            populateList();
            allJobs.SelectedIndexChanged += new EventHandler(allJobs_SelectedIndexChanged);
            this.Controls.Add(new LiteralControl("Job Definition Filter:<br/>"));
            this.Controls.Add(allJobs);
        }
        void populateList()
        {
            SPWeb jobDefWeb=null;
            SPWeb thisWeb = SPContext.Current.Web;
            SPList jobDefList;
            thisWeb.Site.CatchAccessDeniedException = false;
            try
            {
                allJobs.Items.Add("Pick a Job");
                allJobs.Items.Add("All Jobs");
                jobDefWeb = SPContext.Current.Web.Webs["JobData"];
                jobDefList = jobDefWeb.Lists["Job Definitions"];
                foreach (SPListItem item in jobDefList.Items)
                {
                    allJobs.Items.Add(item.Title);
                }
            }
            catch (UnauthorizedAccessException secEx)
            {
                SPUser privilegedAccount = SPContext.Current.Web.AllUsers[@"SHAREPOINT\SYSTEM"];
                SPUserToken privilegedToken = privilegedAccount.UserToken;
                using (SPSite elevatedSite = new SPSite(SPContext.Current.Web.Url, privilegedToken))
                {
                    using (SPWeb elevatedWeb = elevatedSite.OpenWeb())
                    {
                        jobDefWeb = elevatedWeb.Webs["JobData"];
                        jobDefList = jobDefWeb.Lists["Job Definitions"];
                        foreach (SPListItem item in jobDefList.Items)
                        {
                            allJobs.Items.Add(item.Title);
                        }
                    }
                }
            }
            finally
            {
                thisWeb.Site.CatchAccessDeniedException = true;
                jobDefWeb.Dispose();
            }

        }

        void allJobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            _JobDef = allJobs.SelectedValue;
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            base.RenderContents(writer);
        }
    }
    
}

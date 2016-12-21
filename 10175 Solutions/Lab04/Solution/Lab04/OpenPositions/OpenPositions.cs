using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
//Add using statements
using System.Linq;
using Microsoft.SharePoint.Linq;

namespace Lab04.OpenPositions
{
    [ToolboxItemAttribute(false)]
    public class OpenPositions : WebPart
    {
        public OpenPositions()
        {
        }

        private string jobDefinition = "All Jobs";
        [ConnectionConsumer("Job")]
        public void GetJob(Lab04.JobDefinitions.IJobDef Job)
        {
            if (Job != null)
            {
                jobDefinition = Job.JobDef;
            }
        }

        protected override void CreateChildControls()
        {
            Table jobTable = new Table();
            TableRow jobRow = new TableRow();
            TableCell jobDetail = new TableCell();
            jobDetail.Text = "Job";
            jobDetail.Font.Bold = true;
            jobDetail.HorizontalAlign = HorizontalAlign.Center;
            jobRow.Cells.Add(jobDetail);

            jobDetail = new TableCell();
            jobDetail.Text = "Location";
            jobDetail.Font.Bold = true;
            jobDetail.HorizontalAlign = HorizontalAlign.Center;
            jobRow.Cells.Add(jobDetail);
            

            jobDetail = new TableCell();
            jobDetail.Text = "Interviewer";
            jobDetail.Font.Bold = true;
            jobDetail.HorizontalAlign = HorizontalAlign.Center;
            jobRow.Cells.Add(jobDetail);
            
            jobTable.Rows.Add(jobRow);

            this.Controls.Add(new LiteralControl("Open Positions: " + jobDefinition + "<br />"));
            HREntitiesDataContext hrDC = new HREntitiesDataContext("http://sharepoint");
            if (jobDefinition != "All Jobs")
            {
                var filteredJobs = from vacancyList in hrDC.CurrentVacancies
                                  where(vacancyList.Title.Equals(jobDefinition))
                                  orderby vacancyList.Title
                                  select new { vacancyList.Title, vacancyList.Location, vacancyList.InterviewerImnName };
                foreach (var job in filteredJobs)
                {
                    jobRow = new TableRow();
                    jobDetail = new TableCell();
                    jobDetail.Text = job.Title;
                    jobRow.Cells.Add(jobDetail);

                    jobDetail = new TableCell();
                    jobDetail.Text = job.Location;
                    jobRow.Cells.Add(jobDetail);

                    jobDetail = new TableCell();
                    jobDetail.Text = job.InterviewerImnName;
                    jobRow.Cells.Add(jobDetail);
                    
                    jobTable.Rows.Add(jobRow);
                }
                this.Controls.Add(jobTable);
                return;
            }
            var unfilteredJobs = from vacancyList in hrDC.CurrentVacancies
                              orderby vacancyList.Title
                              select new { vacancyList.Title, vacancyList.Location, vacancyList.InterviewerImnName };
            foreach (var job in unfilteredJobs)
            {
                jobRow = new TableRow();
                jobDetail = new TableCell();
                jobDetail.Text = job.Title;
                jobRow.Cells.Add(jobDetail);

                jobDetail = new TableCell();
                jobDetail.Text = job.Location;
                jobRow.Cells.Add(jobDetail);

                jobDetail = new TableCell();
                jobDetail.Text = job.InterviewerImnName;
                jobRow.Cells.Add(jobDetail);

                jobTable.Rows.Add(jobRow);
            }
            this.Controls.Add(jobTable);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            base.RenderContents(writer);
        }
    }
}

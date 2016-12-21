using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//Add using statement:
using Microsoft.SharePoint;

namespace Lab03.Overview
{
    public partial class OverviewUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Add CAML with Joins
            try
            {
                overviewTree.Nodes.Clear();
                SPWeb thisWeb = SPContext.Current.Web;
                //Do not dispose of thisWeb --- it's the global current content Web.
                SPList candidates = thisWeb.Lists["Outcomes"];
                SPQuery itemMashup = new SPQuery();

                itemMashup.Joins =
                    "<Join Type='LEFT' ListAlias='Candidates'>"
                        + "<Eq><FieldRef Name='Candidate' RefType='Id' /><FieldRef List='Candidates' Name='ID' /></Eq>"
                        + "</Join>";
                itemMashup.ProjectedFields = "<Field Name='Applicant' Type='Lookup' List='Candidates' ShowField='Title' /><Field Name='HomeCity' Type='Lookup' List='Candidates' ShowField='HomeCity' />";
                itemMashup.ViewFields = "<FieldRef Name='Applicant' /><FieldRef Name='HomeCity' /><FieldRef Name='Title' /><FieldRef Name='Interviewer' /><FieldRef Name='Offer' />";

                //Render Tree Items based on result of Joined CAML
                //Applicant gets rendered as top-level node, with other details as sub-nodes to applicaant
                SPListItemCollection allCandidates = candidates.GetItems(itemMashup);
                foreach (SPListItem item in allCandidates)
                {
                    TreeNode applicant = new TreeNode(item["Applicant"].ToString(),null,null, thisWeb.Lists["Candidates"].DefaultViewUrl,"_self");
                    TreeNode opportunity = new TreeNode(item["Title"].ToString(), null, null, thisWeb.Lists["Outcomes"].DefaultViewUrl, "_self");
                    TreeNode homeCity = new TreeNode(item["HomeCity"].ToString(), null, null, thisWeb.Lists["Candidates"].DefaultViewUrl, "_self");
                    TreeNode interviewer = new TreeNode("Interviewed by: " + item["Interviewer"].ToString(), null, null, thisWeb.Lists["Interviews"].DefaultViewUrl, "_self");
                    TreeNode offered = new TreeNode(bool.Parse(item["Offer"].ToString()) == true ? "Job Offered" : "Rejected", null, null, thisWeb.Lists["Outcomes"].DefaultViewUrl, "_self");
                    applicant.ChildNodes.Add(opportunity);
                    applicant.ChildNodes.Add(homeCity);
                    applicant.ChildNodes.Add(interviewer);
                    applicant.ChildNodes.Add(offered);
                    overviewTree.Nodes.Add(applicant);

                }
                overviewTree.ExpandAll();
            }
            catch (Exception ex)
            {
                overviewTree.Nodes.Add(new TreeNode("Err" + ex.Message));
            }
        }
    }
}

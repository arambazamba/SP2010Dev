using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace Lab04.Layouts.Lab04
{
    public partial class CreateJobDef : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            creator.Click+=new EventHandler(creator_Click);
        }
        void creator_Click(object sender, EventArgs e)
        {
            SPWeb thisWeb = SPContext.Current.Web;
            SPWeb newWeb = null;
            if (!thisWeb.Webs["JobData"].Exists)
            {
                try
                {
                    newWeb = thisWeb.Webs.Add("JobData",
                        "Job Data",
                        "Data for Jobs",
                        1033,
                        "STS#1",
                        true,
                        false);

                    SPRoleAssignment roleAssign = new SPRoleAssignment(@"SHAREPOINT\KrishnaS", "krishnas@sharepoint.com", @"SHAREPOINT\KrishnaS", "HR Manager");
                    SPRoleDefinition roleDef = newWeb.RoleDefinitions["Contribute"];
                    roleAssign.RoleDefinitionBindings.Add(roleDef);
                    newWeb.RoleAssignments.Add(roleAssign);
                    newWeb.Update();
                    roleAssign = new SPRoleAssignment(@"SHAREPOINT\MartinR", "martinr@sharepoint.com", @"SHAREPOINT\MartinR", "HR Manager");
                    roleDef = newWeb.RoleDefinitions["Contribute"];
                    roleAssign.RoleDefinitionBindings.Add(roleDef);
                    newWeb.RoleAssignments.Add(roleAssign);
                    newWeb.Update();

                    createList(newWeb);
                    status.Text = "Job Data site and Job Definitions list have been added";
                    creator.Enabled = false;
                }
                catch (Exception webEx)
                {
                    status.Text = webEx.Message;
                }
                finally
                {
                    newWeb.Dispose();
                }               
                return;
            }
            using (SPWeb existingWeb = thisWeb.Webs["JobData"])
            {
                try
                {
                    SPList jobDefToCreate = existingWeb.Lists.TryGetList("Job Definitions");
                    if (jobDefToCreate != null)
                    {
                        status.Text = "Job Data site and Job Definitions already exist";
                        creator.Enabled = false;
                        return;
                    }
                    createList(existingWeb);
                    status.Text = "Job Data site already exists. Job Definitions list have been added";
                    creator.Enabled = false;
                }
                catch (Exception listEx)
                {
                    status.Text = listEx.Message;
                }
            }
        }
        void createList(SPWeb subWeb)
        {
            Guid jobDefGuid = subWeb.Lists.Add("Job Definitions", "Jobs, Salary Ranges, and Descriptions", SPListTemplateType.GenericList);
            SPList jobDef = subWeb.Lists[jobDefGuid];
            jobDef.Fields.Add("MinSalary", SPFieldType.Currency, true);
            jobDef.Fields.Add("MaxSalary", SPFieldType.Currency, true);
            jobDef.Fields.Add("JobDescription", SPFieldType.Text, false);
            jobDef.OnQuickLaunch = true;
            jobDef.Update();
            
            SPView vw = jobDef.Views["All Items"];
            vw.ViewFields.Add("MinSalary");
            vw.ViewFields.Add("MaxSalary");
            vw.ViewFields.Add("JobDescription");
            vw.InlineEdit = "TRUE";
            vw.Update();
            
            SPListItem newDef;
            newDef = jobDef.Items.Add();
            newDef["Title"]= "Developer";
            newDef["MinSalary"] = "40000";
            newDef["MaxSalary"] = "80000";
            newDef["JobDescription"] = "SharePoint or other Web Developer";
            newDef.Update();

            newDef = jobDef.Items.Add();
            newDef["Title"] = "Analyst";
            newDef["MinSalary"] = "40000";
            newDef["MaxSalary"] = "90000";
            newDef["JobDescription"] = "Business Analyst";
            newDef.Update();

            newDef = jobDef.Items.Add();
            newDef["Title"] = "Lead Developer";
            newDef["MinSalary"] = "60000";
            newDef["MaxSalary"] = "100000";
            newDef["JobDescription"] = "Developer with Team-Leading Experience";
            newDef.Update();

            newDef = jobDef.Items.Add();
            newDef["Title"] = "Solution Architect";
            newDef["MinSalary"] = "80000";
            newDef["MaxSalary"] = "120000";
            newDef["JobDescription"] = "Experience Solution Architect";
            newDef.Update();
        }
    }
}

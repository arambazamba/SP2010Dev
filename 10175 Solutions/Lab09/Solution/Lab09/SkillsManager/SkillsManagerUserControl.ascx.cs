using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
namespace Lab09.SkillsManager
{
    public partial class SkillsManagerUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                skillsMap.Nodes.Clear();
                SPWeb thisWeb = SPContext.Current.Web;
                SPWeb skillsWeb = thisWeb.Webs["Skills"];
                SPList jobs = skillsWeb.Lists["Jobs"];
                SPList mashup = skillsWeb.Lists["Mashup"];
                SPList skills = skillsWeb.Lists["Skills"];
                foreach (SPListItem item in jobs.Items)
                {
                    TreeNode job = new TreeNode(item["Title"].ToString() + ": " + item["Level"].ToString());
                    int jobID = item.ID;
                    SPQuery skillsMapping = new SPQuery();
                    skillsMapping.ViewFields = "<FieldRef Name='LookupSkills' />";
                    skillsMapping.Query = "<Where><Eq><FieldRef Name='LookupJobs' />"
                                        + "<Value Type='Integer'>" + jobID.ToString() + "</Value></Eq></Where>";
                    SPListItemCollection mappings = mashup.GetItems(skillsMapping);
                    if (mappings.Count > 0)
                    {
                        //
                        SPQuery assignedSkills = new SPQuery();
                        assignedSkills.ViewFields = "<FieldRef Name='ID' /><FieldRef Name='Title' /><FieldRef Name='Importance' />";
                        SPListItemCollection jobSkills = skills.GetItems(assignedSkills);
                        foreach (SPListItem jobSkill in jobSkills)
                        {
                            foreach (SPListItem mapping in mappings)
                            {
                                if(mapping["LookupSkills"].ToString()==jobSkill.ID.ToString())
                                {
                                    job.ChildNodes.Add(new TreeNode(jobSkill["Title"].ToString() + ": " + jobSkill["Importance"].ToString()));
                                }
                            }
                        }

                       
                    }
                    skillsMap.Nodes.Add(job);
                }



               

                
                skillsMap.ExpandAll();
            }
            catch (Exception ex)
            {
                skillsMap.Nodes.Add(new TreeNode(ex.Message));
            }
        }
    }
}

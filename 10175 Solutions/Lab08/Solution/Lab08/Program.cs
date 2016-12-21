using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;


namespace Lab08
{
    class Program
    {
        static void Main(string[] args)
        {
            string option = string.Empty;
            Console.Write("Options: [C] Create Skills Matrix: | [V] View Skills Matrix: ");
            option = Console.ReadLine().ToUpper();
            while ((option != "C") && (option != "V"))
            {
                Console.Write("Options: [C] Create Skills Matrix: | [V] View Skills Matrix: ");
                option = Console.ReadLine().ToUpper();
            }
            if (option == "C")
            {
                createSkillsMatrix();
                return;
            }
            viewSkillsMatrix();
        }
        static void createSkillsMatrix()
        {
            ClientContext remoteCtx = null;
            try
            {
                Console.Write("Enter the URL of a SharePoint site to house the Skills Matrix (e.g. http://myserver): ");
                string siteUrl = Console.ReadLine().ToLower();
                while (!siteUrl.StartsWith("http://"))
                {
                    Console.Write("Enter the URL of a SharePoint site to house the Skills Matrix (e.g. http://myserver): ");
                    siteUrl = Console.ReadLine().ToLower();
                }
                remoteCtx = new ClientContext(siteUrl);
                Web remoteWeb = remoteCtx.Web;
                remoteCtx.Load(remoteWeb);
                WebCreationInformation skillsInfo = new WebCreationInformation();
                skillsInfo.Title = "Skills Matrix";
                skillsInfo.Language = 1033;
                skillsInfo.WebTemplate = "STS#1";
                skillsInfo.Url = "Skills";
                skillsInfo.UseSamePermissionsAsParentSite = true;
                Web skillsWeb = remoteWeb.Webs.Add(skillsInfo);
                skillsWeb.QuickLaunchEnabled = true;
                Console.Write("Skills Site is being created...");
                remoteCtx.ExecuteQuery();
                Console.WriteLine(" Success!");
                Console.Write("Skills Matrix is being created...");
                
                ListCreationInformation jobDefInfo = new ListCreationInformation();
                jobDefInfo.TemplateType = (int)ListTemplateType.GenericList;
                jobDefInfo.Title = "Jobs";
                jobDefInfo.QuickLaunchOption = QuickLaunchOptions.On;
                List jobDef = skillsWeb.Lists.Add(jobDefInfo);
                jobDef.Fields.AddFieldAsXml(@"<Field Type='Text' DisplayName='Description'/>",
                    true,
                    AddFieldOptions.DefaultValue);
                jobDef.Fields.AddFieldAsXml(@"<Field Type='Choice' DisplayName='Level' Format='Dropdown'>"
                    + "<Default>Junior</Default>"
                    + "<CHOICES>"
                        + "<CHOICE>N/A</CHOICE>"
                        + "<CHOICE>Junior</CHOICE>"
                        + "<CHOICE>Senior</CHOICE>"
                        + "<CHOICE>Lead</CHOICE>"
                        + "</CHOICES>"
                    + "</Field>",
                    true, AddFieldOptions.DefaultValue);

                ListItemCreationInformation jobInfo = new ListItemCreationInformation();
                ListItem job = jobDef.AddItem(jobInfo);
                job["Title"] = "Developer";
                job["Description"] = "Writes Code to Spec";
                job["Level"] = "Junior";
                job.Update();
                job = jobDef.AddItem(jobInfo);
                job["Title"] = "Developer";
                job["Description"] = "Creates Specs and Writes Code";
                job["Level"] = "Senior";
                job.Update();
                job = jobDef.AddItem(jobInfo);
                job["Title"] = "Developer";
                job["Description"] = "Manages Development Teams";
                job["Level"] = "Lead";
                job.Update();
                job = jobDef.AddItem(jobInfo);
                job["Title"] = "Analysts";
                job["Description"] = "Defines business processes";
                job["Level"] = "Junior";
                job.Update();
                job = jobDef.AddItem(jobInfo);
                job["Title"] = "Analyst";
                job["Description"] = "Defines business processes and creates reports";
                job["Level"] = "Senior";
                job.Update();
                job = jobDef.AddItem(jobInfo);
                job["Title"] = "Analyst";
                job["Description"] = "Manages Analysis Teams";
                job["Level"] = "Lead";
                job.Update();

                ListCreationInformation skillDefInfo = new ListCreationInformation();
                skillDefInfo.TemplateType = (int)ListTemplateType.GenericList;
                skillDefInfo.Title = "Skills";
                skillDefInfo.QuickLaunchOption = QuickLaunchOptions.On;
                List skillDef = skillsWeb.Lists.Add(skillDefInfo);
                skillDef.Fields.AddFieldAsXml(@"<Field Type='Text' DisplayName='Description'/>",
                    true,
                    AddFieldOptions.DefaultValue);
                skillDef.Fields.AddFieldAsXml(@"<Field Type='Choice' DisplayName='Importance' Format='Dropdown'>"
                    + "<Default>Essential</Default>"
                    + "<CHOICES>"
                        + "<CHOICE>Essential</CHOICE>"
                        + "<CHOICE>Preferred</CHOICE>"
                        + "<CHOICE>Optional</CHOICE>"
                        + "</CHOICES>"
                    + "</Field>",
                    true, AddFieldOptions.DefaultValue);

                ListItemCreationInformation skillInfo = new ListItemCreationInformation();
                ListItem skill = skillDef.AddItem(skillInfo);
                skill["Title"] = "Write Code";
                skill["Importance"] = "Essential";
                skill.Update();
                skillInfo = new ListItemCreationInformation();
                skill = skillDef.AddItem(skillInfo);
                skill["Title"] = "Write Code";
                skill["Importance"] = "Preferred";
                skill.Update();
                skillInfo = new ListItemCreationInformation();
                skill = skillDef.AddItem(skillInfo);
                skill["Title"] = "Write Code";
                skill["Importance"] = "Optional";
                skill.Update();
                skill = skillDef.AddItem(skillInfo);
                skill["Title"] = "Make Technical Decisions";
                skill["Importance"] = "Essential";
                skill.Update();
                skillInfo = new ListItemCreationInformation();
                skill = skillDef.AddItem(skillInfo);
                skill["Title"] = "Make Technical Decisions";
                skill["Importance"] = "Preferred";
                skill.Update();
                skillInfo = new ListItemCreationInformation();
                skill = skillDef.AddItem(skillInfo);
                skill["Title"] = "Make Technical Decisions";
                skill["Importance"] = "Optional";
                skill.Update();

                skill = skillDef.AddItem(skillInfo);
                skill["Title"] = "Analyze Business Processes";
                skill["Importance"] = "Essential";
                skill.Update();
                skillInfo = new ListItemCreationInformation();
                skill = skillDef.AddItem(skillInfo);
                skill["Title"] = "Analyze Business Processes";
                skill["Importance"] = "Preferred";
                skill.Update();
                skillInfo = new ListItemCreationInformation();
                skill = skillDef.AddItem(skillInfo);
                skill["Title"] = "Analyze Business Processes";
                skill["Importance"] = "Optional";
                skill.Update();

                skill = skillDef.AddItem(skillInfo);
                skill["Title"] = "Write Reports";
                skill["Importance"] = "Essential";
                skill.Update();
                skillInfo = new ListItemCreationInformation();
                skill = skillDef.AddItem(skillInfo);
                skill["Title"] = "Write Reports";
                skill["Importance"] = "Preferred";
                skill.Update();
                skillInfo = new ListItemCreationInformation();
                skill = skillDef.AddItem(skillInfo);
                skill["Title"] = "Write Reports";
                skill["Importance"] = "Optional";
                skill.Update();

                remoteCtx.ExecuteQuery();
                remoteCtx.Load(jobDef);
                remoteCtx.Load(skillDef);
                remoteCtx.ExecuteQuery();
                ListCreationInformation mixerInfo = new ListCreationInformation();
                mixerInfo.TemplateType = (int)ListTemplateType.GenericList;
                mixerInfo.Title = "Mashup";
                mixerInfo.QuickLaunchOption = QuickLaunchOptions.Off;
                List mixer = skillsWeb.Lists.Add(mixerInfo);
                mixer.Fields.AddFieldAsXml(@"<Field Type='Integer' Name='LookupJobs' DisplayName='LookupJobs' Required='TRUE'/>",
                    true,
                    AddFieldOptions.DefaultValue);
                mixer.Fields.AddFieldAsXml(@"<Field Type='Integer' Name='LookupSkills' DisplayName='LookupSkills' Required='TRUE'/>",
                    true,
                    AddFieldOptions.DefaultValue);
                Field titleField = mixer.Fields.GetByInternalNameOrTitle("Title");
                titleField.Hidden = true;
                titleField.Update();
                mixer.Hidden = true;
                mixer.Update();
                

                remoteCtx.ExecuteQuery();

                Console.WriteLine("Success!");
                Console.Write("Press any key to exit...");
                Console.ReadLine();
            }
            catch(Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.Write("Press any key to exit...");
                Console.ReadLine();
            }
            finally
            {
                remoteCtx.Dispose();
            }
        }
        static void viewSkillsMatrix()
        {
            //TODO: As part of RTM Release, add ADO.NET Data Services/REST APIs for retrieving data from the skills matrix
        }
    }
}

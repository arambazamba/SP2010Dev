using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Server;
using Microsoft.Office.Server.UserProfiles;


namespace Lab12.Layouts.Lab12
{
    public partial class ProfileReporter : LayoutsPageBase
    {
        UserProfileManager uMan=null;
       
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                SPServiceContext svrCtx = SPServiceContext.Current;
                uMan = new UserProfileManager(svrCtx);
                TableCell cell;
                TableRow row;
                row = new TableRow();
                cell = new TableCell();
                cell.Text = "User";
                cell.Font.Bold = true;
                row.Cells.Add(cell);
                cell = new TableCell();
                cell.Text = "Email";
                cell.Font.Bold = true;
                row.Cells.Add(cell);
                cell = new TableCell();
                cell.Text = "Personal Site";
                cell.Font.Bold = true;
                row.Cells.Add(cell);
                configuredTable.Rows.Add(row);
                foreach (UserProfile uProfile in uMan)
                {
                    ProfileSubtypePropertyManager propMan = uMan.DefaultProfileSubtypeProperties;
                    string accountName = uProfile["AccountName"].Value.ToString();
                    string emailAddr = string.Empty;
                    string personalSite = string.Empty;
                    if (uProfile["WorkEmail"].Value != null)
                    {
                        emailAddr = uProfile["WorkEmail"].Value.ToString();
                    }
                    if (uProfile["PersonalSpace"].Value != null)
                    {
                        personalSite = uProfile["PersonalSpace"].Value.ToString();
                    }
                    row = new TableRow();
                    cell = new TableCell();
                    cell.Text = accountName;
                    row.Cells.Add(cell);
                    cell = new TableCell();
                    if (emailAddr != string.Empty)
                    {
                        HyperLink emailHyper = new HyperLink();
                        emailHyper.Text = emailAddr;
                        emailHyper.NavigateUrl = "mailto:" + emailAddr;
                        cell.Controls.Add(emailHyper);
                    }
                    else
                    {
                        if (uProfile["AccountName"].Value.ToString().ToLower()==SPContext.Current.Web.CurrentUser.LoginName.ToLower())
                        {
                            Button emailUpdater = new Button();
                            emailUpdater.CommandName = uProfile.RecordId.ToString();
                            emailUpdater.Text = "Set to default...";
                            emailUpdater.Click += new EventHandler(emailUpdater_Click);
                            cell.Controls.Add(emailUpdater);
                        }
                        else
                        {
                            cell.Text = "Email not set";
                        }
                    }
                    row.Cells.Add(cell);
                    cell = new TableCell();
                    if ((personalSite == "http://") || (personalSite == string.Empty)) //Default Value or empty
                    {
                        if (uProfile["AccountName"].Value.ToString().ToLower() == SPContext.Current.Web.CurrentUser.LoginName.ToLower())
                        {
                            Button mySiteCreator = new Button();
                            mySiteCreator.CommandName = uProfile.RecordId.ToString();
                            mySiteCreator.Text = "Create...";
                            mySiteCreator.Click += new EventHandler(mySiteCreator_Click);
                            cell.Controls.Add(mySiteCreator);
                        }
                        else
                        {
                            cell.Text = "Personal site not created";
                        }
                    }
                    else
                    {
                        cell.Text = personalSite;

                    }
                    row.Cells.Add(cell);
                    configuredTable.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                this.Page.Response.Write(ex.Message);
            }
        }
        void emailUpdater_Click(object sender, EventArgs e)
        {
            try
            {
                Button src = (Button)sender;
                UserProfile userProfile = (UserProfile)uMan.GetProfile((long.Parse(src.CommandName)));
                string accountName = userProfile["AccountName"].Value.ToString();
                int iPos = accountName.IndexOf("\\");
                accountName = accountName.Substring(iPos + 1);
                string updatedEmail = accountName + "@sharepoint.com";
                userProfile["WorkEmail"].Value = updatedEmail;
                userProfile.Commit();
            }
            catch (Exception ex)
            {
                this.Page.Response.Write(ex.Message);
            }
        }
        void mySiteCreator_Click(object sender, EventArgs e)
        {
            try
            {
                Button src = (Button)sender;
                UserProfile userProfile = (UserProfile)uMan.GetUserProfile(long.Parse(src.CommandName));
                userProfile.CreatePersonalSite();
            }
            catch (Exception ex)
            {
                this.Page.Response.Write(ex.Message);
            }

        }
    }
}

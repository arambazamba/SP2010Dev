using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace Lab05.CheckJobs
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class CheckJobs : SPItemEventReceiver
    {
        public override void ItemAdding(SPItemEventProperties properties)
        {
            try
            {
                bool allowed = checkItem( properties);
                if (!allowed)
                {
                    properties.Status = SPEventReceiverStatus.CancelWithError;
                    properties.ErrorMessage = "The job you have entered is not defined in the Job Definitions List";
                    properties.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                properties.Status = SPEventReceiverStatus.CancelWithError;
                properties.ErrorMessage = ex.Message;
                properties.Cancel = true;
            }
        }

        /// <summary>
        /// An item is being updated.
        /// </summary>
        public override void ItemUpdating(SPItemEventProperties properties)
        {
            try
            {
                bool allowed = checkItem(properties);
                if (!allowed)
                {
                    properties.Status = SPEventReceiverStatus.CancelWithError;
                    properties.ErrorMessage = "The job you have entered is not defined in the Job Definitions List";
                    properties.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                properties.Status = SPEventReceiverStatus.CancelWithError;
                properties.ErrorMessage = ex.Message;
                properties.Cancel = true;
            }
        }

        bool checkItem(SPItemEventProperties properties)
        {
            string jobTitle = properties.AfterProperties["Title"].ToString();
            bool allowed = false;
            SPWeb jobDefWeb = null;
            SPList jobDefList;
            SPUser privilegedAccount = properties.Web.AllUsers[@"SHAREPOINT\SYSTEM"];
            SPUserToken privilegedToken = privilegedAccount.UserToken;
            try
            {
                using (SPSite elevatedSite = new SPSite(properties.Web.Url, privilegedToken))
                {
                    using (SPWeb elevatedWeb = elevatedSite.OpenWeb())
                    {
                        jobDefWeb = elevatedWeb.Webs["JobData"];
                        jobDefList = jobDefWeb.Lists["Job Definitions"];
                        foreach (SPListItem item in jobDefList.Items)
                        {
                            if (item["Title"].ToString() == jobTitle)
                            {
                                allowed = true;
                                break;
                            }
                        }
                    }
                }
                return (allowed);
            }
            finally
            {
                jobDefWeb.Dispose();
            }
        }
    }
}

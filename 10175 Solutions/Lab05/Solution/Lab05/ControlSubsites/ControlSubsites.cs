using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
//1.
using System.Configuration;
using System.Web;


namespace Lab05.ControlSubsites
{
    /// <summary>
    /// Web Events
    /// </summary>
    public class ControlSubsites : SPWebEventReceiver
    {
       /// <summary>
       /// A site is being provisioned.
       /// </summary>
       public override void WebAdding(SPWebEventProperties properties)
       {
           string preventProliferation = ConfigurationManager.AppSettings["preventProliferation"].ToString();
           if (preventProliferation == "1")
           {
               properties.ErrorMessage = "Sub webs are not allowed";
               properties.Status = SPEventReceiverStatus.CancelWithError;
               properties.Cancel = true;
               return;
           }
           base.WebAdding(properties);
       }


    }
}

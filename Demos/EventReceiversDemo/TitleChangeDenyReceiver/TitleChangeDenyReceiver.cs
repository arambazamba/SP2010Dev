using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace EventReceiversDemo.TitleChangeDenyReceiver
{
    public class TitleChangeDenyReceiver : SPItemEventReceiver
    {
       public override void ItemUpdating(SPItemEventProperties properties)
       {
           base.ItemUpdating(properties);
           string bTitle = properties.BeforeProperties["Title"].ToString();
           string aTitle = properties.AfterProperties["Title"].ToString();

           if (bTitle != aTitle)
           {
               properties.ErrorMessage = "Title change is not allowed";
               properties.Status = SPEventReceiverStatus.CancelWithError;
               properties.Cancel = true;
           }
       }
    }
}

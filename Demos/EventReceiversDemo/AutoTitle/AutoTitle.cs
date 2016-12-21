using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace EventReceiversDemo.AutoTitle
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class AutoTitle : SPItemEventReceiver
    {
       /// <summary>
       /// An item was added.
       /// </summary>
       public override void ItemAdded(SPItemEventProperties properties)
       {
           SPListItem item = properties.ListItem;
           if (item.Title == string.Empty)
           {
               item["Title"] = item.Name.Substring(0, item.Name.LastIndexOf("."));
           }

           item.Update();
       }


    }
}

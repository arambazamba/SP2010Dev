using System;
using System.Linq;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using System.Collections.Generic;

namespace Integrations.DocumentLibraryExtensions
{
    public class UploadRestrictionEventReceiver : SPItemEventReceiver
    {
        public override void ItemAdded(SPItemEventProperties properties)
        {            
            //Prüfen der File Extension
            string filename = properties.ListItem.Name;
            string ext = filename.Substring(filename.LastIndexOf(".")+1);
            string allowed;

            if (properties.Web.Properties["aft_" + properties.ListId]!=null)
            {
                allowed = properties.Web.Properties["aft_" + properties.ListId];
                List<string> vals = allowed.Split(new[] {','}).ToList();
                if (vals.Contains(ext)==false)
                {
                    properties.Cancel = true;
                    properties.Status = SPEventReceiverStatus.CancelWithError;
                    properties.ErrorMessage = string.Format("Der Upload von {0} ist nicht erlaubt", ext);
                }                
            }       
        }
    }
}

using System;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using System.Collections.Generic;

namespace Integrations.DocumentLibraryExtensions
{
    public class UploadRestrictionEventReceiver : SPItemEventReceiver
    {
        protected const string key = "AllowedFileTypes";

        public override void ItemAdding(SPItemEventProperties properties)
        {            
            //Prüfen der File Extension
            string ext = properties.AfterUrl.Substring(properties.AfterUrl.LastIndexOf(".") + 1);

            object filetypes = properties.List.RootFolder.Properties[key];
            if (filetypes != null)
            {
                List<string> vals = filetypes.ToString().Split(new[] {','}).ToList();
                if (vals.Contains(ext) == false)
                {
                    string errormsg = string.Format(@"Der Upload von ""{0}""-Dateien ist nicht erlaubt", ext);
                  
                    properties.Cancel = true;
                    properties.Status = SPEventReceiverStatus.CancelWithRedirectUrl;
                    properties.RedirectUrl = string.Format("/_layouts/DocumentLibraryExtensions/UploadError.aspx?errormsg={0}", errormsg);
                }
            }
        }
    }
}

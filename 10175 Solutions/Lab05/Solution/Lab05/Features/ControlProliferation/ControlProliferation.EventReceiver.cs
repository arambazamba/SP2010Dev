using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
//1.
using Microsoft.SharePoint.Administration;

namespace Lab05.Features.ControlProliferation
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("9655c705-fb67-4ed3-9cf9-8c49d1f588f4")]
    public class ControlProliferationEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            setProliferationFlag(true);
        }

        // Uncomment the method below to handle the event raised before a feature is deactivated.
        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            setProliferationFlag(false);
        }
        void setProliferationFlag(bool status)
        {
            
            SPWebApplication webApp = SPWebApplication.Lookup(new Uri("http://SharePoint"));

            try
            {
                SPWebConfigModification mySetting = null;
                if (status)
                {
                    mySetting = new SPWebConfigModification();
                    mySetting.Path = "configuration/appSettings";
                    mySetting.Name = "add [@key='preventProliferation'] [@value='1']";
                    mySetting.Sequence = 0;
                    mySetting.Owner = "Lab05Owner";
                    mySetting.Type = SPWebConfigModification.SPWebConfigModificationType.EnsureChildNode;
                    mySetting.Value = "<add key='preventProliferation' value='1' />";
                    webApp.WebConfigModifications.Add(mySetting);
                }
                else
                {
                    foreach (SPWebConfigModification modification in webApp.WebConfigModifications)
                    {
                        if (modification.Owner == "Lab05Owner")
                        {
                            modification.Value = "<add key='preventProliferation' value='0' />"; ;
                        }
                    }
                }
                webApp.Update();
                webApp.Farm.Services.GetValue<SPWebService>().ApplyWebConfigModifications();
            }
            catch
            {

            }

        }

        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}

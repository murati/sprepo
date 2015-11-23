using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using System.Collections.Generic;
using System.Linq;

namespace SharePoint.Repository.ContentTypes.Features.SharePoint.Repository_Content_Types
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("fb12d18b-9be2-4929-a2e2-181593d0696c")]
    public class SharePointEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWeb web = properties.Feature.Parent as SPWeb;
            Dictionary<string, string> lookupInfo = new Dictionary<string, string> { { "Locations", "Locations List" }};
            if (web != null)
            {
                for (int i = 0; i < lookupInfo.Count; i++)
                {

                    SPList masterList = web.Lists.TryGetList(lookupInfo.ElementAt(i).Value);
                    if (masterList != null)
                    {
                        // find the lookup field
                        SPFieldLookup lookUpField = web.Fields.GetFieldByInternalName(lookupInfo.ElementAt(i).Key) as SPFieldLookup;
                        if (lookUpField != null)
                        {
                            string replaceStr = "List={" + masterList.ID + "}";
                            lookUpField.SchemaXml = lookUpField.SchemaXml.Replace(@"List=""", replaceStr);
                            lookUpField.Update();
                        }
                    }
                }
            }
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        //public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        //{
        //}


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

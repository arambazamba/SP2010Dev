using Microsoft.SharePoint.Client.Services;
using System.ServiceModel.Activation;

namespace PermissionServiceWCF
{
    [BasicHttpBindingServiceMetadataExchangeEndpoint]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class PermissionService : IPermissionService
    {

        public SecurityInfo[] GetPermissions(string SiteURL, string Web)
        {
            return PermissionServiceImpl.GetPermissions(SiteURL, Web);
        }
    }
}

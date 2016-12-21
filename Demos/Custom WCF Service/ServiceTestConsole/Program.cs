using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using ServiceTestConsole.Integrations;

namespace ServiceTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            PermissionServiceClient client = new PermissionServiceClient();
            client.ClientCredentials.Windows.ClientCredential = CredentialCache.DefaultNetworkCredentials;
            client.ClientCredentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Impersonation;

            var result = client.GetPermissions("http://chiron", "Sales");
        }
    }
}

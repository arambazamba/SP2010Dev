using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace PermissionServiceWCF
{
    public static class PermissionServiceImpl
    {
        public static SecurityInfo[] GetPermissions(string SiteURL, string Web)
        {
            var result = new List<SecurityInfo>();
            var site = new SPSite(SiteURL);

            SPWeb web = site.AllWebs[Web];

            foreach (SPList list in web.Lists)
            {
                foreach (SPRoleAssignment rs in list.RoleAssignments)
                {
                    Console.WriteLine(rs.Member.Name);

                    result.AddRange(from SPRoleDefinition rd in rs.RoleDefinitionBindings select new SecurityInfo {Ressource = list.Title, SecurityPrincipal = rs.Member.Name, PermissionLevel = rd.Name});
                }
            }
            return result.ToArray();

        }
    }
}

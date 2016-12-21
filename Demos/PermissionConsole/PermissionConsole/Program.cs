using System;
using System.Linq;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Navigation;

namespace PermissionConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // create site, list, set security
            SPSite col = new SPSite("http://chiron");
            
            SPWeb root = col.RootWeb;

            foreach (SPWebTemplate t in root.GetAvailableWebTemplates(1033))
            {
                Console.WriteLine(t.Name + " " + t.Title);
            }

            SPWeb web = root.Webs.Add("CodedWeb", "CodedWeb", "Demo Description", 1033, "STS#1", true, false);

            // add navigation
            SPNavigation navRoot = root.Navigation;
            SPNavigationNodeCollection navQL = navRoot.QuickLaunch;
            
            var newNav = new SPNavigationNode(web.Title, web.ServerRelativeUrl);
            var orf = new SPNavigationNode("Orf", "http://www.orf.at");

            navQL.Add(newNav, newNav);

            // alternative
            // web.QuickLaunchEnabled = true;

            Guid listid = web.Lists.Add("My Pictures", "PersonalPictures", web.ListTemplates["Picture Library"]);
            SPList list = web.Lists[listid];
            list.OnQuickLaunch = true;
            
            // list permission levels
            foreach (SPRoleDefinition PermLevel in list.ParentWeb.RoleDefinitions)
            {
                Console.WriteLine(string.Format("Role: {0}, ID: {1}", PermLevel.Name, PermLevel.Id));
            }

            // list groups
            foreach (SPGroup gp in list.ParentWeb.SiteGroups)
            {
                Console.WriteLine(string.Format("Group: {0}", gp.Name));
            }

            // break rights inheritance - boolean: copy permissions
            list.BreakRoleInheritance(false);

            // assign permissions
            list.ParentWeb.SiteUsers.Add(@"class\thai.boy", "thai.boy@class.local", "Thai Boy", "A user from Thailand");

            SPUser usrThailand = list.ParentWeb.SiteUsers[@"class\thai.boy"]; //login name

            if (usrThailand != null)
            {
                // lookup permission level full control
                SPRoleDefinition PermLevelFull = list.ParentWeb.RoleDefinitions["Full Control"];

                SPRoleAssignment assignment = new SPRoleAssignment(usrThailand);

                // Bind Assignment to definition
                assignment.RoleDefinitionBindings.Add(PermLevelFull);

                list.RoleAssignments.Add(assignment);

                list.Update();

                // Auslesen von Permissions
                foreach (SPRoleAssignment rs in list.RoleAssignments)
                {
                    //List Security Principals
                    Console.WriteLine(rs.Member.Name);

                    foreach (SPRoleDefinition rd in rs.RoleDefinitionBindings)
                    {
                        //Rechte 
                        Console.WriteLine( rd.Name);
                    }
                }

                //Same Pattern for Groups
                SPGroup mgr = list.ParentWeb.Groups["Manager"];
                if (mgr != null)
                {
                    SPRoleAssignment ass = new SPRoleAssignment(mgr);
                    // ....
                }


            }
        }
    }
}

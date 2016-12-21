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
            // parameter and object definition
            string url = "http://chiron";
            uint locale = 1033;
            string siteTemplate = "STS#1"; //Blank Site Template
            string webName = "CodedWeb";

            // create site, list, set security
            SPSite col = new SPSite(url);
            SPWeb web = CreateSite(col, locale, siteTemplate, webName);
            SPList list = CreateList(web);
            SetSecurityOnRessouces(list);
        }

        private static SPWeb CreateSite(SPSite Col, uint locale, string template, string SiteName)
        {
            SPWeb root = Col.RootWeb;
            SPWeb web = root.Webs.Add(SiteName, SiteName, "Demo Description", locale, template, true, false);

            // add navigation
            SPNavigation navRoot = root.Navigation;
            SPNavigationNodeCollection navQL = navRoot.QuickLaunch;
            var newNav = new SPNavigationNode(web.Title, web.ServerRelativeUrl);
            navQL.Add(newNav, newNav);

            // alternative
            // web.QuickLaunchEnabled = true;

            return web;
        }

        private static SPList CreateList(SPWeb Web)
        {
            Guid listid = Web.Lists.Add("My Pictures", "PersonalPictures", Web.ListTemplates["Picture Library"]);
            SPList pictureLib = Web.Lists[listid];
            pictureLib.OnQuickLaunch = true;
            return pictureLib;
        }

        private static void SetSecurityOnRessouces(SPList List)
        {
            // list permission levels
            foreach (SPRoleDefinition PermLevel in List.ParentWeb.RoleDefinitions)
            {
                Console.WriteLine(string.Format("Role: {0}, ID: {1}", PermLevel.Name, PermLevel.Id));
            }

            // list groups
            foreach (SPGroup gp in List.ParentWeb.SiteGroups)
            {
                Console.WriteLine(string.Format("Group: {0}", gp.Name));
            }

            // break rights inheritance - boolean: copy permissions
            List.BreakRoleInheritance(false);

            // assign permissions to class\katja
            List.ParentWeb.SiteUsers.Add(@"class\thai.boy", "thai.boy@class.local", "Thai Boy", "A user from Thailand");

            SPUser usrThailand = List.ParentWeb.SiteUsers[@"class\thai.boy"]; //login name

            if (usrThailand != null)
            {
                // create a new permission level
                SPRoleDefinition PermLevelFull = List.ParentWeb.RoleDefinitions["Full Control"];

                SPRoleAssignment assignment = new SPRoleAssignment(usrThailand);

                //Same Pattern for Groups
                SPGroup grp = List.ParentWeb.Groups["xy"];
                if (grp != null)
                {
                    SPRoleAssignment ass = new SPRoleAssignment(grp);
                }

                // Bind Assignment to definition
                assignment.RoleDefinitionBindings.Add(PermLevelFull);

                List.RoleAssignments.Add(assignment);

                List.Update();
            }
        }
    }
}

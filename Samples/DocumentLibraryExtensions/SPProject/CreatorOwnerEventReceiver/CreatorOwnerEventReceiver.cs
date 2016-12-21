using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace Integrations.DocumentLibraryExtensions
{
    public class CreatorOwnerEventReceiver : SPItemEventReceiver
    {
       /// <summary>
       /// An item was added.
       /// </summary>
       public override void ItemAdded(SPItemEventProperties properties)
       {
           SPListItem item = properties.ListItem;
           item.BreakRoleInheritance(false);

           SPRoleDefinition full = properties.Web.RoleDefinitions["Full Control"];
           if (full!=null)
           {
               SPRoleAssignment perm = new SPRoleAssignment(properties.Web.CurrentUser);
               perm.RoleDefinitionBindings.Add(full);
               item.RoleAssignments.Add(perm);
           }
       }
    }
}

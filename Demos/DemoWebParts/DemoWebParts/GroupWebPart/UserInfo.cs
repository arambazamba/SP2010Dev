using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Server.UserProfiles;
using Microsoft.SharePoint;
using System.DirectoryServices.AccountManagement;


namespace DemoWebParts.GroupMembershipWebpart
{
    [System.ComponentModel.DataObject(true)]
    public class UserInfo
    {
        protected static string Domain = "Class";

        public string Benutzer { get; set; }
        public string EMail { get; set; }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, false)]
        public static List<UserInfo> GetUserForSiteGroups()
        {
            var result = new Dictionary<string, UserInfo>();

            foreach (SPUser usr in from SPGroup grp in SPContext.Current.Web.Groups from SPUser usr in grp.Users select usr)
            {
                if (usr.IsDomainGroup)
                {
                    var usernames = ResolveGroup(usr.LoginName.Substring(Domain.Length+1));
                    foreach (var name in usernames.Where(name => result.ContainsKey(name.ToLower())==false))
                    {
                        result.Add(name.ToLower(), new UserInfo { Benutzer = GetDisplayNameFromProfile(name)});
                    }
                }
                else
                {
                    if (result.ContainsKey(usr.LoginName.ToLower()) == false)
                    {
                        result.Add(usr.LoginName.ToLower(), new UserInfo { Benutzer = GetDisplayNameFromProfile(usr.LoginName) });
                    }
                }
            }
            
            return result.Select(r=>r.Value).OrderBy(i=>i.Benutzer).ToList();            
        }

        public static string GetDisplayNameFromProfile(string Login)
        {
            SPServiceContext ctx = SPServiceContext.GetContext(SPContext.Current.Site);
            var upm = new UserProfileManager(ctx);
            try
            {
                UserProfile p = upm.GetUserProfile(Login);
                return p != null ? p.DisplayName : Login;
            }
            catch (Exception)
            {
                return Login;
            }
        }

        public static List<string> ResolveGroup(string Group)
        {
            var result= new List<string>();
            var ctx = new PrincipalContext(ContextType.Domain, Domain);
            GroupPrincipal grp = GroupPrincipal.FindByIdentity(ctx, IdentityType.Name, Group);

            if (grp != null)
            {
                result.AddRange(grp.GetMembers(true).Select(p => string.Format(@"{0}\{1}", Domain, p.SamAccountName)));
                grp.Dispose();
                ctx.Dispose();
            }
            return result;
        }
    }
}

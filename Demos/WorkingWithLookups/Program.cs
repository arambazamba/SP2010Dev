using System;
using System.Linq;
using Microsoft.SharePoint;

namespace WorkingWithLookups
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SPSite col = new SPSite("http://chiron");
            SPWeb web = col.AllWebs["Lookups"];
            SPList list = web.Lists["Person"];

            foreach (SPListItem item in list.Items)
            {
                SPFieldLookupValue job = new SPFieldLookupValue(item["Job"].ToString());
                string skills = GetSkills(item);
                Console.WriteLine("{0} works as a {1} with skills {2}", item["Title"], job.LookupValue, skills);
            }
        }

        private static string GetSkills(SPListItem Item)
        {
            string result = string.Empty;

            SPFieldLookupValueCollection skills = new SPFieldLookupValueCollection(Item["Skills"].ToString());
            return skills.Aggregate(result, (current, skill) => current + (skill.LookupValue + ";"));
        }
    }
}

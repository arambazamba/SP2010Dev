using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.SharePoint;

namespace WorkWithFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            SPSite site = new SPSite("http://chiron");
            SPWeb web = site.AllWebs["sales"];
            SPList list = web.Lists["MyDocs"];

            //Export a file
            if (list.ItemCount>0)
            {
                SPListItem item = list.Items[0];
                Debug.WriteLine(string.Format("Exporting: {0}", item.Name));
                Stream input = item.File.OpenBinaryStream();
                Stream target = File.Create(string.Format(@"c:\{0}", item.Name));

	            BinaryReader reader = new BinaryReader(input);
	            BinaryWriter writer = new BinaryWriter(target);
	            writer.Write(reader.ReadBytes((int)input.Length));
	            writer.Flush();
            }

            //Import a file - Hashtable holds Metadata
            const string path = @"c:\myword.docx";
            byte[] file = File.ReadAllBytes(path);

            Hashtable props = new Hashtable();
            props.Add("ContentType", "Document");
            props.Add("Title", "My super demo word file");
            props.Add("DocType", "Bericht");

            SPFolder folder = list.RootFolder;
            SPFile f = folder.Files.Add(path.Substring(3), file, props, true);
            //Change the checkout state
            if (f.CheckOutType == SPFile.SPCheckOutType.None)
            {
                //Check out to local drafts folder
                f.CheckOut(SPFile.SPCheckOutType.Offline, DateTime.Now.ToString());
            }
            else
            {
                //Check in a minor version
                f.CheckIn("Checking in a minor version", SPCheckinType.MinorCheckIn);
            }

            // Accessing File Versions
            foreach (SPFileVersion v in f.Versions)
            {
                Debug.WriteLine("Version: " + v.VersionLabel);
                SPFile fv = v.File;
                // If you want to dump this version repeat the pattern used above
            }

            //Filter items in a list using Caml Query
            string camlquery = @"<Where><Eq><FieldRef Name=""DocType"" /><Value Type=""Choice"">Bericht</Value></Eq></Where>";

            SPQuery query = new SPQuery {Query = camlquery};
            SPListItemCollection items = list.GetItems(query);
            foreach (SPListItem item in items)
            {
                Console.WriteLine("Found item {0} modified on {1}", item.Name, item["Modified"]);
            }

            // break rights inheritance - boolean: copy permissions
            list.BreakRoleInheritance(false);

            SPRoleDefinition PermLevelFull = list.ParentWeb.RoleDefinitions["Full Control"];
            SPGroup grp = list.ParentWeb.Groups["Smart Portal Members"];

            if (grp!=null && PermLevelFull!=null)
            {
                SPRoleAssignment assignment = new SPRoleAssignment(grp);
                assignment.RoleDefinitionBindings.Add(PermLevelFull);
                list.RoleAssignments.Add(assignment);
                list.Update();               
            }
        }
    }
}

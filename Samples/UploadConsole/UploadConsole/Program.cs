using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace UploadConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"c:\myword.docx";
            byte[] file = File.ReadAllBytes(path);
            Hashtable props = new Hashtable ();
            props.Add("ContentType", "Document");
            props.Add("Title", "My super demo word file");
            
            SPSite site = new SPSite("http://chiron");
            SPWeb web = site.RootWeb;
            SPList list = web.Lists["MyLib"];

            SPFolder folder = list.RootFolder;
            folder.Files.Add(path.Substring(3), file, props, true);

        }
    }
}

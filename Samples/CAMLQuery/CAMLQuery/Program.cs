using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace CAMLQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            string camlQuery = @"<Where><Eq><FieldRef Name=""Title""/><Value Type=""Text"">Simpson</Value></Eq></Where>";

            var col = new SPSite("http://chiron");
            SPWeb web = col.RootWeb;
            SPList list = web.Lists["Customers"];

            var query = new SPQuery {Query = camlQuery};

            SPListItemCollection filteredItems = list.GetItems(query);

            Debug.WriteLine(string.Format("Found {0} items in query.", filteredItems.Count));
            foreach (SPListItem item in filteredItems)
            {
                Debug.WriteLine(string.Format("Found item: {0} modiefied on {1}",
                                              item["Title"],
                                              item["Modified"]));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using RESTConsole.SmartPortal;

namespace RESTConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Service Reference
            //http://chiron/_vti_bin/listdata.svc


            SmartPortalDataContext dc = new SmartPortalDataContext(new Uri("http://chiron/_vti_bin/listdata.svc")){Credentials = CredentialCache.DefaultNetworkCredentials};

            var result = from d in dc.Inventory
                         select new
                         {
                             Title = d.Title,
                             Description = d.Description,
                             Cost = d.Cost,
                         };

            foreach (var d in result)
            {
                Console.WriteLine(string.Format("Item {0} costs {1}",d.Title,d.Cost));
            }

            //Insert

            InventoryItem newItem = new InventoryItem
                                        {
                                            Title = "Boat", 
                                            Description = "Little Yellow Boat", 
                                            Cost = 300
                                        };
            dc.AddToInventory(newItem);
            dc.SaveChanges();

            //Update
            
            InventoryItem item = dc.Inventory.FirstOrDefault(i => i.Title == "Car");
            if (item!=null)
            {
                item.Title = "Fast Car";
                item.Description = "Super Fast Car";
                item.Cost = 500;
                dc.UpdateObject(item);
                dc.SaveChanges();                
            }

            item = null;
            item = dc.Inventory.FirstOrDefault(i => i.Title == "Boat");
            if (item != null)
            {
                dc.DeleteObject(item);
                dc.SaveChanges();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Linq;

namespace SPLinqSample
{
    class Program
    {
        static void Main(string[] args)
        {

            SmartPortalDataContext dc = new SmartPortalDataContext("http://chiron");

            foreach (CustomersContact c in dc.Customers)
            {
                Console.WriteLine(c.Title);
            }

            var simpsons = from c in dc.Customers where c.Title == "Simpson" select c;
            
            TextWriter writer = new StreamWriter(@"c:\caml.txt", true);
            dc.Log = writer;

            foreach (CustomersContact c in simpsons)
            {
                Console.WriteLine(string.Format("{0} {1}",c.FirstName,c.Title));
            }

            CustomersContact nc = new CustomersContact() {Title = "Simpson", FirstName = "Lisa"};
            dc.Customers.InsertOnSubmit(nc);
            dc.SubmitChanges();

            CustomersContact homer = (from c in dc.Customers where c.FirstName == "Homer" select c).FirstOrDefault();
            homer.FirstName = "Homer J.";
            dc.SubmitChanges();

        }
    }
}

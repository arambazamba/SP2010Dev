using System;
using System.Linq;
using Microsoft.SharePoint;

namespace EventBindConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string evtReceiverClass = "";
            string assembly = "";

            SPSite site = new SPSite("http://chiron/");
            SPWeb web = site.RootWeb;

            SPList list = web.Lists["ListWhereToBindTheEvent"];

            list.EventReceivers.Add(SPEventReceiverType.ItemAdding, assembly, evtReceiverClass);

        }
    }
}

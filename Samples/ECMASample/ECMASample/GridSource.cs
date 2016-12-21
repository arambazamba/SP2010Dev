using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace ECMASample
{
    public static class GridSource
    {
        public static DataTable BindGrid()
        {
            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists["Contacts"];
            return list.Items.GetDataTable();
        }

        public static void Delete()
        {
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqConsole.Data
{
    public static class ILinqObjectExtensions
    {
        public static void Save(this ILinqObject obj)
        {
            LinqDemoDataContext dc = new LinqDemoDataContext();
            if (obj.ID==Guid.Empty)
            {
                obj.ID = Guid.NewGuid();
                dc.GetTable(obj.GetType()).InsertOnSubmit(obj);                
            }
            dc.SubmitChanges();
        }

        public static void Delete(this ILinqObject obj)
        {
            LinqDemoDataContext dc = new LinqDemoDataContext();
            if (obj.ID!=Guid.Empty)
            {
                dc.GetTable(obj.GetType()).DeleteOnSubmit(obj);
            }
            dc.SubmitChanges();
        }
    }
}

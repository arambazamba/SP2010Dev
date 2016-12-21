using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqConsole.Data;

namespace LinqConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Instanzierung des DataContexts
            LinqDemoDataContext dc = new LinqDemoDataContext();

            foreach (Employee emp in dc.Employees)
            {
                Console.WriteLine(emp.Name);
            }

            Employee ne = new Employee { Birthdate = DateTime.Now, Firstname = "Josef", Lastname = "Kunz" };
            ne.Save();

            Employee oe = (from e in dc.Employees where e.Firstname == "Hugo" select e).FirstOrDefault();
            if (oe != null)
            {
                oe.Firstname = "Hugobert";
                oe.Save();
            }

            foreach (Employee e in dc.Employees)
            {
               Skill s = new Skill { Name = "Führerschein", EmployeeID = e.ID };
               s.Save();
            }

            List<Employee> ewf =
                (from e in dc.Employees join s in dc.Skills on e.ID equals s.EmployeeID
                 where s.Name == "Führerschein" select e).ToList();
        }
    }
}

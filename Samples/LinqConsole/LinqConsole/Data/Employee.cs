using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqConsole.Data
{
    public partial class Employee : ILinqObject
    {
        public string Name
        {
            get { return Firstname + " " + Lastname; }
        }

        public int Age
        {
            get { return (DateTime.Now - Birthdate).Days/365; }
        }        
    }
}

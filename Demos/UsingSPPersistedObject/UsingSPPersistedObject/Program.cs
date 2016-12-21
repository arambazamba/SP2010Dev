using System;
using System.Linq;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace UsingSPPersistedObject
{
    class Program
    {
        static void Main(string[] args)
        {
            SPServer server = new SPServer("Chiron");
            Guid idForFuture;

            Counter counter = new Counter("d3", server.Farm) {Name = "class\\administstror", Password = "password"};

            idForFuture = counter.Id;
            counter.Update();

            Counter echo = (Counter)server.Farm.GetObject(idForFuture);
            echo.Name = "This is a test ";
            echo.Update();
            Console.WriteLine(echo.Name + echo.Password);
        }
    }

    public class Counter : SPPersistedObject
    {
        [Persisted]
        public string Password;

        [Persisted]
        public string Name;

        public Counter()
        {
        }

        public Counter(String name, SPPersistedObject parent) : base(name, parent)
        {

        }
    }
}

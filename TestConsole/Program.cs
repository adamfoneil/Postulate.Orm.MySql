using Postulate;
using Postulate.Orm.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestConsole.Models;

namespace TestConsole
{
    class Program
    {
        private static MySqlDb<int> db = new MySqlDb<int>("DefaultConnection");

        static void Main(string[] args)
        {
            var c = new Customer()
            {
                FirstName = "Fred",
                LastName = "Flinstone",
                Email = "fred.flinstone@nowhere.org",
                DiscountRate = 0.2m,
                EffectiveDate = new DateTime(1990, 1, 1)
            };
            db.Save(c);

            Console.WriteLine($"row created: {c.Id}");
            Console.ReadLine();
        }
    }
}

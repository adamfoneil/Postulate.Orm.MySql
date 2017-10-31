using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Postulate.Orm.MySql;
using AdamOneilSoftware;
using MySqlTest.Models;
using Dapper;
using System.Linq;

namespace MySqlTest
{
    [TestClass]
    public class Crud
    {
        private static MySqlDb<int> _db = new MySqlDb<int>("DefaultConnection");

        [TestMethod]
        public void TestInsert()
        {
            var tdg = new TestDataGenerator();
            tdg.Generate<Customer>(100, (c) =>
            {
                c.FirstName = tdg.Random(Source.FirstName);
                c.LastName = tdg.Random(Source.LastName);
                c.Email = $"{c.FirstName}.{c.LastName}@nowhere.org";
                c.DiscountRate = tdg.RandomInRange(0, 100, (v) => { return Convert.ToDecimal(v) * 0.01m; });
                c.EffectiveDate = tdg.RandomInRange(0, 1000, (v) => { return DateTime.Today.AddDays(v * -1); });
                c.Phone = tdg.Random(Source.USPhoneNumber);
            }, (records) =>
            {
                _db.SaveMultiple(records);                
            });            
        }

        [TestMethod]
        public void TestUpdate()
        {
            const string newName = "Updated";

            using (var cn = _db.GetConnection())
            {
                cn.Open();
                var latestIds = cn.Query<int>("SELECT `Id` FROM `customer` WHERE `LastName` <> @newName ORDER BY `Id` DESC LIMIT 10", new { newName = newName });
                foreach (var id in latestIds)
                {
                    var c = _db.Find<Customer>(cn, id);
                    c.LastName = newName;
                    c.Email = $"{c.FirstName}.{c.LastName}@nowhere.org";
                    _db.Save(c);
                }

                Assert.IsTrue(latestIds.All(id =>
                {
                    var c = _db.Find<Customer>(cn, id);
                    return c.LastName.Equals(newName);
                }));
            }
        }
    }
}

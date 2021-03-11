using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework.Internal;
using VueltoApi.Admin;
using VueltoApi.Context;
using VueltoApi.Models;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "Vuelto1")
            .Options;

            using (var context = new AppDbContext(options))
            {
                context.Banknotes.Add(new Banknotes { Id = 1, Value =  100});
                context.Banknotes.Add(new Banknotes { Id = 2, Value = 50 });
                context.Banknotes.Add(new Banknotes { Id = 3, Value = 20 });
                context.Banknotes.Add(new Banknotes { Id = 4, Value = 10 });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                context.Coins.Add(new Coin { Id = 1, Value = 0.50m });
                context.Coins.Add(new Coin { Id = 2, Value = 0.10m });
                context.Coins.Add(new Coin { Id = 3, Value = 0.05m });
                context.Coins.Add(new Coin { Id = 4, Value = 0.01m });

                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                SaleAdmin adm = new SaleAdmin(context);
                var result = adm.SaveSale(70, 100);

                Assert.AreEqual("Valor do Troco: R$ 30.00.\nEntregar:\n1 nota de R$20.00.\n1 nota de R$10.00.\n", result);
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "Vuelto2")
            .Options;

            using (var context = new AppDbContext(options))
            {
                context.Banknotes.Add(new Banknotes { Id = 1, Value = 100 });
                context.Banknotes.Add(new Banknotes { Id = 2, Value = 50 });
                context.Banknotes.Add(new Banknotes { Id = 3, Value = 20 });
                context.Banknotes.Add(new Banknotes { Id = 4, Value = 10 });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                context.Coins.Add(new Coin { Id = 1, Value = 0.50m });
                context.Coins.Add(new Coin { Id = 2, Value = 0.10m });
                context.Coins.Add(new Coin { Id = 3, Value = 0.05m });
                context.Coins.Add(new Coin { Id = 4, Value = 0.01m });

                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                SaleAdmin adm = new SaleAdmin(context);
                var result = adm.SaveSale(20, 100);

                Assert.AreEqual("Valor do Troco: R$ 80.00.\nEntregar:\n1 nota de R$50.00.\n1 nota de R$20.00.\n1 nota de R$10.00.\n", result);
            }
        }

        [TestMethod]
        public void TestMethod3()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "Vuelto3")
            .Options;

            using (var context = new AppDbContext(options))
            {
                context.Banknotes.Add(new Banknotes { Id = 1, Value = 100 });
                context.Banknotes.Add(new Banknotes { Id = 2, Value = 50 });
                context.Banknotes.Add(new Banknotes { Id = 3, Value = 20 });
                context.Banknotes.Add(new Banknotes { Id = 4, Value = 10 });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                context.Coins.Add(new Coin { Id = 1, Value = 0.50m });
                context.Coins.Add(new Coin { Id = 2, Value = 0.10m });
                context.Coins.Add(new Coin { Id = 3, Value = 0.05m });
                context.Coins.Add(new Coin { Id = 4, Value = 0.01m });

                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                SaleAdmin adm = new SaleAdmin(context);
                var result = adm.SaveSale(28.83m, 100);

                Assert.AreEqual("Valor do Troco: R$ 71.17.\nEntregar:\n1 nota de R$50.00.\n1 nota de R$20.00.\n2 moedas de R$0.50.\n1 moeda de R$0.10.\n1 moeda de R$0.05.\n2 moedas de R$0.01.\n", result);
            }
        }

        [TestMethod]
        public void TestMethod4()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "Vuelto4")
            .Options;        

            using (var context = new AppDbContext(options))
            {
                SaleAdmin adm = new SaleAdmin(context);
                var result = adm.SaveSale(125, 100);

                Assert.AreEqual("Total no puede ser mayor que monto pagado.", result);
            }
        }

        [TestMethod]
        public void TestMethod5()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "Vuelto5")
            .Options;        

            using (var context = new AppDbContext(options))
            {
                SaleAdmin adm = new SaleAdmin(context);
                var result = adm.SaveSale(-125, 100);

                Assert.AreEqual("No se aceptan valores negativos.", result);
            }
        }

        [TestMethod]
        public void TestMethod6()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "Vuelto6")
            .Options;         

            using (var context = new AppDbContext(options))
            {
                SaleAdmin adm = new SaleAdmin(context);
                var result = adm.SaveSale(0, 0);

                Assert.AreEqual("El total no puede ser 0.", result);
            }
        }

        [TestMethod]
        public void TestMethod7()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "Vuelto7")
            .Options;

            using (var context = new AppDbContext(options))
            {
                context.Banknotes.Add(new Banknotes { Id = 1, Value = 100 });
                context.Banknotes.Add(new Banknotes { Id = 2, Value = 50 });
                context.Banknotes.Add(new Banknotes { Id = 3, Value = 20 });
                context.Banknotes.Add(new Banknotes { Id = 4, Value = 10 });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                context.Coins.Add(new Coin { Id = 1, Value = 0.50m });
                context.Coins.Add(new Coin { Id = 2, Value = 0.10m });
                context.Coins.Add(new Coin { Id = 3, Value = 0.05m });
                context.Coins.Add(new Coin { Id = 4, Value = 0.01m });

                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                SaleAdmin adm = new SaleAdmin(context);
                var result = adm.SaveSale(4.33m, 5);

                Assert.AreEqual("Valor do Troco: R$ 0.67.\nEntregar:\n1 moeda de R$0.50.\n1 moeda de R$0.10.\n1 moeda de R$0.05.\n2 moedas de R$0.01.\n", result);
            }
        }
    }
}

using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            // Организация
            Wear wear1 = new Wear { WearId = 1, Name = "Wear1" };
            Wear wear2 = new Wear { WearId = 2, Name = "Wear2" };

            Cart cart = new Cart();

            // Действие
            cart.AddItem(wear1, 1);
            cart.AddItem(wear2, 1);
            List<CartLine> results = cart.Lines.ToList();

            // Утвержение
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Wear, wear1);
            Assert.AreEqual(results[1].Wear, wear2);
        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            // Организация
            Wear wear1 = new Wear { WearId = 1, Name = "Wear1" };
            Wear wear2 = new Wear { WearId = 2, Name = "Wear2" };


            Cart cart = new Cart();

            // Действие
            cart.AddItem(wear1, 1);
            cart.AddItem(wear2, 1);
            cart.AddItem(wear1, 5);
            List<CartLine> results = cart.Lines.OrderBy(c => c.Wear.WearId).ToList();

            // Утвержение
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Quantity, 6);
            Assert.AreEqual(results[1].Quantity, 1);
        }

        [TestMethod]
        public void Can_Remove_Line()
        {
            // Организация
            Wear wear1 = new Wear { WearId = 1, Name = "Wear1" };
            Wear wear2 = new Wear { WearId = 2, Name = "Wear2" };
            Wear wear3 = new Wear { WearId = 3, Name = "Wear3" };


            Cart cart = new Cart();

            // Действие
            cart.AddItem(wear1, 1);
            cart.AddItem(wear2, 1);
            cart.AddItem(wear1, 5);
            cart.AddItem(wear3, 2);
            cart.RemoveLine(wear2);

            // Утвержение
            Assert.AreEqual(cart.Lines.Where(c => c.Wear == wear2).Count(), 0);
            Assert.AreEqual(cart.Lines.Count(), 2);
        }

        [TestMethod]
        public void Calculate_Cart_Total()
        {
            // Организация
            Wear wear1 = new Wear { WearId = 1, Name = "Wear1", Price = 100 };
            Wear wear2 = new Wear { WearId = 2, Name = "Wear2", Price = 55 };

            Cart cart = new Cart();

            // Действие
            cart.AddItem(wear1, 1);
            cart.AddItem(wear2, 1);
            cart.AddItem(wear1, 5);
            decimal result = cart.ComputeTotalValue();

            // Утвержение
            Assert.AreEqual(result, 655);
        }

        [TestMethod]
        public void Can_Clear_Contents()
        {
            // Организация
            Wear wear1 = new Wear { WearId = 1, Name = "Wear1", Price = 100 };
            Wear wear2 = new Wear { WearId = 2, Name = "Wear2", Price = 50 };

            Cart cart = new Cart();

            // Действие
            cart.AddItem(wear1, 1);
            cart.AddItem(wear2, 1);
            cart.AddItem(wear1, 5);
            cart.Clear();

            // Утвержение
            Assert.AreEqual(cart.Lines.Count(), 0);
        }
    }
}

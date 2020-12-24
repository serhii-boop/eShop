using Domain.Abstract;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WebUI.Controllers;
using WebUI.Models;

namespace UnitTest
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void Can_Filter_Wears()
        {
            // Организация (arrange)
            Mock<IWearRepository> mock = new Mock<IWearRepository>();
            mock.Setup(m => m.Wears).Returns(new List<Wear>
            {
                new Wear{WearId = 1, Name = "Wear1", Category="Category1"},
                new Wear{WearId = 2, Name = "Wear2", Category="Category2"},
                new Wear{WearId = 3, Name = "Wear3", Category="Category1"},
                new Wear{WearId = 4, Name = "Wear4", Category="Category3"},
                new Wear{WearId = 5, Name = "Wear5", Category="Category2"}
            });

            WearsController controller = new WearsController(mock.Object);
            controller.pageSize = 3;

            // Действие (act)
            List<Wear> result = ((WearsListViewModel)controller.List("Category2", 1).Model).Wears.ToList();

            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].Name == "Wear2" && result[0].Category == "Category2");
            Assert.IsTrue(result[1].Name == "Wear5" && result[1].Category == "Category2");
        }


        [TestMethod]
        public void Can_Create_Categories()
        {
            // Организация (arrange)
            Mock<IWearRepository> mock = new Mock<IWearRepository>();
            mock.Setup(m => m.Wears).Returns(new List<Wear>
            {
                new Wear{WearId = 1, Name = "Wear1", Category="Category1"},
                new Wear{WearId = 2, Name = "Wear2", Category="Category2"},
                new Wear{WearId = 3, Name = "Wear3", Category="Category1"},
                new Wear{WearId = 4, Name = "Wear4", Category="Category3"},
                new Wear{WearId = 5, Name = "Wear5", Category="Category2"}
            });

            NavController target = new NavController(mock.Object);

            // Действие (act)
            List<string> result = ((IEnumerable<string>)target.Menu().Model).ToList();

            Assert.AreEqual(result.Count(), 3);
            Assert.AreEqual(result[0], "Category1");
            Assert.AreEqual(result[1], "Category2");
            Assert.AreEqual(result[2], "Category3");
        }


        [TestMethod]
        public void Generete_Category_Specific_Wear_Count()
        {
            // Организация (arrange)
            Mock<IWearRepository> mock = new Mock<IWearRepository>();
            mock.Setup(m => m.Wears).Returns(new List<Wear>
            {
                new Wear{WearId = 1, Name = "Wear1", Category="Category1"},
                new Wear{WearId = 2, Name = "Wear2", Category="Category2"},
                new Wear{WearId = 3, Name = "Wear3", Category="Category1"},
                new Wear{WearId = 4, Name = "Wear4", Category="Category3"},
                new Wear{WearId = 5, Name = "Wear5", Category="Category2"}
            });

            WearsController controller = new WearsController(mock.Object);
            controller.pageSize = 3;

            int res1 = ((WearsListViewModel)controller.List("Category1").Model).PagingInfo.TotalItems;
            int res2 = ((WearsListViewModel)controller.List("Category2").Model).PagingInfo.TotalItems;
            int res3 = ((WearsListViewModel)controller.List("Category3").Model).PagingInfo.TotalItems;
            int resAll = ((WearsListViewModel)controller.List(null).Model).PagingInfo.TotalItems;

            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }

    }
}

using Domain.Abstract;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebUI.Controllers;

namespace UnitTest
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Wears()
        {
            // Организация (arrange)
            Mock<IWearRepository> mock = new Mock<IWearRepository>();
            mock.Setup(m => m.Wears).Returns(new List<Wear>
            {
                new Wear{WearId = 1, Name = "Wear1"},
                new Wear{WearId = 2, Name = "Wear2"},
                new Wear{WearId = 3, Name = "Wear3"},
                new Wear{WearId = 4, Name = "Wear4"},
                new Wear{WearId = 5, Name = "Wear5"}
            });

            AdminController controller = new AdminController(mock.Object);

            // Действие (act)
            List<Wear> result = ((IEnumerable<Wear>)controller.Index().ViewData.Model).ToList();

            // Утверждение (assert)
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual(result[0].Name, "Wear1");
            Assert.AreEqual(result[1].Name, "Wear2");
        }

        [TestMethod]
        public void Can_Edit_Wear()
        {
            // Организация (arrange)
            Mock<IWearRepository> mock = new Mock<IWearRepository>();
            mock.Setup(m => m.Wears).Returns(new List<Wear>
            {
                new Wear{WearId = 1, Name = "Wear1"},
                new Wear{WearId = 2, Name = "Wear2"},
                new Wear{WearId = 3, Name = "Wear3"},
                new Wear{WearId = 4, Name = "Wear4"},
                new Wear{WearId = 5, Name = "Wear5"}
            });

            AdminController controller = new AdminController(mock.Object);

            // Действие (act)
            Wear wear1 = controller.Edit(1).ViewData.Model as Wear;
            Wear wear2 = controller.Edit(2).ViewData.Model as Wear;
            Wear wear3 = controller.Edit(3).ViewData.Model as Wear;

            // Утверждение (assert)
            Assert.AreEqual(1, wear1.WearId);
            Assert.AreEqual(2, wear2.WearId);
            Assert.AreEqual(3, wear3.WearId);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Wear()
        {
            // Организация (arrange)
            Mock<IWearRepository> mock = new Mock<IWearRepository>();
            mock.Setup(m => m.Wears).Returns(new List<Wear>
            {
                new Wear{WearId = 1, Name = "Wear1"},
                new Wear{WearId = 2, Name = "Wear2"},
                new Wear{WearId = 3, Name = "Wear3"},
                new Wear{WearId = 4, Name = "Wear4"},
                new Wear{WearId = 5, Name = "Wear5"}
            });

            AdminController controller = new AdminController(mock.Object);

            // Действие (act)
            Wear result = controller.Edit(7).ViewData.Model as Wear;

            // Утверждение (assert)
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            Mock<IWearRepository> mock = new Mock<IWearRepository>();
            AdminController controller = new AdminController(mock.Object);

            Wear wear = new Wear { Name = "Test" };

            ActionResult result = controller.Edit(wear);

            mock.Verify(m => m.SaveWear(wear));

            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Save_Invalid_Changes()
        {
            Mock<IWearRepository> mock = new Mock<IWearRepository>();
            AdminController controller = new AdminController(mock.Object);

            Wear wear = new Wear { Name = "Test" };

            controller.ModelState.AddModelError("error", "error");

            ActionResult result = controller.Edit(wear);

            mock.Verify(m => m.SaveWear(It.IsAny<Wear>()), Times.Never());

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using TimeTable.Controllers;

namespace TimeTable.Tests.Controllers {
    [TestClass]
    public class HomeControllerTest {

        [TestMethod]
        public void About() {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

    }
}

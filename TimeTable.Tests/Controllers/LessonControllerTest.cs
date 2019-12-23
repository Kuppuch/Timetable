using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeTable.Controllers;

namespace TimeTable.Tests.Controllers {
    [TestClass]
    public class LessonControllerTest {
        [TestMethod]
        public void TestMethod1() { }
            [TestMethod]
        public void IndexViewResultNotNull() {
            LessonController controller = new LessonController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexViewEqualIndexCshtml() {
            LessonController controller = new LessonController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void IndexStringInViewbag() {
            LessonController controller = new LessonController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.AreEqual("Hello world!", result.ViewBag.Message);
        }
    }
    
}

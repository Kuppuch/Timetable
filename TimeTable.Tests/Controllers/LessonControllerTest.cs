using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.Mvc;
using TimeTable.Controllers;
using TimeTable.Models;
using MySql.Data.MySqlClient;
//using NUnit.Framework;

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
        public void TestDetailsOrder() {
            Lesson lesson = new Lesson();


            using (var reader = (new MySqlCommand("SELECT * FROM less WHERE id = 1", DAO.DAO.connection)).ExecuteReader()) {
                while (reader.Read())
                    lesson = (new Lesson() {
                        Id = (int)reader["id"],
                        Discipline = (int)reader["discipline_id"],
                        Group = (int)reader["group_id"],
                        Teacher = (int)reader["teacher_id"],
                        DisciplineText = (string)reader["discipline"],
                        GroupText = (string)(reader["group"]) + (int)(reader["year"]),
                        TeacherText = (string)reader["teacher"]
                    });
            }

            Lesson lesson_test = new Lesson();
            lesson_test = (new Lesson() {
                Id = 1,
                Discipline = 1,
                Group = 1,
                Teacher = 10,
                DisciplineText = "Технологии программирования",
                GroupText = "ПРИ-1",
                TeacherText = "Вершинин В.В"
            });




            Assert.AreEqual(lesson,  lesson_test);


        }

        //[TestMethod]
        //public void IndexViewEqualIndexCshtml() {
        //    LessonController controller = new LessonController();

        //    ViewResult result = controller.Index() as ViewResult;

        //    Assert.AreEqual("Index", result.ViewName);
        //}

        //[TestMethod]
        //public void IndexStringInViewbag() {
        //    LessonController controller = new LessonController();

        //    ViewResult result = controller.Index() as ViewResult;

        //    Assert.AreEqual("Hello world!", result.ViewBag.Message);
        //}


    }

}

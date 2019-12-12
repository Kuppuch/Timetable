using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTable.Models;
using TimeTable.DAO;

namespace TimeTable {
    public struct LessionContainer {

        public List<Lesson> lessons;
        public List<Discipline> disciplines;
        public List<Group> groups;
        public List<User> users;
        public DAOLesson daoLesson;

        

        public List<Lesson> GetLessons() {
            return lessons;
        }
        public List<Discipline> GetDisciplines() {
            return disciplines;
        }

        public List<Group> GetGroups() {
            return groups;
        }

        public List<User> GetUsers() {
            return users;
        }

        public bool InsertLesson(Lesson l) {
            return daoLesson.InsertLesson(l);
        }

        public List<Lesson> GetLesson(int Id) {
            return lessons;
        }


    }
}
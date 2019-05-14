using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace University.Models
{
    public class Course
    {
        private int _id; 
        private string _courseName;
        private int _courseNumber;
        private List<Student> _students;

        public Course(string courseName, int courseNumber, int id = 0)
        {
            _id = id;
            _courseName = courseName;
            _courseNumber = courseNumber;
            _students = new List<Student>{};
        }
        public int GetId()
        {
            return _id;
        }
        public override int GetHashCode()
        {
            return this.GetId().GetHashCode();
        }
        public string GetCourseName()
        {
            return _courseName;
        }
        public void SetCourseName(string courseName)
        {
            _courseName = courseName;
        }
        public int GetCourseNumber()
        {
            return _courseNumber;
        }
        public void SetCourseNumber(int courseNumber)
        {
            _courseNumber = courseNumber;
        }
        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM course; DELETE FROM students_courses";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
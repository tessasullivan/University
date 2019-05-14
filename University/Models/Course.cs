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
            cmd.CommandText = @"DELETE FROM courses; DELETE FROM students_courses";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO courses (id, course_name, course_number) VALUES (@id, @name, @number);";
            MySqlParameter id = new MySqlParameter();
            id.ParameterName = "@id";
            id.Value = this._id;
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._courseName;
            MySqlParameter number = new MySqlParameter();
            number.ParameterName = "@number";
            number.Value = this._courseNumber;
            cmd.Parameters.Add(id);                      
            cmd.Parameters.Add(name);                      
            cmd.Parameters.Add(number);                      

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static Course Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM courses WHERE id = @thisId;";
            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int courseId = 0;
            string name = "";
            int number = 0;

            while (rdr.Read())
            {
                courseId = rdr.GetInt32(0);
                name = rdr.GetString(1);
                number = rdr.GetInt32(2);
            }

            Course foundCourse = new Course(name, number, courseId);

            conn.Close();

            if( conn != null )
            {
                conn.Dispose();
            }
            return foundCourse;
        }
        public override bool Equals(System.Object otherCourse)
        {
            if (!(otherCourse is Course))
            {
                return false;
            }
            else 
            {
                Course newCourse = (Course) otherCourse;
                bool idEquality = (this.GetId() == newCourse.GetId());
                bool nameEq = (this.GetCourseName() == newCourse.GetCourseName());
                bool numberEq = (this.GetCourseNumber() == newCourse.GetCourseNumber());
                return (idEquality && nameEq && numberEq);
            }
        }
    }
}
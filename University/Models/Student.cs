using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace University.Models
{
    public class Student
    {
        private int _id; 
        private string _firstName;
        private string _lastName;
        private DateTime _dateOfEnrollment;
        private List<Course> _courses;

        public Student(string firstName, string lastName, DateTime dateOfEnrollment, int id = 0)
        {
            _id = id;
            _firstName = firstName;
            _lastName = lastName;
            _dateOfEnrollment = dateOfEnrollment;
            _courses = new List<Course>{};
        }
        public int GetId()
        {
            return _id;
        }
        public override int GetHashCode()
        {
            return this.GetId().GetHashCode();
        }
        public string GetFirstName()
        {
            return _firstName;
        }
        public void SetFirstName(string firstName)
        {
         _firstName = firstName;
        }
            public string GetLastName()
        {
            return _lastName;
        }
        public void SetLastName(string lastName)
        {
            _lastName = lastName;
        }
        public DateTime GetDateOfEnrollment()
        {
            return _dateOfEnrollment;
        }
        public void SetDateOfEnrollment(DateTime dateOfEnrollment)
        {
            _dateOfEnrollment = dateOfEnrollment;
        }
        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM students; DELETE FROM students_courses";
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
            cmd.CommandText = @"INSERT INTO students(id, first_name, last_name, date_of_enrollment) VALUES 
                (@id, @firstName, @lastName, @dateOfEnrollment);";
            MySqlParameter id = new MySqlParameter();
            id.ParameterName = "@id";
            id.Value = this._id;
            MySqlParameter firstName = new MySqlParameter();
            firstName.ParameterName = "@firstName";
            firstName.Value = this._firstName;
            MySqlParameter lastName = new MySqlParameter();
            lastName.ParameterName = "@lastName";
            lastName.Value = this._lastName;
            MySqlParameter dateOfEnrollment = new MySqlParameter();
            dateOfEnrollment.ParameterName = "@dateOfEnrollment";
            dateOfEnrollment.Value = this._dateOfEnrollment;
            cmd.Parameters.Add(id);                      
            cmd.Parameters.Add(firstName);                      
            cmd.Parameters.Add(lastName);
            cmd.Parameters.Add(dateOfEnrollment);
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static Student Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM students WHERE id = @thisId;";
            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int studentId = 0;
            string firstName = "";
            string lastName = "";
            DateTime dateOfEnrollment = new DateTime();

            while (rdr.Read())
            {
                studentId = rdr.GetInt32(0);
                firstName = rdr.GetString(1);
                lastName = rdr.GetString(2);
                dateOfEnrollment = rdr.GetDateTime(3);
            }

            Student foundStudent = new Student(firstName, lastName, dateOfEnrollment, studentId);

            conn.Close();

            if( conn != null )
            {
                conn.Dispose();
            }
            return foundStudent;
        }
        public static List<Student> GetAll()
        {
            List<Student> allStudents = new List<Student> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM students;";
 
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int studentId = 0;
            string firstName = "";
            string lastName = "";
            DateTime dateOfEnrollment = new DateTime();

            while (rdr.Read())
            {
                studentId = rdr.GetInt32(0);
                firstName = rdr.GetString(1);
                lastName = rdr.GetString(2);
                dateOfEnrollment = rdr.GetDateTime(3);
                Student foundStudent = new Student(firstName, lastName, dateOfEnrollment, studentId);
                allStudents.Add(foundStudent);
            }
            conn.Close();

            if( conn != null )
            {
                conn.Dispose();
            }
            return allStudents;
        }        
        public List<Course> GetCourses()
        {
            List<Course> courses = new List<Course> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT courses.* FROM students
                JOIN students_courses ON (students.id = students_courses.student_id)
                JOIN courses ON (students_courses.courses_id = courses.id)
                WHERE students.id = @studentId;";
            MySqlParameter studentId = new MySqlParameter();
            studentId.ParameterName = "@studentId";
            studentId.Value = _id;
            cmd.Parameters.Add(studentId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string courseName = rdr.GetString(1);
                int courseNumber = rdr.GetInt32(2);
                Course foundCourse = new Course(courseName, courseNumber, id);
                courses.Add(foundCourse);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return courses;            
        }

        public override bool Equals(System.Object otherStudent)
        {
            if (!(otherStudent is Student))
        {
            return false;
        }
        else 
        {
            Student newStudent = (Student) otherStudent;
            bool idEquality = (this.GetId() == newStudent.GetId());
            bool firstNameEq = (this.GetFirstName() == newStudent.GetFirstName());
            bool lastNameEq = (this.GetLastName() == newStudent.GetLastName());
            bool dateOfEnrollmentEq = (this.GetDateOfEnrollment() == newStudent.GetDateOfEnrollment());
            return (idEquality && firstNameEq && lastNameEq && dateOfEnrollmentEq);
        }
    }
    }
}
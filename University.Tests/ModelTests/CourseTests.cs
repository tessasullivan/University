using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using University.Models;

namespace University.Tests
{
  [TestClass]
  public class CourseTest : IDisposable
  {
    public CourseTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=university_test;convert zero datetime=True;";
    }
    public void Dispose()
    {
      Course.DeleteAll();
    }
    [TestMethod]
    public void Equals_ReturnsTrueIfDetailsAreTheSame_True()
    {
      int courseId = 1;
      string courseName = "HIST";
      int courseNumber = 101;
      Course addedCourse = new Course(courseName, courseNumber, courseId);
      int courseId2 = 1;
      string courseName2 = "HIST";
      int courseNumber2 = 101;
      Course addedCourse2 = new Course(courseName2, courseNumber2, courseId2);
      Assert.AreEqual(addedCourse, addedCourse2);      
    }
    [TestMethod]
    public void Find_ReturnsCorrectCourseFromDB_Student()
    {
      int courseId = 1;
      string courseName = "HIST";
      int courseNumber = 101;
      Course addedCourse = new Course(courseName, courseNumber, courseId);
      addedCourse.Save();
      Course foundCourse = Course.Find(addedCourse.GetId());
      System.Console.WriteLine("GetId "+ addedCourse.GetId());
      System.Console.WriteLine("CourseName "+ addedCourse.GetCourseName());
      System.Console.WriteLine("CourseNumber "+ addedCourse.GetCourseNumber());
      System.Console.WriteLine("GetId "+ foundCourse.GetId());
      System.Console.WriteLine("CourseName "+ foundCourse.GetCourseName());
      System.Console.WriteLine("CourseNumber "+ foundCourse.GetCourseNumber());      
      Assert.AreEqual(addedCourse, foundCourse);      
    }
  }
}
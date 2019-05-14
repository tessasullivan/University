using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using University.Models;

namespace University.Tests
{
  [TestClass]
  public class StudentTest : IDisposable
  {
    public StudentTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=university_test;convert zero datetime=True;";
    }
    public void Dispose()
    {
      Student.DeleteAll();
    }
    [TestMethod]
    public void StudentConstructor_CreatesInstanceOfStudent_Student()
    {
      int id=444556666;
      string firstName = "Sylvia";
      string lastName = "Green";
      DateTime dateOfEnrollment = new DateTime(2019, 05, 06);
      Student newStudent = new Student(firstName, lastName, dateOfEnrollment, id);
      Assert.AreEqual(typeof(Student), newStudent.GetType());
    }
    [TestMethod]
    public void Find_ReturnsCorrectStudentFromDB_Student()
    {
      int id=444556666;
      string firstName = "Sylvia";
      string lastName = "Green";
      DateTime dateOfEnrollment = new DateTime(2019, 05, 06);
      Student newStudent = new Student(firstName, lastName, dateOfEnrollment, id);
      newStudent.Save();
      Student foundStudent = Student.Find(newStudent.GetId());
      Assert.AreEqual(newStudent, foundStudent);      
    }
    [TestMethod]
    public void GetAll_ReturnsAllStudents_StudentList()
    {
      int id1=444556666;
      string firstName1 = "Sylvia";
      string lastName1 = "Green";
      DateTime dateOfEnrollment1 = new DateTime(2019, 05, 06);
      Student newStudent1 = new Student(firstName1, lastName1, dateOfEnrollment1, id1);
      newStudent1.Save();
      int id2=867530900;
      string firstName2 = "Justin";
      string lastName2 = "Johnson";
      DateTime dateOfEnrollment2 = new DateTime(2019, 05, 06);
      Student newStudent2 = new Student(firstName2, lastName2, dateOfEnrollment2, id2);
      newStudent2.Save();

      List<Student> expectedResult = new List<Student> {newStudent1, newStudent2};
      List<Student> actualResult = Student.GetAll();
      CollectionAssert.AreEqual(expectedResult, actualResult);   
    }
  }
}
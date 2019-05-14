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
  }
}
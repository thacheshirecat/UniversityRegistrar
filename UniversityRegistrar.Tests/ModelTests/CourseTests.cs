using System;
using UniversityRegistrar.Models;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversityRegistrar.Tests
{
  [TestClass]
  public class CourseTests : IDisposable
  {
    public CourseTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=universityregistrar_test;";
    }
    public void Dispose()
    {
      Student.DeleteAll();
      Course.DeleteAll();
    }
    [TestMethod]
    public void Save_SavesToDB_CourseList()
    {
      Course testCourse1 = new Course("Math", "MA101");
      List<Course> resultList = new List<Course> {testCourse1};

      testCourse1.Save();
      List<Course> testList = Course.GetAll();

      CollectionAssert.AreEqual(resultList, testList);

    }
    [TestMethod]
    public void GetAll_ReturnsAllCoursesInDB_CourseList()
    {
      Course testCourse1 = new Course("Math", "MA101");
      Course testCourse2 = new Course("Science", "SCI101");
      List<Course> resultList = new List<Course> {testCourse1, testCourse2};

      testCourse1.Save();
      testCourse2.Save();
      List<Course> testList = Course.GetAll();

      CollectionAssert.AreEqual(resultList, testList);
    }
  }
}

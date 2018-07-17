using System;
using UniversityRegistrar.Models;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversityRegistrar.Tests
{
  [TestClass]
  public class StudentTests : IDisposable
  {
    public StudentTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=universityregistrar_test;";
    }
    public void Dispose()
    {
      Student.DeleteAll();
      Course.DeleteAll();
    }
    [TestMethod]
    public void Save_SavesToDB_StudentList()
    {
      Student testStudent1 = new Student("Tom", "7/1/2017");
      List<Student> resultList = new List<Student> {testStudent1};

      testStudent1.Save();
      List<Student> testList = Student.GetAll();

      CollectionAssert.AreEqual(resultList, testList);

    }
    [TestMethod]
    public void GetAll_ReturnsAllStudentsInDB_StudentList()
    {
      Student testStudent1 = new Student("Tom", "7/1/2017");
      Student testStudent2 = new Student("Max", "7/14/2017");
      List<Student> resultList = new List<Student> {testStudent1, testStudent2};

      testStudent1.Save();
      testStudent2.Save();
      List<Student> testList = Student.GetAll();

      CollectionAssert.AreEqual(resultList, testList);
    }
    [TestMethod]
    public void Find_ReturnsCorrectStudent_Student()
    {
      Student testStudent1 = new Student("Tom", "7/1/2017");
      Student testStudent2 = new Student("Max", "7/14/2017");

      testStudent1.Save();
      testStudent2.Save();
      Student resultStudent = Student.Find(testStudent1.GetId());

      Assert.AreEqual(testStudent1, resultStudent);
    }
  }
}

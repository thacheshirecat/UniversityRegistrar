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
    [TestMethod]
    public void AddCourse_CorrectlyAttatchesCourseToStudent_Course()
    {
      Student testStudent1 = new Student("Tom", "7/1/2017");
      Course testCourse1 = new Course("Math", "MA101");
      List<Course> testList = new List<Course> {testCourse1};

      testStudent1.AddCourse(testCourse1);
      List<Course> resultList = testStudent1.GetAllCourses();

      CollectionAssert.AreEqual(testList, resultList);
    }
    [TestMethod]
    public void GetAllCourses_ReturnsAllCoursesAttachedToStudent_CourseList()
    {
      Student testStudent1 = new Student("Tom", "7/1/2017");
      Course testCourse1 = new Course("Math", "MA101");
      Course testCourse2 = new Course("Science", "SCI101");
      List<Course> testList = new List<Course> {testCourse1, testCourse2};

      testStudent1.AddCourse(testCourse1);
      testStudent1.AddCourse(testCourse2);
      List<Course> resultList = testStudent1.GetAllCourses();

      CollectionAssert.AreEqual(testList, resultList);
    }
  }
}

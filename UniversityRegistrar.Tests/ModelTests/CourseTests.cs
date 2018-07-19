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
    [TestMethod]
    public void Find_ReturnsCorrectCourse_Course()
    {
      Course testCourse1 = new Course("Math", "MA101");
      Course testCourse2 = new Course("Science", "SCI101");

      testCourse1.Save();
      testCourse2.Save();
      Course resultCourse = Course.Find(testCourse1.GetId());

      Assert.AreEqual(testCourse1, resultCourse);
    }
    [TestMethod]
    public void AddStudent_CorrectlyAttatchesStudentToCourse_StudentList()
    {
      Course testCourse = new Course("Pre-Corn", "PRCN100");
      testCourse.Save();
      Student testStudent1 = new Student("Tom", "7/1/2017");
      testStudent1.Save();

      testCourse.AddStudent(testStudent1);
      List<Student> testList = testCourse.GetAllStudents();
      List<Student> resultList = new List<Student> {testStudent1};

      CollectionAssert.AreEqual(resultList, testList);
    }
    [TestMethod]
    public void GetAllStudents_ReturnsAllStudentsAttachedToCourse_StudentList()
    {
      Course testCourse = new Course("Pre-Corn", "PRCN100");
      testCourse.Save();
      Student testStudent1 = new Student("Tom", "7/1/2017");
      Student testStudent2 = new Student("Max", "7/14/2017");
      Student testStudent3 = new Student("Scavvy", "6/31/2017");
      testStudent1.Save();
      testStudent2.Save();
      testStudent3.Save();

      testCourse.AddStudent(testStudent1);
      testCourse.AddStudent(testStudent2);
      List<Student> testList = testCourse.GetAllStudents();
      List<Student> resultList = new List<Student> {testStudent1, testStudent2};

      CollectionAssert.AreEqual(resultList, testList);
    }
  }
}

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
    public void AddCourse_CorrectlyAttatchesCourseToStudent_CourseList()
    {
      Student testStudent1 = new Student("Tom", "7/1/2017");
      testStudent1.Save();
      Course testCourse1 = new Course("Math", "MA101");
      testCourse1.Save();
      List<Course> testList = new List<Course> {testCourse1};

      testStudent1.AddCourse(testCourse1);
      List<Course> resultList = testStudent1.GetAllCourses();

      CollectionAssert.AreEqual(testList, resultList);
    }
    [TestMethod]
    public void GetAllCourses_ReturnsAllCoursesAttachedToStudent_CourseList()
    {
      Student testStudent1 = new Student("Tom", "7/1/2017");
      testStudent1.Save();
      Course testCourse1 = new Course("Math", "MA101");
      testCourse1.Save();
      Course testCourse2 = new Course("Science", "SCI101");
      testCourse2.Save();
      List<Course> testList = new List<Course> {testCourse1, testCourse2};

      testStudent1.AddCourse(testCourse1);
      testStudent1.AddCourse(testCourse2);
      List<Course> resultList = testStudent1.GetAllCourses();

      CollectionAssert.AreEqual(testList, resultList);
    }
    [TestMethod]
    public void Delete_DeletesOnlySpecifiedStudent_StudentList()
    {
      Student testStudent1 = new Student("Tom", "7/1/2017");
      Student testStudent2 = new Student("Max", "7/14/2017");

      testStudent1.Save();
      testStudent2.Save();
      testStudent2.Delete();
      List<Student> testList = new List<Student> {testStudent1};
      List<Student> resultList = Student.GetAll();

      CollectionAssert.AreEqual(testList, resultList);
    }
    [TestMethod]
    public void Update_CorrectlyUpdatesStudentName_String()
    {
      Student testStudent = new Student("Tom", "7/1/2017");
      testStudent.Save();
      Student resultStudent = new Student("Max", "7/14/2017");
      resultStudent.Save();

      testStudent.Update("Max", "7/1/2017");
      Student controlStudent = Student.Find(testStudent.GetId());
      string test = controlStudent.GetName();
      string result = resultStudent.GetName();

      Assert.AreEqual(result, test);
    }
  }
}

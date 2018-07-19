using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniversityRegistrar.Models;

namespace UniversityRegistrar.Controllers
{
  public class CourseController : Controller
  {
    [HttpGet("/Courses")]
    public ActionResult Index()
    {
      return View(Course.GetAll());
    }
    [HttpGet("/Courses/Add")]
    public ActionResult CreateForm()
    {
      return View();
    }
    [HttpPost("/Courses/Success")]
    public ActionResult AddCourse(string newname, string newcode)
    {
      Course newCourse = new Course(newname, newcode);

      newCourse.Save();

      return View("Index", Course.GetAll());
    }
    [HttpGet("/Courses/{id}/View")]
    public ActionResult ViewCourse(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Course selectedCourse = Course.Find(id);
      List<Student> courseStudents = selectedCourse.GetAllStudents();
      List<Student> allStudents = Student.GetAll();

      model.Add("course", selectedCourse);
      model.Add("students", courseStudents);
      model.Add("allstudents", allStudents);

      return View("View", model);
    }
    [HttpPost("/Course/AddStudent/")]
    public ActionResult AddStudentToCourse(int newstudent, int courseid)
    {
      Course thisCourse = Course.Find(courseid);
      Student thisStudent = Student.Find(newstudent);

      thisCourse.AddStudent(thisStudent);

      return View("Success");
    }
    [HttpGet("/Courses/{id}/Remove")]
    public ActionResult DeleteStudent(int id)
    {
      Course foundCourse = Course.Find(id);

      foundCourse.Delete();

      return View("Success");
    }
  }
}

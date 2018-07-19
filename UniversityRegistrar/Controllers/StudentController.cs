using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UniversityRegistrar.Models;

namespace UniversityRegistrar.Controllers
{
  public class StudentController : Controller
  {
    [HttpGet("/Students")]
    public ActionResult Index()
    {
      return View(Student.GetAll());
    }
    [HttpGet("/Students/Add")]
    public ActionResult CreateForm()
    {
      return View();
    }
    [HttpPost("/Students/Success")]
    public ActionResult AddStudent(string newname, string newenrollment)
    {
      Student newStudent = new Student(newname, newenrollment);

      newStudent.Save();

      return View("Index", Student.GetAll());
    }
    [HttpGet("/Students/{id}/View")]
    public ActionResult ViewStudent(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Student selectedStudent = Student.Find(id);
      List<Course> studentCourses = selectedStudent.GetAllCourses();
      List<Course> allCourses = Course.GetAll();

      model.Add("student", selectedStudent);
      model.Add("courses", studentCourses);
      model.Add("allcourses", allCourses);

      return View("View", model);
    }
    [HttpPost("/Student/AddCourse/")]
    public ActionResult AddCourse(int studentid, int newcourse)
    {
      Student foundStudent = Student.Find(studentid);
      Course newCourse = Course.Find(newcourse);

      foundStudent.AddCourse(newCourse);

      return View("Success");
    }
  }
}

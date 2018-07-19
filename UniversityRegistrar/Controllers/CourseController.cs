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
  }
}

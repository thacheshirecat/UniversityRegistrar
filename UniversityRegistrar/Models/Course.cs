using System;
using UniversityRegistrar;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace UniversityRegistrar.Models
{
  public class Course
  {
    private int _id;
    private string _name;
    private string _code;

    public Course(string newName, string newCode, int id = 0)
    {
      _name = newName;
      _code = newCode;
      _id = id;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public string GetCode()
    {
      return _code;
    }
    public override bool Equals(System.Object otherObject)
    {
      if(!(otherObject is Course))
      {
        return false;
      }
      else
      {
        Course newCourse = (Course) otherObject;
        bool IdEquality = (this.GetId() == newCourse.GetId());
        bool NameEquality = (this.GetName() == newCourse.GetName());
        bool CodeEquality = (this.GetCode() == newCourse.GetCode());
        return (IdEquality && NameEquality && CodeEquality);
      }
    }
    public override int GetHasCode()
    {
      this.GetName().GetHasCode();
    }
    public void Save()
    {

    }
    public List<Student> GetAll()
    {

    }
    public void DeleteAll()
    {

    }

  }
}

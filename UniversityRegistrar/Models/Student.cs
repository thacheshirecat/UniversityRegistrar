using System;
using UniversityRegistrar;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace UniversityRegistrar.Models
{
  public class Student
  {
    private int _id;
    private string _name;
    private string _enrollment;

    public Student(string newName, string newEnrollment, int id = 0)
    {
      _name = newName;
      _enrollment = newEnrollment;
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
    public string GetEnrollment()
    {
      return _enrollment;
    }
    public override bool Equals(System.Object otherObject)
    {
      if(!(otherObject is Student))
      {
        return false;
      }
      else
      {
        Student newStudent = (Student) otherObject;
        bool IdEquality = (this.GetId() == newStudent.GetId());
        bool NameEquality = (this.GetName() == newStudent.GetName());
        bool EnrollmentEquality = (this.GetEnrollment() == newStudent.GetEnrollment());
        return (IdEquality && NameEquality && EnrollmentEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }
    public void Save()
    {

    }
    public static List<Student> GetAll()
    {
      List<Student> nullList = new List<Student>{};
      return nullList;
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM students; DELETE FROM students_courses;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }


  }
}

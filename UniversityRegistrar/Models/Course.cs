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
    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO courses (name, code) VALUES (@CourseName, @CourseCode)";

      cmd.Parameters.Add(new MySqlParameter("@CourseName", _name));
      cmd.Parameters.Add(new MySqlParameter("@CourseCode", _code));
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Course> GetAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM courses;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      List<Course> allCourses = new List<Course>{};
      while(rdr.Read())
      {
        int CourseId = rdr.GetInt32(0);
        string CourseName = rdr.GetString(1);
        string CourseCode = rdr.GetString(2);
        Course newCourse = new Course(CourseName, CourseCode, CourseId);
        allCourses.Add(newCourse);
      }

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allCourses;
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM courses; DELETE FROM students_courses;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public static Course Find(int id)
    {
      Course foundCourse = new Course("null", "null");
      return foundCourse;
    }
  }
}

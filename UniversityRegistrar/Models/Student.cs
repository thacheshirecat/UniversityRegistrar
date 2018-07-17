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
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO students (name, enrollment) VALUES (@StudentName, @StudentEnrollment)";

      cmd.Parameters.Add(new MySqlParameter("@StudentName", _name));
      cmd.Parameters.Add(new MySqlParameter("@StudentEnrollment", _enrollment));
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Student> GetAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM students;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      List<Student> allStudents = new List<Student>{};
      while(rdr.Read())
      {
        int StudentId = rdr.GetInt32(0);
        string StudentName = rdr.GetString(1);
        string StudentEnrollment = rdr.GetString(2);
        Student newStudent = new Student(StudentName, StudentEnrollment, StudentId);
        allStudents.Add(newStudent);
      }

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allStudents;
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

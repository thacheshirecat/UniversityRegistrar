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
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM courses WHERE id = @CourseId;";

      cmd.Parameters.Add(new MySqlParameter("@CourseId", id));

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int CourseId = 0;
      string CourseName = "";
      string CourseCode = "";

      while(rdr.Read())
      {
        CourseId = rdr.GetInt32(0);
        CourseName = rdr.GetString(1);
        CourseCode = rdr.GetString(2);
      }

      Course foundCourse = new Course(CourseName, CourseCode, CourseId);

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return foundCourse;
    }
    public void AddStudent(Student newStudent)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO students_courses (student_id, course_id) VALUES (@StudentId, @CourseId);";

      cmd.Parameters.Add(new MySqlParameter("@StudentId", newStudent.GetId()));
      cmd.Parameters.Add(new MySqlParameter("@CourseId", _id));

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public List<Student> GetAllStudents()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT students.* FROM courses
                          JOIN students_courses ON (courses.id = students_courses.course_id)
                          JOIN students ON (students_courses.student_id = students.id)
                          WHERE courses.id = @CourseId;";

      cmd.Parameters.Add(new MySqlParameter("@CourseId", _id));

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Student> allStudents = new List<Student> {};

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
    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM courses WHERE id = @CourseId;DELETE FROM students_courses WHERE course_id = @CourseId;";

      cmd.Parameters.Add(new MySqlParameter("@CourseId", _id));

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public void Update(string newName, string newCode)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE courses SET name = @CourseName, code = @CourseCode WHERE id = @CourseId;";

      cmd.Parameters.Add(new MySqlParameter("@CourseId", _id));
      cmd.Parameters.Add(new MySqlParameter("@CourseName", newName));
      cmd.Parameters.Add(new MySqlParameter("@CourseCode", newCode));

      cmd.ExecuteNonQuery();
      _name = newName;
      _enrollment = newEnrollment;

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
  }
}

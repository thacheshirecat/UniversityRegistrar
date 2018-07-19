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
    public static Student Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM students WHERE id = @StudentId;";

      cmd.Parameters.Add(new MySqlParameter("@StudentId", id));

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int StudentId = 0;
      string StudentName = "";
      string StudentEnrollment = "";

      while(rdr.Read())
      {
        StudentId = rdr.GetInt32(0);
        StudentName = rdr.GetString(1);
        StudentEnrollment = rdr.GetString(2);
      }

      Student foundStudent = new Student(StudentName, StudentEnrollment, StudentId);

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }

      return foundStudent;
    }
    public void AddCourse(Course newCourse)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO students_courses (student_id, course_id) VALUES (@StudentId, @CourseId);";

      cmd.Parameters.Add(new MySqlParameter("@StudentId", _id));
      cmd.Parameters.Add(new MySqlParameter("@CourseId", newCourse.GetId()));

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public List<Course> GetAllCourses()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT courses.* FROM students
                          JOIN students_courses ON (students.id = students_courses.student_id)
                          JOIN courses ON (students_courses.course_id = courses.id)
                          WHERE students.id = @StudentId;";

      cmd.Parameters.Add(new MySqlParameter("@StudentId", _id));

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Course> allCourses = new List<Course> {};

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
    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM students WHERE id = @StudentId;DELETE FROM students_courses WHERE student_id = @StudentId;";

      cmd.Parameters.Add(new MySqlParameter("@StudentId", _id));

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public void Update(string newName, string newEnrollment)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE students SET name = @StudentName, enrollment = @StudentEnrollment WHERE id = @StudentId;";

      cmd.Parameters.Add(new MySqlParameter("@StudentId", _id));
      cmd.Parameters.Add(new MySqlParameter("@StudentName", newName));
      cmd.Parameters.Add(new MySqlParameter("@StudentEnrollment", newEnrollment));

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

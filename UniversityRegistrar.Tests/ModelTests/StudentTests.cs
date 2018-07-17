using System;
using UniversityRegistrar.Models;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversityRegistrar.Tests
{
  [TestClass]
  public class StudentTest : IDisposable
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
  }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoGrader.Models;
using AutoGrader.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace AutoGrader.DataAccess
{
    public class StudentDataService : Service
    {
        public StudentDataService(AutoGraderDbContext dbContext) : base(dbContext) { }

        public IEnumerable<Student> GetStudents()
        {
            return autoGraderDbContext.Students.ToList();
        }

        public Student GetStudentById(int id)
        {
            return autoGraderDbContext.Students
                .Include(s => s.StudentClasses)
                .FirstOrDefault(e => e.Id == id);
        }

        public Student GetStudentByUsername(string username)
        {
            return GetStudents().FirstOrDefault(e => e.UserName == username);
        }

        public void AddStudent(Student student)
        {
            autoGraderDbContext.Students.Add(student);
        }
    }
}

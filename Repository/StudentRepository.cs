using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Aplication.Data;
using API_Aplication.Model;
using Microsoft.EntityFrameworkCore;

namespace API_Aplication.Repository
{
    public interface IStudentRepository
    {
        Task<Student> CreateStudent(Student student);
        Task<string> DeleteStudent(Student student);
        Student GetStudentById(int StudentId);
        Task<List<Student>> GetStudents();
        Task<Student> UpdateStudent(Student student);
    }

    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public StudentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Create or insert Student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public async Task<Student> CreateStudent(Student student)
        {
            _dbContext.Student.Add(student);
            int res = await _dbContext.SaveChangesAsync();
            if (res > 0)
            {
                return student;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Read or Get All Students
        /// </summary>
        /// <returns></returns>
        public async Task<List<Student>> GetStudents()
        {
            List<Student> students = await _dbContext.Student.ToListAsync();
            return students;
        }

        /// <summary>
        /// Read A Students
        /// </summary>
        /// <returns></returns>
        public Student GetStudentById(int StudentId)
        {
            return _dbContext.Student.Find(StudentId);
        }

        /// <summary>
        /// Update A Student
        /// </summary>
        /// <returns></returns>
        public async Task<Student> UpdateStudent(Student student)
        {
            _dbContext.Entry(student).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return student;
        }

        /// <summary>
        /// Delete A Students
        /// </summary>
        /// <returns></returns>
        public async Task<string> DeleteStudent(Student student)
        {
            _dbContext.Student.Remove(student);
            await _dbContext.SaveChangesAsync();
            return null;
        }

    }
}

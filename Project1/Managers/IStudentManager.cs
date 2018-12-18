using System.Collections.Generic;
using Project1.Models;

namespace Project1.Managers
{
    public interface IStudentManager
    {
        Student Create(Student user);
        void Delete(string id);
        Student GetStudent(string id);
        List<Student> GetStudents(string id);
        void Update(Student student);
    }
}
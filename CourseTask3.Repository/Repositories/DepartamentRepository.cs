using CourseTask3.Repository.Interfaces;
using CourseTask3.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseTask3.Repository.Repositories
{
    public class DepartamentRepository : IDepartamentRepository
    {
        private readonly CourseTaskDbContext _dbContext;
        public DepartamentRepository(CourseTaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Departament GetDepartamentByID(int id)
        {
            Departament departament = _dbContext.Departaments.Include("Students").Include("Courses").ToList().Where<Departament>(x => x.ID == id).FirstOrDefault();
            if (departament == null)
            {
                Console.WriteLine($"Departament ID {id} does not exist.");
            }
            return departament;
        }
        public void AddNewDepartament(string name)
        {
            Departament departament = new Departament(name);
            var departamentList = _dbContext.Departaments.Where<Departament>(x => x.Name == name).FirstOrDefault();
            if (departamentList != null)
            {
                Console.WriteLine($"Course {departament.Name} already exists.");
            }
            else
            {
                _dbContext.Departaments.Add(departament);
                _dbContext.SaveChanges();
            };
        }
        public List<Student> GetAllStudentsInDepartament(string departamentName)
        {
            Departament departament = _dbContext.Departaments.Include("Students").ToList().Where<Departament>(x => x.Name == departamentName).FirstOrDefault();
            var students = departament.Students;
            if (departament == null)
            {
                Console.WriteLine($"Departament {departamentName} does not exist.");
            }
            List<Student> studentslist = new List<Student>();
            foreach (var student in departament.Students)
            {
                studentslist.Add(student);
            }
            return studentslist;
        }
        public void RemoveDepartament(string departamentName)
        {
            Departament departament = _dbContext.Departaments.Include("Students").Include("Courses").ToList().Where<Departament>(x => x.Name == departamentName).FirstOrDefault();
            if (departament == null)
            {
                Console.WriteLine($"Course {departamentName} does not exist.");
            }
            else
            {
                foreach (var course in departament.Courses)
                {
                    if (course.Departaments.Contains(departament))
                    {
                        course.Departaments.Remove(departament);
                    };
                };
                foreach (var student in departament.Students)
                {
                    student.Departament = null; // change to relocate students to other departaments later
                }
                _dbContext.Departaments.Where<Departament>(x => x.Name == departamentName).ExecuteDelete();
                _dbContext.SaveChanges();
            };
        }
    }
}

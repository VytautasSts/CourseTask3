using CourseTask3.Repository.Interfaces;
using CourseTask3.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseTask3.Repository.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CourseTaskDbContext _dbContext;
        public CourseRepository(CourseTaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Course GetCourseByID(int id)
        {
            Course course = _dbContext.Courses.Where<Course>(x => x.ID == id).FirstOrDefault();
            if (course == null)
            {
                Console.WriteLine($"Course ID {id} does not exist.");
            };
            return course;
        }
        public void AddNewCourse(string name, bool mandatory)
        {
            Course course = new Course(name, mandatory);
            var courseList = _dbContext.Courses.Where<Course>(x => x.Name == name).FirstOrDefault();
            if (courseList != null)
            {
                Console.WriteLine($"Course {course.Name} already exists.");
            }
            else
            {
                _dbContext.Courses.Add(course);
                _dbContext.SaveChanges();
            };
        }
        public void LinkCourseToDepartament(int id, string departmanetName)
        {
            Course course = _dbContext.Courses.Where<Course>(x => x.ID == id).FirstOrDefault();
            Departament departament = _dbContext.Departaments.Where<Departament>(x => x.Name == departmanetName).FirstOrDefault();
            if (course != null)
            {
                if (departament != null)
                {
                    if (course.Departaments == null)
                    {
                        course.Departaments.Add(departament);
                        if (departament.Courses == null)
                        {
                            departament.Courses.Add(course);
                            _dbContext.SaveChanges();
                        }
                        else
                        {
                            if (!departament.Courses.Contains(course))
                            {
                                departament.Courses.Add(course);
                                _dbContext.SaveChanges();
                            }
                            else
                            {
                                Console.WriteLine($"{departament.Name} already contains {course.Name} course");
                            };
                        };
                    }
                    else
                    {
                        if (!course.Departaments.Contains(departament))
                        {
                            departament.Courses.Add(course);
                            _dbContext.SaveChanges();
                        }
                        else
                        {
                            Console.WriteLine($"Course {course.Name} already linked with {departament.Name}.");
                        };
                    };
                }
                else
                {
                    Console.WriteLine($"Departament not found");
                };
            }
            else
            {
                Console.WriteLine($"ID not found");
            };
        }

        public void RemoveCourse(string name)
        {
            Course course = _dbContext.Courses.Include("Departaments").ToList().Where<Course>(x => x.Name == name).FirstOrDefault();

            if (course != null)
            {
                foreach (var dep in course.Departaments)
                {
                    Departament departament = dep;
                    departament.Courses.Remove(course);
                    foreach (var stud in departament.Students)
                    {
                        Student student = stud;
                        if (student.Courses.Contains(course))
                        {
                            student.Courses.Remove(course);
                        }
                        else continue;
                    }
                }
                _dbContext.Courses.Where<Course>(x => x.ID == course.ID).ExecuteDelete();
                _dbContext.SaveChanges();
            }
            else
            {
                Console.WriteLine($"Course {name} was not found!");
            };
        }
    }
}

using CourseTask3.Repository.Interfaces;
using CourseTask3.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseTask3.Repository.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CourseTaskDbContext _dbContext;
        public StudentRepository(CourseTaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Student GetStudentByID(int id)
        {
            Student student = _dbContext.Students.Include("Departament").Include("Courses").ToList().Where<Student>(x => x.ID == id).FirstOrDefault();
            if (student == null)
            {
                Console.WriteLine($"Student ID {id} does not exist.");
            };
            return student;
        }
        public void AddNewStudent(string firstName, string lastName, string personalCode, string level, int departamentID)
        {
            Departament departament = _dbContext.Departaments.Where<Departament>(x => x.ID == departamentID).FirstOrDefault();
            Student student = new Student(firstName, lastName, personalCode, level, departamentID);
            var studentList = _dbContext.Students.Where<Student>(x => x.PersonalCode == personalCode).FirstOrDefault();
            if (studentList != null)
            {
                Console.WriteLine($"Student with {student.PersonalCode} already exists.");
            }
            else
            {
                _dbContext.Students.Add(student);
                _dbContext.SaveChanges();
            };
            MoveStudentToDepartament(personalCode, departament.Name);
        }
        public void MoveStudentToDepartament(string personalCode, string departmanetName)
        {
            Student student = _dbContext.Students.Include("Departament").Include("Courses").ToList().Where<Student>(x => x.PersonalCode == personalCode).FirstOrDefault();
            Departament departament = _dbContext.Departaments.Include("Students").Include("Courses").ToList().Where<Departament>(x => x.Name == departmanetName).FirstOrDefault();
            if (student != null)
            {
                if (departament != null)
                {
                    student.Departament = departament;
                    student.Courses.Clear();
                    foreach (var course in departament.Courses)
                    {
                        if (course.Name.Contains(student.Level))
                        {
                            if (course.Mandatory) student.Courses.Add(course);
                            else
                            {
                                Console.Clear();
                                Console.WriteLine($"Will student {student.FirstName} {student.LastName} attend optional course: {course.Name}? Enter y/n and press Enter.");
                                var decision = Console.ReadLine()[0];
                                if (decision == 'y')
                                {
                                    var thiscourse = _dbContext.Courses.Where<Course>(x => x.ID == course.ID).FirstOrDefault();
                                    student.Courses.Add(thiscourse);
                                }

                                if (decision == 'n') continue;
                            }
                        }
                        else continue;
                    };
                    _dbContext.SaveChanges();
                    Console.WriteLine($"Student {student.FirstName} {student.LastName} transferred to {departament.Name}.");
                }
                else Console.WriteLine($"Departament not found");
            }
            else Console.WriteLine($"ID not found");
        }
        public void AddCourse(string personalCode, int courseid)
        {
            Student student = _dbContext.Students.Include("Departament").Include("Courses").ToList().Where<Student>(x => x.PersonalCode == personalCode).FirstOrDefault();
            Departament departament = student.Departament;
            Course course = _dbContext.Courses.Where<Course>(x => x.ID == courseid).FirstOrDefault();
            if (student != null)
            {
                if (departament != null)
                {
                    if (course != null)
                    {
                        if (departament.Courses.Contains(course))
                        {
                            if (student.Courses.Contains(course))
                            {
                                Console.WriteLine("Student is already enrolled in this course!");
                            }
                            else
                            {
                                if (course.Name.Contains(student.Level))
                                {
                                    student.Courses.Add(course);
                                    _dbContext.SaveChanges();
                                }
                                else
                                {
                                    Console.WriteLine("Course level does not match student level!");
                                };
                            };
                        }
                        else Console.WriteLine("Course not available in this departament");
                    }
                    else Console.WriteLine("Course not found!");
                }
                else Console.WriteLine($"Departament not found");
            }
            else Console.WriteLine($"ID not found");
        }
        public void RemoveStudentByPersonalCode(string personalCode)
        {
            Student student = _dbContext.Students.Include("Departament").Include("Courses").ToList().Where<Student>(x => x.PersonalCode == personalCode).FirstOrDefault();
            Departament departament = student.Departament;
            if (student != null)
            {
                if (departament != null)
                {
                    departament.Students.Remove(student);
                    _dbContext.Students.Where<Student>(x => x.PersonalCode == personalCode).ExecuteDelete();
                    _dbContext.SaveChanges();
                    Console.WriteLine($"Student {student.FirstName} {student.LastName} removed from the lists.");
                }
                else Console.WriteLine($"Departament not found");
            }
            else Console.WriteLine($"ID not found");
        }
    }
}

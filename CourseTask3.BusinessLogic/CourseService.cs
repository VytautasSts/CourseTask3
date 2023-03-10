using CourseTask3.BusinessLogic.Interfaces;
using CourseTask3.Repository.Interfaces;
using CourseTask3.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseTask3.BusinessLogic
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public Course GetCourseByID(int id)
        {
            Course course = _courseRepository.GetCourseByID(id);
            return course;
        }
        public void AddNewCourse(string name, bool mandatory)
        {
            _courseRepository.AddNewCourse(name, mandatory);
        }
        public void LinkCourseToDepartament(int id, string departmanetName)
        {
            _courseRepository.LinkCourseToDepartament(id, departmanetName);
        }
        public void RemoveCourse(string name)
        {
            _courseRepository.RemoveCourse(name);
        }
    }
}

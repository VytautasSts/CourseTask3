using CourseTask3.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseTask3.Repository.Interfaces
{
    public interface ICourseRepository
    {
        Course GetCourseByID(int id);
        void AddNewCourse(string name, bool mandatory);
        void LinkCourseToDepartament(int id, string departmanetName);
        void RemoveCourse(string name);
    }
}

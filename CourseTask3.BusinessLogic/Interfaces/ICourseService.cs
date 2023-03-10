using CourseTask3.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseTask3.BusinessLogic.Interfaces
{
    public interface ICourseService
    {
        Course GetCourseByID(int id);
        void AddNewCourse(string name, bool mandatory);
        void LinkCourseToDepartament(int id, string departmanetName);
        void RemoveCourse(string name);
    }
}

using CourseTask3.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseTask3.BusinessLogic.Interfaces
{
    public interface IDepartamentService
    {
        Departament GetDepartamentByID(int id);
        void AddNewDepartament(string name);
        List<Student> GetAllStudentsInDepartament(string departamentName);
        void RemoveDepartament(string departamentName);
    }
}

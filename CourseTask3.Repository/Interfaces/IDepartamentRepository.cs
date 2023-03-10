using CourseTask3.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseTask3.Repository.Interfaces
{
    public interface IDepartamentRepository
    {
        Departament GetDepartamentByID(int id);
        void AddNewDepartament(string name);
        List<Student> GetAllStudentsInDepartament(string departamentName);
        void RemoveDepartament(string name);
    }
}

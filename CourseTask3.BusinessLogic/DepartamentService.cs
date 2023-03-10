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
    public class DepartamentService : IDepartamentService
    {
        private readonly IDepartamentRepository _departamentRepository;

        public DepartamentService(IDepartamentRepository departamentRepository)
        {
            _departamentRepository = departamentRepository;
        }
        public Departament GetDepartamentByID(int id)
        {
            Departament departament = _departamentRepository.GetDepartamentByID(id);
            return departament;
        }
        public void AddNewDepartament(string name)
        {
            _departamentRepository.AddNewDepartament(name);
        }
        public List<Student> GetAllStudentsInDepartament(string departamentName)
        {
            List<Student> students = _departamentRepository.GetAllStudentsInDepartament(departamentName);
            return students;
        }
        public void RemoveDepartament(string departamentName)
        {
            _departamentRepository.RemoveDepartament(departamentName);
        }
    }
}

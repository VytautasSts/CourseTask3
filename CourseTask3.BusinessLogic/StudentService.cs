using CourseTask3.BusinessLogic.Interfaces;
using CourseTask3.Repository.Interfaces;
using CourseTask3.Repository.Models;

namespace CourseTask3.BusinessLogic
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public Student GetStudentByID(int id)
        {
            Student student = _studentRepository.GetStudentByID(id);
            return student;
        }
        public void AddNewStudent(string firstName, string lastName, string personalCode, string level, int departamentID)
        {
            _studentRepository.AddNewStudent(firstName, lastName, personalCode, level, departamentID);
        }
        public void MoveStudentToDepartament(string personalCode, string departmanetName)
        {
            _studentRepository.MoveStudentToDepartament(personalCode, departmanetName);
        }
        public void AddCourse(string personalCode, int courseid)
        {
            _studentRepository.AddCourse(personalCode, courseid);
        }
        public void RemoveStudentByPersonalCode(string personalCode)
        {
            _studentRepository.RemoveStudentByPersonalCode(personalCode);
        }
    }
}

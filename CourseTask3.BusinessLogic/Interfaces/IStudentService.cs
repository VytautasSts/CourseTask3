using CourseTask3.Repository.Models;

namespace CourseTask3.BusinessLogic.Interfaces
{
    public interface IStudentService
    {
        Student GetStudentByID(int id);
        void AddNewStudent(string firstName, string lastName, string personalCode, string level, int departamentID);
        void MoveStudentToDepartament(string personalCode, string departmanetName);
        void AddCourse(string personalCode, int courseid);
        void RemoveStudentByPersonalCode(string personalCode);
    }
}

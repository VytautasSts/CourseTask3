using CourseTask3.Repository.Models;

namespace CourseTask3.Repository.Interfaces
{
    public interface IStudentRepository
    {
        Student GetStudentByID(int id);
        void AddNewStudent(string firstName, string lastName, string personalCode, string level, int departamentID);
        void MoveStudentToDepartament(string peronalid, string departmanetName);
        void AddCourse(string personalCode, int courseid);
        void RemoveStudentByPersonalCode(string personalCode);
    }
}

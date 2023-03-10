namespace CourseTask3.Repository.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalCode { get; set; }
        public string Level { get; set; }
        public int DepartamentID { get; set; }

        public Departament? Departament { get; set; }
        public ICollection<Course> Courses { get; set; }

        public Student(string firstName, string lastName, string personalCode, string level, int departamentID)
        {
            FirstName = firstName;
            LastName = lastName;
            PersonalCode = personalCode;
            Level = level;
            DepartamentID = departamentID;
            Courses = new HashSet<Course>();
        }
    }
}

namespace CourseTask3.Repository.Models
{
    public class Departament
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<Course> Courses { get; set; }
        public ICollection<Student> Students { get; set; }

        public Departament(string name)
        {
            Name = name;
            Courses = new HashSet<Course>();
            Students = new HashSet<Student>();
        }
    }
}

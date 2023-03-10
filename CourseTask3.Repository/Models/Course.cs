namespace CourseTask3.Repository.Models
{
    public class Course
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Mandatory { get; set; }
        public ICollection<Departament> Departaments { get; set; }
        public ICollection<Student> Students { get; set; }

        public Course(string name, bool mandatory)
        {
            Name = name;
            Mandatory = mandatory;
            Departaments = new HashSet<Departament>();
            Students = new HashSet<Student>();
        }

    }
}

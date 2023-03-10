using CourseTask3.BusinessLogic;
using CourseTask3.BusinessLogic.Interfaces;
using CourseTask3.Repository;
using CourseTask3.Repository.Interfaces;
using CourseTask3.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CourseTask3.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            var courseService = host.Services.GetRequiredService<ICourseService>();
            var studentService = host.Services.GetRequiredService<IStudentService>();
            var departamentService = host.Services.GetRequiredService<IDepartamentService>();
            var courseRepository = host.Services.GetRequiredService<ICourseRepository>();
            var studentRepository = host.Services.GetRequiredService<IStudentRepository>();
            var departamentRepository = host.Services.GetRequiredService<IDepartamentRepository>();

            departamentService.AddNewDepartament("Test departament"); //Pridėti departamentą tiesiog nurodanta pavadinimą
            courseService.AddNewCourse("4.TestSubjectA", true); //Pridėti paskaitą. Būtinai reikia nurodyti lygį formatu "1.","2.","3.". Pagal tai paskaitos priskiriamos studentams.
            courseService.LinkCourseToDepartament(1, "Mathematics departament"); // Nurodyti paskaitos ID ir departamentą. 
            studentService.AddNewStudent("name1", "lastname1", "000000001", "1", 1); //sukurti studentą nurodant vardą, pavardę, AK, lygi ir departamento ID.
            //sukūrimo proceso metu studentui priskiriamos privaomos paskaitos pagal departamentą ir klausiama kuriuos pasirenkamuosius dalykus nori įtraukti į kursų sąrašą

            var departament = departamentService.GetDepartamentByID(1);
            Console.Clear();
            Console.WriteLine(departament.ID);
            Console.WriteLine(departament.Name);
            foreach (var course in departament.Courses)
            {
                if (course.Mandatory) Console.WriteLine(course.Name);
                if (!course.Mandatory) Console.WriteLine(course.Name);
            };
            foreach (var student in departament.Students)
            {
                Console.WriteLine($"Student: {student.FirstName} {student.LastName}, Level: {student.Level}");
            };
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            var myStudent = studentService.GetStudentByID(3);
            Console.Clear();
            Console.WriteLine(string.Format("{0,20}:{1,20}", "Student ID:", myStudent.ID));
            Console.WriteLine(string.Format("{0,20}:{1,20}", "Student Name:", myStudent.FirstName));
            Console.WriteLine(string.Format("{0,20}:{1,20}", "Student Surname:", myStudent.LastName));
            Console.WriteLine(string.Format("{0,20}:{1,20}", "Student code:", myStudent.PersonalCode));
            Console.WriteLine(string.Format("{0,20}:{1,20}", "Student level:", myStudent.Level));
            Console.WriteLine(string.Format("{0,20}:{1,20}", "Student dep. ID:", myStudent.Departament.ID));
            Console.WriteLine(string.Format("{0,20}:{1,20}", "Student dep. Name:", myStudent.Departament.Name));
            Console.WriteLine($"******C O U R S E S*********************");
            foreach (var entry in myStudent.Courses)
            {
                Console.WriteLine($"Is mandatory: {entry.Mandatory}; Name: {entry.Name}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            //host.Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<CourseTaskDbContext>(options => { options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection")); }, ServiceLifetime.Scoped);
                    services.AddScoped<IStudentService, StudentService>();
                    services.AddScoped<IStudentRepository, StudentRepository>();
                    services.AddScoped<ICourseService, CourseService>();
                    services.AddScoped<ICourseRepository, CourseRepository>();
                    services.AddScoped<IDepartamentService, DepartamentService>();
                    services.AddScoped<IDepartamentRepository, DepartamentRepository>();
                });
        }
    }
}
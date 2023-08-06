using UniversityShared.Models;

namespace UniversityRestApi.Data;
public class DataSeeder
{
    public static void SeedData(UniversityContext context)
    {
        if (context.Students.Any())
        {
            return;   // DB has been seeded
        }

        var alexander = new Student
        {
            ID = Guid.NewGuid(),
            FirstName = "Carson",
            LastName = "Alexander",
            EnrollmentDate = DateTime.Parse("2016-09-01")
        };

        var alonso = new Student
        {
            ID = Guid.NewGuid(),
            FirstName = "Meredith",
            LastName = "Alonso",
            EnrollmentDate = DateTime.Parse("2018-09-01")
        };

        var anand = new Student
        {
            ID = Guid.NewGuid(),
            FirstName = "Arturo",
            LastName = "Anand",
            EnrollmentDate = DateTime.Parse("2019-09-01")
        };

        var barzdukas = new Student
        {
            ID = Guid.NewGuid(),
            FirstName = "Gytis",
            LastName = "Barzdukas",
            EnrollmentDate = DateTime.Parse("2018-09-01")
        };

        var li = new Student
        {
            ID = Guid.NewGuid(),
            FirstName = "Yan",
            LastName = "Li",
            EnrollmentDate = DateTime.Parse("2018-09-01")
        };

        var justice = new Student
        {
            ID = Guid.NewGuid(),
            FirstName = "Peggy",
            LastName = "Justice",
            EnrollmentDate = DateTime.Parse("2017-09-01")
        };

        var norman = new Student
        {
            ID = Guid.NewGuid(),
            FirstName = "Laura",
            LastName = "Norman",
            EnrollmentDate = DateTime.Parse("2019-09-01")
        };

        var olivetto = new Student
        {
            ID = Guid.NewGuid(),
            FirstName = "Nino",
            LastName = "Olivetto",
            EnrollmentDate = DateTime.Parse("2011-09-01")
        };

        var students = new Student[]
        {
                alexander,
                alonso,
                anand,
                barzdukas,
                li,
                justice,
                norman,
                olivetto
        };

        context.AddRange(students);

        var abercrombie = new Instructor
        {
            ID = Guid.NewGuid(),
            FirstName = "Kim",
            LastName = "Abercrombie",
            HireDate = DateTime.Parse("1995-03-11")
        };

        var fakhouri = new Instructor
        {
            ID = Guid.NewGuid(),
            FirstName = "Fadi",
            LastName = "Fakhouri",
            HireDate = DateTime.Parse("2002-07-06")
        };

        var harui = new Instructor
        {
            ID = Guid.NewGuid(),
            FirstName = "Roger",
            LastName = "Harui",
            HireDate = DateTime.Parse("1998-07-01")
        };

        var kapoor = new Instructor
        {
            ID = Guid.NewGuid(),
            FirstName = "Candace",
            LastName = "Kapoor",
            HireDate = DateTime.Parse("2001-01-15")
        };

        var zheng = new Instructor
        {
            ID = Guid.NewGuid(),
            FirstName = "Roger",
            LastName = "Zheng",
            HireDate = DateTime.Parse("2004-02-12")
        };

        var instructors = new Instructor[]
        {
                abercrombie,
                fakhouri,
                harui,
                kapoor,
                zheng
        };

        context.AddRange(instructors);

        var officeAssignments = new OfficeAssignment[]
        {
                new OfficeAssignment {
                    Instructor = fakhouri,
                    Location = "Smith 17" },
                new OfficeAssignment {
                    Instructor = harui,
                    Location = "Gowan 27" },
                new OfficeAssignment {
                    Instructor = kapoor,
                    Location = "Thompson 304" }
        };

        context.AddRange(officeAssignments);

        var english = new Department
        {
            DepartmentID = Guid.NewGuid(),
            Name = "English",
            Budget = 350000,
            StartDate = DateTime.Parse("2007-09-01"),
            Administrator = abercrombie
        };

        var mathematics = new Department
        {
            DepartmentID = Guid.NewGuid(),
            Name = "Mathematics",
            Budget = 100000,
            StartDate = DateTime.Parse("2007-09-01"),
            Administrator = fakhouri
        };

        var engineering = new Department
        {
            DepartmentID = Guid.NewGuid(),
            Name = "Engineering",
            Budget = 350000,
            StartDate = DateTime.Parse("2007-09-01"),
            Administrator = harui
        };

        var economics = new Department
        {
            DepartmentID = Guid.NewGuid(),
            Name = "Economics",
            Budget = 100000,
            StartDate = DateTime.Parse("2007-09-01"),
            Administrator = kapoor
        };

        var departments = new Department[]
        {
                english,
                mathematics,
                engineering,
                economics
        };

        context.AddRange(departments);

        var chemistry = new Course
        {
            ID = Guid.NewGuid(),
            Title = "Chemistry",
            Credits = 3,
            Department = engineering,
            Instructors = new List<Instructor> { kapoor, harui }
        };

        var microeconomics = new Course
        {
            ID = Guid.NewGuid(),
            Title = "Microeconomics",
            Credits = 3,
            Department = economics,
            Instructors = new List<Instructor> { zheng }
        };

        var macroeconmics = new Course
        {
            ID = Guid.NewGuid(),
            Title = "Macroeconomics",
            Credits = 3,
            Department = economics,
            Instructors = new List<Instructor> { zheng }
        };

        var calculus = new Course
        {
            ID = Guid.NewGuid(),
            Title = "Calculus",
            Credits = 4,
            Department = mathematics,
            Instructors = new List<Instructor> { fakhouri }
        };

        var trigonometry = new Course
        {
            ID = Guid.NewGuid(),
            Title = "Trigonometry",
            Credits = 4,
            Department = mathematics,
            Instructors = new List<Instructor> { harui }
        };

        var composition = new Course
        {
            ID = Guid.NewGuid(),
            Title = "Composition",
            Credits = 3,
            Department = english,
            Instructors = new List<Instructor> { abercrombie }
        };

        var literature = new Course
        {
            ID = Guid.NewGuid(),
            Title = "Literature",
            Credits = 4,
            Department = english,
            Instructors = new List<Instructor> { abercrombie }
        };

        var courses = new Course[]
        {
                chemistry,
                microeconomics,
                macroeconmics,
                calculus,
                trigonometry,
                composition,
                literature
        };

        context.AddRange(courses);

        var enrollments = new Enrollment[]
        {
                new Enrollment {
                    ID = Guid.NewGuid(),
                    Student = alexander,
                    Course = chemistry,
                    Grade = Grade.A
                },
                new Enrollment {
                    ID = Guid.NewGuid(),
                    Student = alexander,
                    Course = microeconomics,
                    Grade = Grade.C
                },
                new Enrollment {
                    ID = Guid.NewGuid(),
                    Student = alexander,
                    Course = macroeconmics,
                    Grade = Grade.B
                },
                new Enrollment {
                    ID = Guid.NewGuid(),
                    Student = alonso,
                    Course = calculus,
                    Grade = Grade.B
                },
                new Enrollment {
                    ID = Guid.NewGuid(),
                    Student = alonso,
                    Course = trigonometry,
                    Grade = Grade.B
                },
                new Enrollment {
                    ID = Guid.NewGuid(),
                    Student = alonso,
                    Course = composition,
                    Grade = Grade.B
                },
                new Enrollment {
                    ID = Guid.NewGuid(),
                    Student = anand,
                    Course = chemistry
                },
                new Enrollment {
                    ID = Guid.NewGuid(),
                    Student = anand,
                    Course = microeconomics,
                    Grade = Grade.B
                },
                new Enrollment {
                    ID = Guid.NewGuid(),
                    Student = barzdukas,
                    Course = chemistry,
                    Grade = Grade.B
                },
                new Enrollment {
                    ID = Guid.NewGuid(),
                    Student = li,
                    Course = composition,
                    Grade = Grade.B
                },
                new Enrollment {
                    ID = Guid.NewGuid(),
                    Student = justice,
                    Course = literature,
                    Grade = Grade.B
                }
        };

        context.AddRange(enrollments);
        context.SaveChanges();
    }
}

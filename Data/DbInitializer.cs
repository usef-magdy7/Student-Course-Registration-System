using StudentCourseSystem.Models.Entities;

namespace StudentCourseSystem.Data
{
    public static class DbInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
      
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // 1. Seed Courses
            if (!context.Courses.Any())
            {
                Course[] courses =
                [
                    new Course { CourseCode = "CS101", Title = "Introduction to Computer Science", CreditHours = 3, Department = "Computer Science", MaxStudents = 40, IsActive = true, Description = "Fundamentals of programming and algorithms." },
                    new Course { CourseCode = "CS202", Title = "Data Structures & Algorithms", CreditHours = 4, Department = "Computer Science", MaxStudents = 35, IsActive = true, Description = "Arrays, Linked Lists, Trees, Graphs, and Sorting algorithms." },
                    new Course { CourseCode = "CS305", Title = "Database Systems", CreditHours = 3, Department = "Computer Science", MaxStudents = 30, IsActive = true, Description = "Relational databases, SQL, Normalization, and ERD design." },
                    new Course { CourseCode = "IS201", Title = "System Analysis and Design", CreditHours = 3, Department = "Information Systems", MaxStudents = 45, IsActive = true, Description = "Software lifecycle, UML diagrams, and requirements analysis." },
                    new Course { CourseCode = "SWE401", Title = "Software Architecture", CreditHours = 3, Department = "Software Engineering", MaxStudents = 25, IsActive = true, Description = "Design patterns, Microservices, and Clean Architecture." },
                    new Course { CourseCode = "AI301", Title = "Artificial Intelligence Basics", CreditHours = 3, Department = "Artificial Intelligence", MaxStudents = 30, IsActive = true, Description = "Search algorithms, Knowledge representation, and Intro to ML." },
                    new Course { CourseCode = "NET102", Title = "Computer Networks", CreditHours = 3, Department = "Information Technology", MaxStudents = 40, IsActive = true, Description = "OSI model, TCP/IP, Routing protocols, and IP addressing." },
                    new Course { CourseCode = "MATH101", Title = "Calculus I", CreditHours = 4, Department = "Mathematics", MaxStudents = 50, IsActive = true, Description = "Limits, Differentiation, and Basic Integration." },
                    new Course { CourseCode = "MATH202", Title = "Linear Algebra", CreditHours = 3, Department = "Mathematics", MaxStudents = 50, IsActive = true, Description = "Matrices, Vector spaces, and Eigenvalues." },
                    new Course { CourseCode = "WEB201", Title = "Web Development with ASP.NET Core", CreditHours = 3, Department = "Software Engineering", MaxStudents = 30, IsActive = true, Description = "Building MVC web applications and REST APIs." }
                ];

                context.Courses.AddRange(courses);
                context.SaveChanges();
            }

          
            if (!context.Students.Any())
            {
                Student[] students =
                [
                    new Student { FullName = "Ahmed Ali", Email = "student1@univ.edu", Department = "Computer Science", Level = 1 },
                    new Student { FullName = "Mina Magdy", Email = "student2@univ.edu", Department = "Computer Science", Level = 2 },
                    new Student { FullName = "Sara Hassan", Email = "student3@univ.edu", Department = "Computer Science", Level = 3 },
                    new Student { FullName = "Omar Khaled", Email = "student4@univ.edu", Department = "Information Systems", Level = 2 },
                    new Student { FullName = "Mariam Adel", Email = "student5@univ.edu", Department = "Software Engineering", Level = 4 },
                    new Student { FullName = "Mohamed Ibrahim", Email = "student6@univ.edu", Department = "Information Technology", Level = 1 },
                    new Student { FullName = "John Nabil", Email = "student7@univ.edu", Department = "Artificial Intelligence", Level = 3 },
                    new Student { FullName = "Nour El-Din", Email = "student8@univ.edu", Department = "Computer Science", Level = 2 },
                    new Student { FullName = "Youssef Mahmoud", Email = "student9@univ.edu", Department = "Mathematics", Level = 1 },
                    new Student { FullName = "Kareem Tarek", Email = "student10@univ.edu", Department = "Software Engineering", Level = 4 },
                    new Student { FullName = "Salma Fathy", Email = "student11@univ.edu", Department = "Information Systems", Level = 3 },
                    new Student { FullName = "Hady Mostafa", Email = "student12@univ.edu", Department = "Computer Science", Level = 2 }
                ];

                context.Students.AddRange(students);
                context.SaveChanges();
            }

            // 3. Seed 15 Pending Requests
            if (!context.CourseRequests.Any())
            {
                var courseIds = context.Courses.Select(c => c.Id).ToList();
                var studentIds = context.Students.Select(s => s.Id).ToList();

                if (studentIds.Count >= 12 && courseIds.Count >= 10)
                {
                    CourseRequest[] requests =
                    [
                        new CourseRequest { StudentId = studentIds[0], CourseId = courseIds[0], Status = RequestStatus.Pending, RequestedAt = DateTime.Now.AddHours(-1) },
                        new CourseRequest { StudentId = studentIds[1], CourseId = courseIds[0], Status = RequestStatus.Pending, RequestedAt = DateTime.Now.AddHours(-3) },
                        new CourseRequest { StudentId = studentIds[2], CourseId = courseIds[1], Status = RequestStatus.Pending, RequestedAt = DateTime.Now.AddHours(-5) },
                        new CourseRequest { StudentId = studentIds[3], CourseId = courseIds[1], Status = RequestStatus.Pending, RequestedAt = DateTime.Now.AddHours(-8) },
                        new CourseRequest { StudentId = studentIds[4], CourseId = courseIds[2], Status = RequestStatus.Pending, RequestedAt = DateTime.Now.AddDays(-1) },
                        new CourseRequest { StudentId = studentIds[5], CourseId = courseIds[3], Status = RequestStatus.Pending, RequestedAt = DateTime.Now.AddDays(-1).AddHours(-4) },
                        new CourseRequest { StudentId = studentIds[6], CourseId = courseIds[4], Status = RequestStatus.Pending, RequestedAt = DateTime.Now.AddDays(-2) },
                        new CourseRequest { StudentId = studentIds[7], CourseId = courseIds[5], Status = RequestStatus.Pending, RequestedAt = DateTime.Now.AddHours(-2) },
                        new CourseRequest { StudentId = studentIds[8], CourseId = courseIds[6], Status = RequestStatus.Pending, RequestedAt = DateTime.Now.AddHours(-6) },
                        new CourseRequest { StudentId = studentIds[9], CourseId = courseIds[7], Status = RequestStatus.Pending, RequestedAt = DateTime.Now.AddDays(-3) },
                        new CourseRequest { StudentId = studentIds[10], CourseId = courseIds[8], Status = RequestStatus.Pending, RequestedAt = DateTime.Now.AddHours(-4) },
                        new CourseRequest { StudentId = studentIds[11], CourseId = courseIds[9], Status = RequestStatus.Pending, RequestedAt = DateTime.Now.AddHours(-10) },
                        new CourseRequest { StudentId = studentIds[0], CourseId = courseIds[9], Status = RequestStatus.Pending, RequestedAt = DateTime.Now.AddDays(-1) },
                        new CourseRequest { StudentId = studentIds[1], CourseId = courseIds[4], Status = RequestStatus.Pending, RequestedAt = DateTime.Now.AddHours(-7) },
                        new CourseRequest { StudentId = studentIds[2], CourseId = courseIds[5], Status = RequestStatus.Pending, RequestedAt = DateTime.Now.AddHours(-12) }
                    ];

                    context.CourseRequests.AddRange(requests);
                    context.SaveChanges();
                }
            }
        }
    }
}

using Bogus;
using Bogus.DataSets;
using LMS.api.Model;
using System.Globalization;

namespace LMS.api.Seed
{
    public class SeedData
    {

        public static Faker faker;
        public static async Task InitAsync(LMSContext context)
        {

           // if (await context.Students.AnyAsync()) return;

            faker = new Faker("sv");

            var students = GenerateStudents(50);
            await context.AddRangeAsync(students);

            //var courses = GenerateCourses(20);
          //  await context.AddRangeAsync(students);

            await context.SaveChangesAsync();

        }

        private static IEnumerable<Course> GenerateCourses(int numberOfCourses)
        {
            var courses = new List<Course>();

            for (int i = 0; i < numberOfCourses; i++)
            {
                var title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(faker.Company.Bs());
                var course = new Course { Title = title };
                courses.Add(course);
            }
            return courses;
        }

        private static IEnumerable<User> GenerateStudents(int numberOfStudents)
        {
            var students = new List<User>();

            for (int i = 0; i < numberOfStudents; i++)
            {
                var avatar = faker.Internet.Avatar();
                var fName = faker.Name.FirstName();
                var lName = faker.Name.LastName();
                var email = faker.Internet.Email(fName, lName, "lexicon.se");

                var student = new User { FirstName = fName, LastName = lName, Email = email, Password = "123" };
           
                students.Add(student);
            }

            return students;
        }

    }
}

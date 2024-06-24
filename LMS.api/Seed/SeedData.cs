
using Bogus;
using Bogus.DataSets;
using LMS.api.Model;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace LMS.api.Seed
{
    public class SeedData
    {

        public static Faker faker;
        private static IEnumerable<Course> courses;
        private static Random random = new Random();
        public static async Task InitAsync(LMSContext context)
        {

           if (await context.User.AnyAsync()) return;

            faker = new Faker("sv");

            courses = GenerateCourses(60);

            var students = GenerateStudents(1000);            
            await context.AddRangeAsync(students);

            await context.SaveChangesAsync();

        }

        private static IEnumerable<Course> GenerateCourses(int numberOfCourses)
        {
            
            var courses = new List<Course>();

            for (int i = 0; i < numberOfCourses; i++)
            {
              
                var title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(faker.Company.Bs());
                var course = new Course {
                    Title = title,
                    Description = title,
                    Modules = GenerateModules(random.Next(1, 4)).ToList()
            };
                courses.Add(course);
            }
            return courses;
        }
        private static Course SelectACourse()
        {
            int next = random.Next(0, courses.Count());
            return courses.ElementAt(next);
        }

        private static IEnumerable<Module> GenerateModules(int numberOfModules)
        {
            var modules = new List<Module>();

            for (int i = 0; i < numberOfModules; i++)
            {
                var module = new Module
                {
                    Title = faker.Lorem.Slug(3),
                    Description = faker.Lorem.Sentences(),
                    Start = DateTime.Now,
                    End = DateTime.Now.AddDays(random.Next(0, 100)),
                    Activities = GenerateActivities(random.Next(1,5)).ToList()
                };
                modules.Add(module);
            }

            return modules;
        }

        private static IEnumerable<Activity> GenerateActivities(int numberOfActivities)
        {
            var activities = new List<Activity>();

            for (int i = 0; i < numberOfActivities; i++)
            {
                var activity = new Activity
                {
                    Title = faker.Lorem.Slug(1),
                    Description = faker.Lorem.Sentence(),
                    Start = DateTime.Now,
                    End = DateTime.Now.AddDays(random.Next(0, 100))
                };
                activities.Add(activity);
            }

            return activities;
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
                var course = SelectACourse();

                var student = new User {
                    FirstName = fName,
                    LastName = lName,
                    Email = email, 
                    Password = "123",
                    Course = course };
           
                students.Add(student);
            }

            return students;
        }

    }
}

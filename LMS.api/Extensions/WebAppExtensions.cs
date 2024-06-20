using LMS.api.Model;
using LMS.api.Seed;

namespace LMS.api.Extensions
{
    public static class WebAppExtensions
    {
        public static async Task SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var context = serviceProvider.GetRequiredService<LMSContext>();


                try
                {
                    await SeedData.InitAsync(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }

            }
        }
 
    }
}

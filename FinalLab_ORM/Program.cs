namespace FinalLab_ORM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // The options part was added to disable JSON Case conversion
            // By configuring the JSON serializer options to use null for the PropertyNamingPolicy, 
            // you effectively disable the camelCase conversion, allowing the serializer to preserve 
            // the original casing of the properties.
            builder.Services.AddControllersWithViews()/*.AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null)*/;

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
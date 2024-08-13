namespace MVC_Assignment1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Middleware that terminates the request if the path contains "/end".
            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Contains("/end"))
                {
                    await context.Response.WriteAsync("request terminated");
                }
                else
                {
                    await next();
                }
            });

            // Middleware that writes "Hello1" if the path contains "hello".
            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Contains("hello"))
                {
                    await context.Response.WriteAsync("Hello1");
                    await next();
                }
                else
                {
                    await next();
                }
            });


            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Contains("hello"))
                {
                    await context.Response.WriteAsync("Hello2");
                    await next();
                }
                else
                {
                    await next();
                }
            });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

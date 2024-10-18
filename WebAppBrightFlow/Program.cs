using Microsoft.EntityFrameworkCore;
using WebAppBrightFlow.Data;
using WebAppBrightFlow.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllersWithViews();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
    SeedDatabase(dbContext);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
void SeedDatabase(ApplicationDbContext context)
{

    if (!context.People.Any())
    {
        context.People.AddRange(
            new Person { Name = "Jessica", Age = 22, Gender = "Vrouw", Description = "Jessica werkt als secretaresse en is een heel vriendelijk en zeer hard werkende werknemer.", YearOfEmployment = 2021 },
            new Person { Name = "Bob", Age = 53, Gender = "Man", Description = "Bob zorgt voor een goede en gezellige sfeer als product manager, daarnaast kan hij de lekkerste appeltaarten bakken.", YearOfEmployment = 2006 },
            new Person { Name = "Charlie", Age = 27, Gender = "Man", Description = "Charlie is de van de kantine en houdt enorm van broodjes.", YearOfEmployment = 2018 }
        );
        context.SaveChanges();
    }
}
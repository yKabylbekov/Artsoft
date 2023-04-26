using Artsofte.Data;
using Artsofte.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ArtsoftDbContext>( options =>
    options.UseSqlServer( builder.Configuration.GetConnectionString( "DeffaultConnection" ) ) );

var app = builder.Build();

using( var scope = app.Services.CreateScope() ) {
    var services = scope.ServiceProvider;

    try {
        var context = services.GetRequiredService<ArtsoftDbContext>();
        context.Database.EnsureCreated();
        if( !context.Departments.Any() ) {
            context.Departments.AddRange(
                new Department { Name = "IT", Floor = 4 },
                new Department { Name = "HR", Floor = 2 },
                new Department { Name = "Finance", Floor = 3 } );
        }

        if( !context.ProgrammingLanguages.Any() ) {
            context.ProgrammingLanguages.AddRange(
                new ProgrammingLanguage { Name = "C#" },
                new ProgrammingLanguage { Name = "Java" },
                new ProgrammingLanguage { Name = "Python" }
            );
        }
        context.SaveChanges();
    }
    catch( Exception ex ) {
        Console.WriteLine( ex.Message );
    }
}

// Configure the HTTP request pipeline.
if( !app.Environment.IsDevelopment() ) {
    app.UseExceptionHandler( "/Home/Error" );
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=Index}/{id?}" );

app.Run();

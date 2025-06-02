using Microsoft.EntityFrameworkCore;
using pja_apbd_cwic12.Models;

namespace pja_apbd_cwic12;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<Tutorial12Context>(options =>
        {
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("Default")
            );
        });

        builder.Services.AddControllers();
        
        builder.Services.AddOpenApi();

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.MapControllers();

        app.Run();
    }
}
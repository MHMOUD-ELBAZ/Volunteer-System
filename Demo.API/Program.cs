
using Demo.API.Services;
using Microsoft.EntityFrameworkCore;

namespace Demo.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        Registration.RegisterServices(builder.Services,builder.Configuration);
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseStaticFiles();
        app.UseHttpsRedirection();

        app.UseCors("MyPolicy");

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

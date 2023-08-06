using Microsoft.EntityFrameworkCore;
using UniversityRestApi.Data;
using UniversityRestApi.Exceptions;
using UniversityRestApi.Services;
using UniversityShared.Mapping;
using UniversityShared.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("UniversityContext");

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add<UniversityResponseExceptionFilter>());


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UniversityContext>((options) => options.UseSqlServer(connectionString));
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddScoped<Repository<Course>>();
builder.Services.AddScoped<Repository<Student>>();
builder.Services.AddScoped<Repository<Department>>();
builder.Services.AddScoped<CoursesService>();
builder.Services.AddScoped<StudentsService>();
builder.Services.AddScoped<DepartmentsService>();
builder.Services.AddCors(policy => {
    policy.AddPolicy("CorsPolicy", opt =>
    {
        opt.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<UniversityContext>();
    DataSeeder.SeedData(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();

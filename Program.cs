using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniversityRestApi.Data;
using UniversityRestApi.Mapping;
using UniversityRestApi.Models;
using UniversityRestApi.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("UniversityContext");

// Add services to the container.

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UniversityContext>((options) => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddScoped<Repository<Course>>();
builder.Services.AddScoped<Repository<Student>>();
builder.Services.AddScoped<CoursesService>();
builder.Services.AddScoped<StudentsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

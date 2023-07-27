using AutoMapper;
using UniversityRestApi.Dto;
using UniversityRestApi.Models;

namespace UniversityRestApi.Mapping;
public class MapperConfig
{
    public static Mapper InitializeAutomapper()
    {
        var config = new MapperConfiguration((config) => {
            config.CreateMap<Course, CourseDto>().ReverseMap();
        });

         return new Mapper(config);
    }
}

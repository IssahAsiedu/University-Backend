using AutoMapper;
using UniversityRestApi.Dto;
using UniversityRestApi.Models;

namespace UniversityRestApi.Mapping;

public class MapperConfig: Profile
{
   public MapperConfig() 
    {
        CreateMap<Course, CourseDto>().ReverseMap();

        CreateMap<Course, CourseCreatedDto>();
    }
}

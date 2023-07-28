using AutoMapper;
using System.Globalization;
using UniversityRestApi.Dto;
using UniversityRestApi.Models;

namespace UniversityRestApi.Mapping;

public class MapperConfig: Profile
{
   public MapperConfig() 
    {
        CreateMap<Course, CourseCreationData>().ReverseMap();

        CreateMap<Course, CourseResponseData>();

        CreateMap<StudentRegistrationData, Student>();

        CreateMap<Student, StudentRegistrationResponseData>();

        CreateMap<Student, StudentResponseData>();

        CreateMap<Enrollment, EnrollmentResponseData>();

        CreateMap<EnrollmentCreationData, Enrollment>();

        CreateMap<string, DateTimeOffset>().ConvertUsing(new StringToDateConverter());

        CreateMap<DateTimeOffset, string>().ConvertUsing(new DateToStringConverter());
    }
}


public class StringToDateConverter : ITypeConverter<string, DateTimeOffset>
{
    public DateTimeOffset Convert(string source, DateTimeOffset destination, ResolutionContext context)
    {
        DateTimeOffset result;

        DateTimeOffset.TryParseExact(source, "yyyy-MM-dd", null, DateTimeStyles.AssumeUniversal, out result);

        return result;
    }
}

public class DateToStringConverter : ITypeConverter<DateTimeOffset, string>
{
    public string Convert(DateTimeOffset source, string destination, ResolutionContext context)
    {
        return source.ToString("yyyy-MM-dd");
    }
}
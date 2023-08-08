using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniversityDto;
using UniversityRestApi.Data;
using UniversityRestApi.Exceptions;
using UniversityShared.Models;

namespace UniversityRestApi.Services;
public class InstructorsService
{

    private readonly Repository<Instructor> instructorsRepo;
    private readonly Repository<Course> coursesRepo;
    private readonly IMapper mapper;

    public InstructorsService(Repository<Instructor> instructorsRepo, Repository<Course> coursesRepo,IMapper mapper)
    {
        this.instructorsRepo = instructorsRepo;
        this.coursesRepo = coursesRepo;
        this.mapper = mapper;
    }

    public async Task<InstructorPaginationData> GetInstructors(PaginationFilter filter)
    {
        static IQueryable<Instructor> query(IQueryable<Instructor> query)
        {
            return query
                .Include(i => i.Courses)
                .ThenInclude(c => c.Department)
                .Include(i => i.Courses)
                .ThenInclude(c => c.Enrollments)
                .ThenInclude(e => e.Student)
            .Include(i => i.OfficeAssignment);
        }

        var paginatedData = await instructorsRepo.GetAll(filter, query);

        var items = mapper.Map<List<InstructorDto>>(paginatedData.Items);

        return new InstructorPaginationData(items)
        {
            Count = paginatedData.TotalItems,
            CurrentIndex = paginatedData.CurrentIndex
        };

    }

    public async Task<InstructorDto> Register(InstructorRegistrationData data)
    {
        var instructor = mapper.Map<Instructor>(data);
        instructor.ID = Guid.NewGuid();

        
        foreach(var id in data.Courses)
        {
            var course = await coursesRepo.FilterForFirst((q) => q.Where((c) => c.ID.Equals(Guid.Parse(id))));
            if (course == null)
            {
                var exception = new UniversityException(StatusCodes.Status400BadRequest);
                throw exception;
            }
            instructor.Courses.Add(course!);
        }

        if(data.Office != null)
        {
            OfficeAssignment assignment = new ()
            {
                InstructorID = instructor.ID,
                Location = data.Office
            };
            instructor.OfficeAssignment = assignment;
        }

        await instructorsRepo.Create(instructor);

        return mapper.Map<InstructorDto>(instructor);
    }
}

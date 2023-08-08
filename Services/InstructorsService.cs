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

        var paginatedData = await instructorsRepo.GetAll(filter, IncludeProperties);

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

    public async Task UpdateInstructor(Guid id, InstructorUpdateData data)
    {
       var instructor = await instructorsRepo.FilterForFirst((q) => IncludeProperties(q).Where(i => i.ID.Equals(id)));

        if (instructor == null)
        {
            throw new UniversityException(StatusCodes.Status400BadRequest);
        }

        mapper.Map(data, instructor);

        instructor.Courses.RemoveAll((c) => !data.Courses.Contains(c.ID.ToString()));

        foreach(var cid in data.Courses)
        {
           var teaching = instructor.Courses.Any(c =>  c.ID.Equals(Guid.Parse(cid)));

           if(teaching)
            {
                continue;
            }

            var course = await coursesRepo.FilterForFirst((q) => q.Where((c) => c.ID.Equals(Guid.Parse(cid))));
            if (course == null)
            {
                var exception = new UniversityException(StatusCodes.Status400BadRequest);
                throw exception;
            }
            instructor.Courses.Add(course!);
        }

        if(data.Office != null)
        {
            if(instructor.OfficeAssignment == null)
            {
                OfficeAssignment assignment = new()
                {
                    InstructorID = instructor.ID,
                    Location = data.Office
                };
                instructor.OfficeAssignment = assignment;
            } else
            {
                instructor.OfficeAssignment.Location = data.Office;
            }
        }

        await instructorsRepo.Save();
    }

    public async Task<InstructorDto> GetInstructor(Guid id)
    {
        var instructor = await instructorsRepo.FilterForFirst((q) => IncludeProperties(q).Where(i => i.ID.Equals(id)))
            ?? throw new UniversityException(StatusCodes.Status400BadRequest);
        return mapper.Map<InstructorDto>(instructor);
    }

    private static IQueryable<Instructor> IncludeProperties(IQueryable<Instructor> query)
    {
        return query
            .Include(i => i.Courses)
            .ThenInclude(c => c.Department)
            .Include(i => i.Courses)
            .ThenInclude(c => c.Enrollments)
            .ThenInclude(e => e.Student)
        .Include(i => i.OfficeAssignment);
    }
}

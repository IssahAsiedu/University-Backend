using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniversityDto;
using UniversityRestApi.Data;
using UniversityShared.Models;

namespace UniversityRestApi.Services;
public class InstructorsService
{

    private readonly Repository<Instructor> repository;
    private readonly IMapper mapper;

    public InstructorsService(Repository<Instructor> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<InstructorPaginationData> GetInstructors(PaginationFilter filter)
    {
        static IQueryable<Instructor> query(IQueryable<Instructor> query)
        {
            return query.Include(i => i.Courses).ThenInclude(c => c.Enrollments).ThenInclude(e => e.Student)
            .Include(i => i.OfficeAssignment);
        }

        var paginatedData = await repository.GetAll(filter, query);

        var items = mapper.Map<List<InstructorDto>>(paginatedData.Items);

        return new InstructorPaginationData(items)
        {
            Count = paginatedData.TotalItems,
            CurrentIndex = paginatedData.CurrentIndex
        };

    }
}

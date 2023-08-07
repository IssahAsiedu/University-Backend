using UniversityDto;
using UniversityRestApi.Data;
using UniversityShared.Models;

namespace UniversityRestApi.Services;
public class InstructorsService
{

    private readonly Repository<Instructor> repository;

    public InstructorsService(Repository<Instructor> repository)
    {
        this.repository = repository;
    }

    public Task<InstructorPaginationData> GetInstructors(PaginationFilter filter)
    {
        throw new NotImplementedException();
    }
}

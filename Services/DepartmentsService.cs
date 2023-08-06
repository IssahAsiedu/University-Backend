using AutoMapper;
using UniversityDto;
using UniversityRestApi.Data;
using UniversityShared.Models;

namespace UniversityRestApi.Services;
public class DepartmentsService
{
    private readonly Repository<Department> departmentsRepo;
    private readonly IMapper mapper;

    public DepartmentsService(Repository<Department> departmentsRepo, IMapper mapper)
    {
        this.departmentsRepo = departmentsRepo;
        this.mapper = mapper;
    }


    public async Task<ICollection<DepartmentResponseData>> GetDepartments()
    {
        PaginationFilter paginationFilter = new PaginationFilter() { PageSize = 20 };
        var paginatedData = await departmentsRepo.GetAll(paginationFilter);
        var result = mapper.Map<List<DepartmentResponseData>>(paginatedData.Items);
        return result;
    }
}

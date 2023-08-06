using UniversityDto;
using UniversityRestApi.Data;
using UniversityShared.Models;

namespace UniversityRestApi.Services;
public class DepartmentsService
{
    private readonly Repository<Department> departmentsRepo;

    public DepartmentsService(Repository<Department> departmentsRepo)
    {
        this.departmentsRepo = departmentsRepo;
    }


    public async Task<ICollection<Department>> GetDepartments()
    {
        PaginationFilter paginationFilter = new PaginationFilter() { PageSize = 20 };
        var result = await departmentsRepo.GetAll(paginationFilter);
        return result.Items;
    }
}

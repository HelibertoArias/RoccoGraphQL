// <copyright file="EmployeeRepository.cs" company="Rocco Company">
// Copyright (c) 2022, Heliberto Arias
// </copyright>

using Microsoft.EntityFrameworkCore;
using RoccoGraphQL.Domain;

namespace RoccoGraphQL.Persistence.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly RoccoContext _dbContext;

    public EmployeeRepository(RoccoContext repositoryContext)
    {
        this._dbContext = repositoryContext;
    }

    // More about how to use ILookup https://bit.ly/3pOIlhk
    public async Task<ILookup<Guid, Employee>> GetEmployeesByCompanyId(IEnumerable<Guid> companiesIds)
    {
        var employees = await _dbContext.Employees
                                .AsNoTracking()
                                .Where(x => companiesIds.Contains(x.CompanyId))
                                .ToListAsync();

        return employees.ToLookup(x => x.CompanyId);
    }
}

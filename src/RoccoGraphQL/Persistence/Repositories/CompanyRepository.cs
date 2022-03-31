// <copyright file="CompanyRepository.cs" company="Rocco Company">
// Copyright (c) 2022, Heliberto Arias
// </copyright>

using Microsoft.EntityFrameworkCore;
using RoccoGraphQL.Domain;
using System.Linq.Expressions;

namespace RoccoGraphQL.Persistence.Repositories;
public class CompanyRepository : ICompanyRepository
{

    private readonly RoccoContext _dbContext;

    public CompanyRepository(RoccoContext repositoryContext)
    {
        this._dbContext = repositoryContext;
    }

    public async Task<Company?> FindOneByCondition(Expression<Func<Company, bool>> expression)
    {

        return
             await _dbContext.Set<Company>()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(expression);


    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync().ConfigureAwait(true);
    }

    public async Task Add(Company entity)
    {
        await _dbContext.Set<Company>().AddAsync(entity).ConfigureAwait(true);
    }

    public IQueryable<Company> FindAll()
    {
        return _dbContext.Set<Company>().AsNoTracking();

    }
}

// <copyright file="ICompanyRepository.cs" company="Rocco Company">
// Copyright (c) 2022, Heliberto Arias
// </copyright>

using RoccoGraphQL.Domain;
using System.Linq.Expressions;

namespace RoccoGraphQL.Persistence.Repositories;
public interface ICompanyRepository
{
    Task<Company> FindOneByCondition(Expression<Func<Company, bool>> expression);

    Task SaveChangesAsync();

    Task Add(Company entity);

    IQueryable<Company> FindAll();

}

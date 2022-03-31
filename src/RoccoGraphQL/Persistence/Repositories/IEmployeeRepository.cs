// <copyright file="IEmployeeRepository.cs" company="Rocco Company">
// Copyright (c) 2022, Heliberto Arias
// </copyright>

using RoccoGraphQL.Domain;

namespace RoccoGraphQL.Persistence.Repositories;

public interface IEmployeeRepository
{
    Task<ILookup<Guid, Employee>> GetEmployeesByCompanyId(IEnumerable<Guid> companiesIds);
}

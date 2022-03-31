// <copyright file="CompanyType.cs" company="Rocco Company">
// Copyright (c) 2022, Heliberto Arias
// </copyright>

using GraphQL.DataLoader;
using GraphQL.Types;
using RoccoGraphQL.Domain;
using RoccoGraphQL.Persistence.Repositories;

namespace RoccoGraphQL.GraphQL.Types;

public class CompanyType : ObjectGraphType<Company>
{
    public CompanyType(IEmployeeRepository employeeRepository,
                        IDataLoaderContextAccessor dataLoaderContextAccessor)
    {
        Name = nameof(Company);
        Description = "Company Type";
        Field(x => x.Id);
        Field(x => x.Name).Description("The name of the company");
        Field(x => x.Address).Description("Max 200 characters");
        Field(x => x.Country);
        Field<ListGraphType<EmployeeType>>(
            "employees",
            resolve: context =>
            {
                // This will make a query per company
                //return employeeRepository.FindAllByCondition(x => x.CompanyId == context.Source.Id).ToList();

                // Improve performance reducing queries to the database
                var loader = dataLoaderContextAccessor
                            .Context
                            .GetOrAddCollectionBatchLoader<Guid, Employee>(
                    "GetEmployeesByCompany",
                    employeeRepository.GetEmployeesByCompanyId);

                return loader.LoadAsync(context.Source.Id);
            });
    }
}

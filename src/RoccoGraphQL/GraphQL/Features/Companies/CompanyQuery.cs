// <copyright file="CompanyQuery.cs" company="Rocco Company">
// Copyright (c) 2022, Heliberto Arias
// </copyright>

using GraphQL;
using GraphQL.Types;
using RoccoGraphQL.GraphQL.Types;
using RoccoGraphQL.Persistence.Repositories;

namespace RoccoGraphQL.GraphQL.Features.Companies;

public class CompanyQuery : ObjectGraphType
{
    public CompanyQuery(ICompanyRepository companyRepository)
    {
        Field<ListGraphType<CompanyType>>(
            name: "companies",
            resolve: context => companyRepository.FindAll()
        );

        Field<CompanyType>(
            name: "company",
            arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>>
            { Name = "id" }),
            resolve: context =>
           {
               var id = context.GetArgument<Guid>("id");
               return companyRepository.FindOneByCondition(x => x.Id == id);
           });
    }
}

// <copyright file="CompanyInputType.cs" company="Rocco Company">
// Copyright (c) 2022, Heliberto Arias
// </copyright>

using GraphQL.Types;

namespace RoccoGraphQL.GraphQL.Types;

public class CompanyInputType : InputObjectGraphType
{
    public CompanyInputType()
    {
        Name = "companyInput";
        Field<NonNullGraphType<StringGraphType>>("name");
        Field<StringGraphType>("Address");
        Field<StringGraphType>("Country");
    }
}

// <copyright file="Employee.cs" company="Rocco Company">
// Copyright (c) 2022, Heliberto Arias
// </copyright>

namespace RoccoGraphQL.Domain;
public class Employee
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public int Age { get; set; }

    public string Position { get; set; } = null!;

    public Guid CompanyId { get; set; }

    public Company Company { get; set; } = null!;

}


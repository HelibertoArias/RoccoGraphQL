// <copyright file="Company.cs" company="Rocco Company">
// Copyright (c) 2022, Heliberto Arias
// </copyright>

namespace RoccoGraphQL.Domain;

public class Company
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Country { get; set; } = null!;

    public ICollection<Employee> Employees { get; set; } = null!;
}

// <copyright file="RoccoContext.cs" company="Rocco Company">
// Copyright (c) 2022, Heliberto Arias
// </copyright>

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RoccoGraphQL.Domain;

namespace RoccoGraphQL.Persistence.Repositories;

// Working with DbContext - EF6 : https://bit.ly/3gUZK3k
public class RoccoContext : DbContext
{
    public RoccoContext(DbContextOptions<RoccoContext> options) : base(options)
    {
        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }

    }

    public DbSet<Company> Companies { get; set; } = null!;

    public DbSet<Employee> Employees { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Add All entity configurations in a dynamic way 
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}


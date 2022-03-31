// <copyright file="Program.cs" company="Rocco Company">
// Copyright (c) 2022, Heliberto Arias
// </copyright>

using GraphQL.MicrosoftDI;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.SystemTextJson;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using RoccoGraphQL.GraphQL.Features.Companies;
using RoccoGraphQL.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration configuration = builder.Configuration;


// Connection String
var stringConnection = "RoccoConnectionString";
var connectionConfiguration = configuration.GetConnectionString(stringConnection);
if (connectionConfiguration == null)
{
    throw new ArgumentNullException(nameof(connectionConfiguration).ToString(),
            message: $"{stringConnection} doesn't exist in your appsetings.json");
}

builder.Services.AddDbContext<RoccoContext>(options =>
  options.UseSqlServer(connectionConfiguration)
    .EnableSensitiveDataLogging() // This allow to see the SQL operations in console
);

// Setting up repositories
// Registering by one
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

// add company schema
builder.Services.AddScoped<CompanyQuery>();
builder.Services.AddScoped<ISchema, CompanySchema>(services => new CompanySchema(new SelfActivatingServiceProvider(services)));


// register graphQL 
builder.Services.AddGraphQL(options =>
{
    options.EnableMetrics = true;


})
    .AddGraphTypes(ServiceLifetime.Scoped)


.AddSystemTextJson()
.AddErrorInfoProvider(opt =>
                opt.ExposeExceptionStackTrace = true // Set to false to ommit "extension" in the reponse
).AddDataLoader() // To improve performance using cache in data
.AddWebSockets();

builder.Services.AddCors();

// default setup
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "GraphQLNetExample", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GraphQLNetExample v1"));


}

app.UseHttpsRedirection();

app.UseCors();

app.UseWebSockets();
app.UseGraphQLWebSockets<CompanySchema>(path: "/graphql");
app.UseGraphQL<ISchema>();
// add playground UI to development only   
app.UseGraphQLPlayground(new PlaygroundOptions() { GraphQLEndPoint = "/graphql" }, path: "/ui/playground");

app.UseAuthorization();


app.Run();

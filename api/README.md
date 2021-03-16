# CQRS-Student-Management
Student management app re-created from the [CQRS in Practice Pluralsight course](https://www.pluralsight.com/courses/cqrs-in-practice), demonstrating CQRS, The Clean Architecture, and some DDD principles.

## Getting Started

1. Clone the repo
2. Add a config.json to the src/StudentManagement.Api, tests/StudentManagement.ApiTests, and tests/StudentManagement.InfrastructureTests projects. Add a DbConnectionString property with a connection string for your MySQL database.
3. From the solution root directory, run `dotnet test` to run all tests, and verify they pass.
4. From the solution root directory, run `dotnet run --project src/StudentManagement.Api` to run the Api project. Use a HTTP client or the [accompanying React project](https://github.com/jlwalkerlg/student-management-client) to interact with the API.

## Solution structure

The solution contains 4 projects in the src/ directory for each of the layers of The Clean Architecture: Domain, Application, Infrastructure, and Api (Web/UI). The tests/ directory contains a corresponding test project for each src/ project.

## Architecture

The architecture follows the principles of The Clean Architecture, with some exceptions where deemed appropriate.

Namely, the entites are not entirely separate from the ORM (EF Core) since doing so would not be worth it.

Also, there are two external libraries used in the Application layer that provide a lot of benefit: MediatR and Fluent Validation.

There is a separate branch named "BeforeMediatR" which shows how to set up command and query handlers without using MediatR, along with the decorator attributes.

Each command/query handler returns a Result object, signifying whether or not the request was successful. If so, the Result can contain optional data; otherwise, it will contain an Error representing the error.

Almost none of the commands return any data, with the exception of the RegisterStudentCommandHandler, which returns an ID for the new student.

There are separate repository implementations for the command and query sides, with the query side specifying DTO repositories whose methods return simple DTOs rather than domain entities. The StudentManagement.Api currently uses EF Core for both command and query repositories, but there are also DTO repository implementations using Dapper that could be wired up.

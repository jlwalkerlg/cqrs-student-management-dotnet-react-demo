using Xunit;
using StudentManagement.Application.Students;
using StudentManagement.Application;
using Microsoft.Extensions.DependencyInjection;
using System;
using MediatR;

namespace StudentManagement.ApiTests.Functional
{
    public class ContainerTests : ApiTestBase
    {
        public ContainerTests(WebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public void It_Resolves_Command_Handlers()
        {
            var handler = provider.GetService<IRequestHandler<RegisterStudentCommand, Result<Guid>>>();

            Assert.NotNull(handler);
        }
    }
}

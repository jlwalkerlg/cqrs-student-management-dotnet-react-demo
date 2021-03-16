using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using StudentManagement.ApiTests.Doubles;
using Xunit;

namespace StudentManagement.ApiTests.Functional
{
    public class ExceptionHandlingTests : ApiTestBase
    {
        private readonly MediatorSpy mediatorSpy;

        public ExceptionHandlingTests(WebAppFactory factory) : base(factory)
        {
            mediatorSpy = new MediatorSpy();
        }

        protected override void ConfigureWebHost(IWebHostBuilder config)
        {
            config.ConfigureServices(services =>
            {
                services.AddScoped<IMediator>(provider => mediatorSpy);
            });
        }

        [Fact]
        public async Task It_Catches_Unhandled_Exceptions()
        {
            // returning null from IMediator will cause
            // a cast exception to be thrown
            mediatorSpy.Response = null;

            var response = await Get("/students");
            var json = await response.ToJson();

            Assert.Equal(500, response.StatusCode);
            Assert.NotNull(json["error"]);
        }
    }
}

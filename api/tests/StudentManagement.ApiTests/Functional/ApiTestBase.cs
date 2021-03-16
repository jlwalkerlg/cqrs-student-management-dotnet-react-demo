using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using StudentManagement.Infrastructure.Data.EF;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using StudentManagement.Api;

namespace StudentManagement.ApiTests.Functional
{
    [Collection("Sequential")]
    public abstract class ApiTestBase : IClassFixture<WebAppFactory>, IDisposable
    {
        protected readonly WebApplicationFactory<Startup> factory;
        private readonly IServiceScope scope;
        protected readonly IServiceProvider provider;
        protected readonly HttpClient client;
        protected readonly AppDbContext context;

        public ApiTestBase(WebAppFactory fixture)
        {
            factory = fixture.WithWebHostBuilder(ConfigureWebHost);
            scope = factory.Services.CreateScope();
            provider = scope.ServiceProvider;
            client = factory.CreateClient();

            context = provider.GetService<AppDbContext>();
            context.Database.EnsureCreated();
        }

        protected virtual void ConfigureWebHost(IWebHostBuilder config)
        {
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
            scope.Dispose();
        }

        protected async Task<TestResponse> Get(string path)
        {
            var response = await client.GetAsync(path);
            return new TestResponse(response);
        }

        protected async Task<TestResponse> Post(string path, object data)
        {
            var requestJson = JsonSerializer.Serialize(data);
            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(path, content);
            return new TestResponse(response);
        }

        protected async Task<TestResponse> Put(string path, object data)
        {
            var requestJson = JsonSerializer.Serialize(data);
            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(path, content);
            return new TestResponse(response);
        }

        protected async Task<TestResponse> Delete(string path)
        {
            var response = await client.DeleteAsync(path);
            return new TestResponse(response);
        }
    }
}

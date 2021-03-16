using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace StudentManagement.ApiTests.Functional
{
    public class TestResponse
    {
        private readonly HttpResponseMessage response;

        public TestResponse(HttpResponseMessage response)
        {
            this.response = response;
        }

        public bool IsSuccess => response.IsSuccessStatusCode;
        public bool IsFailure => !IsSuccess;
        public int StatusCode => (int)response.StatusCode;

        public async new Task<string> ToString()
        {
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<JObject> ToJson()
        {
            return JObject.Parse(await ToString());
        }
    }
}

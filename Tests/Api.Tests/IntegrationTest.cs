using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using DataAccess.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Threading.Tasks;
using Api.Network;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

namespace Api.Tests
{
    public class IntegrationTest
    {

        protected readonly HttpClient httpClient;
        protected readonly IApiClient apiClient;


        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
              .WithWebHostBuilder(builder =>
              {
                  builder.ConfigureServices(services =>
                  {
                      services.RemoveAll(typeof(AppDbContext));
                      services.AddDbContext<AppDbContext>(options => { options.UseInMemoryDatabase("TestDb"); });
                  });
              });

            httpClient = appFactory.CreateClient();


        }

        protected async Task<string> DoPostAsync<T>(string url, Dictionary<string, string> headers, T dto)
        {
            var json = JsonConvert.SerializeObject(dto, Formatting.Indented);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, stringContent);

            return await response.Content.ReadAsStringAsync();
        }
    }
}

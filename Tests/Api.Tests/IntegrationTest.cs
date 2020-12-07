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
using Microsoft.Extensions.Configuration;
using System.IO;

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

                  builder.ConfigureAppConfiguration((context, conf) =>
                  {
                      conf.AddJsonFile("appsettings.tests.json", optional: true);
                      conf.AddEnvironmentVariables();
                      conf.Build();
                      
                  });

                  
              });

            httpClient = appFactory.CreateClient();


        }

        protected async Task<string> DoPostAsync<T>(string url, Dictionary<string, string> headers, T dto)
        {
     
            var json = JsonConvert.SerializeObject(dto, Formatting.Indented);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            foreach (var pair in headers)
            {
                httpClient.DefaultRequestHeaders.Add(pair.Key, pair.Value);
            }
            var response = await httpClient.PostAsync(url, stringContent);

            return await response.Content.ReadAsStringAsync();
        }

        protected IConfiguration InitConfiguration()
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.tests.json");

            configPath = configPath.Replace("bin/Debug/netcoreapp3.1/", "");


            var config = new ConfigurationBuilder()
                .AddJsonFile(configPath)
                .Build();
            return config;
        }

    }
}

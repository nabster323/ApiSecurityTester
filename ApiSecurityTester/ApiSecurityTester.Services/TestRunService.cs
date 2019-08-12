using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ApiSecurityTester.Dtos;
using ApiSecurityTester.Interfaces;
using Microsoft.Extensions.Logging;

namespace ApiSecurityTester.Services
{
    public class TestRunService : ITestRunService
    {
        private readonly ILogger<TestRunService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        private const string FAIL_REASON_NO_401 = "The server did not return a 401";
        private const string PASS_401 = "Returned 401";

        public TestRunService(ILogger<TestRunService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<TestRunResult>> RunTests(IEnumerable<SettingsFile> settings)
        {
            var testRunResultTasks = new List<Task<TestRunResult>>();

            foreach (var setting in settings)
                testRunResultTasks.Add(RunResult(setting));

            var testRunResults = await Task.WhenAll(testRunResultTasks);

            return testRunResults;
        }

        private async Task<TestRunResult> RunResult(SettingsFile setting)
        {
            var runItemTasks = new List<Task<TestItemRunResult>>();

            foreach (var runItem in setting.Tests)
                runItemTasks.Add(RunItem(runItem, setting));

            var runItemResults = await Task.WhenAll(runItemTasks);

            return new TestRunResult {
                SettingsFile = setting,
                TestItemRunResults = runItemResults
            };
        }

        private async Task<TestItemRunResult> RunItem(TestItem testItem, SettingsFile setting)
        {
            var failReasons = new List<string>();
            var passes = new List<string>();

            HttpRequestMessage request = null;

            var httpClient = _httpClientFactory.CreateClient();

            foreach (var verb in testItem.Verbs)
            {
                switch (verb.ToLower())
                {
                    case "post":
                        request = new HttpRequestMessage(HttpMethod.Post, $"{setting.BaseUri}{testItem.EndPoint}");
                        break;
                    case "get":
                        request = new HttpRequestMessage(HttpMethod.Get, $"{setting.BaseUri}{testItem.EndPoint}");
                        break;

                    default:
                        break;
                }

                if (request != null)
                {
                    var response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        failReasons.Add($"{FAIL_REASON_NO_401} - Method: {request.Method}");
                    }
                    //TODO: Use the passed expected code
                    else if(response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        passes.Add($"{PASS_401} - Method: {request.Method}");
                    }

                    //TODO: check the response type against provided
                }
            }

            //TODO:
            //Run HTTPS only tests

            return new TestItemRunResult(testItem) {
                Pass = !failReasons.Any(),
                Failures = failReasons,
                Passes = passes
            };
        }

    }
}

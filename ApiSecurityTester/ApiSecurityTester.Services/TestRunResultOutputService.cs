using ApiSecurityTester.Dtos;
using ApiSecurityTester.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSecurityTester.Services
{
    public class TestRunResultOutputService : ITestRunResultOutputService
    {
        public async Task<bool> WriteResultsToTxtFile(TestRunResult testRunResult, string location)
        {
            try
            {
                var runFileName = $"{DateTime.UtcNow:dd-MM-yyyy-hh-mm-ss-f}.ApiSecurityTesterRun.txt";

                var runFileLocation = Path.Combine(location, runFileName);

                using (StreamWriter file = new StreamWriter(runFileLocation))
                {
                    await file.WriteLineAsync($"------------------- Test results --------------------");
                    await file.WriteLineAsync($"Test run results from '{testRunResult.SettingsFile.Name}'");
                    await file.WriteLineAsync($"Base Uri: {testRunResult.SettingsFile.BaseUri}");
                    await file.WriteLineAsync($"Only Https: {testRunResult.SettingsFile.OnlyHttps}");

                    await file.WriteLineAsync($"-- Individual tests and results ({testRunResult.TestItemRunResults.Count()} tests) --");

                    var currentTestNumber = 1;

                    foreach (var testResult in testRunResult.TestItemRunResults)
                    {
                        await file.WriteLineAsync($"Test # {currentTestNumber}");
                        await file.WriteLineAsync($"Test endpoint: {testResult.TestItem.EndPoint}");
                        await file.WriteLineAsync($"Test expected response code: {testResult.TestItem.ExpectedHttpResponseCode}");
                        await file.WriteLineAsync($"Test expected content type: {testResult.TestItem.ExpectedHttpResponseContentType}");
                        await file.WriteLineAsync($"Test result: {(testResult.Pass ? "Pass" : "Fail")}");

                        await file.WriteLineAsync($"===FAILURE ITEMS===");

                        if (testResult.Failures.Any())
                            foreach (var failure in testResult.Failures)
                                await file.WriteLineAsync($"{failure}");
                        else
                            await file.WriteLineAsync($"## No failure items ##");


                        await file.WriteLineAsync($"===PASS ITEMS===");

                        if (testResult.Passes.Any())
                            foreach (var pass in testResult.Passes)
                            await file.WriteLineAsync($"{pass}");
                        else
                            await file.WriteLineAsync($"## No pass items ##");

                        currentTestNumber++;
                    }

                    await file.WriteLineAsync($"------------------- End of Test results --------------------");
                }

                return true;
            }
            catch (Exception ex)
            {
                //Log here....throw perhaps?
                return false;
            }
        }
    }
}

using ApiSecurityTester.Interfaces;
using ApiSecurityTester.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ApiSecurityTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{DateTime.UtcNow} - Welcome to ApiSecurityTester version {Assembly.GetEntryAssembly().GetName().Version}");

            try
            {
                var serviceCollection = BuildServiceCollection();

                using (var serviceProvider = serviceCollection.BuildServiceProvider())
                {
                    var configLoader = serviceProvider.GetService<ITestConfigurationService>();

                    var testRunService = serviceProvider.GetService<ITestRunService>();

                    var testRunOutputService = serviceProvider.GetService<ITestRunResultOutputService>();

                    //N.B - might be safer to use the current directory always. Especially if it's running from CI
                    var directoryToUse = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

                    var tests = configLoader.LoadConfigurationsForTests(directoryToUse).Result;

                    if (tests.Any())
                    {
                        Console.WriteLine($"{DateTime.UtcNow} - Starting tests now...");

                        var results = testRunService.RunTests(tests).Result;

                        Console.WriteLine($"{DateTime.UtcNow} - Test run finished");

                        Console.WriteLine($"{DateTime.UtcNow} - Outputting results to log file.");

                        foreach (var result in results)
                        {
                            testRunOutputService.WriteResultsToTxtFile(result, directoryToUse);
                        }

                        Console.WriteLine($"{DateTime.UtcNow} - Done outputting.");
                    }
                    else
                    {
                        Console.WriteLine($"{DateTime.UtcNow} - There are no tests to run. Skipping.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.UtcNow} - There was a problem. It's probably a good idea to read this (or you can throw the machine in the bin) {ex}");
            }

            Console.WriteLine($"{DateTime.UtcNow} - Exiting....");
        }

        private static IServiceCollection BuildServiceCollection()
        {
            return new ServiceCollection()
                .AddLogging(cfg => { cfg.AddConsole(); })
                .AddHttpClient()
                .AddScoped<ITestConfigurationService, TestConfigurationService>()
                .AddScoped<ITestRunService, TestRunService>()
                .AddScoped<ITestRunResultOutputService, TestRunResultOutputService>();
        }
    }
}

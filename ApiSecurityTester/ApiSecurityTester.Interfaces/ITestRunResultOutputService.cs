using ApiSecurityTester.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiSecurityTester.Interfaces
{
    public interface ITestRunResultOutputService
    {
        Task<bool> WriteResultsToTxtFile(TestRunResult testRunResult, string location);
    }
}

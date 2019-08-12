using ApiSecurityTester.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiSecurityTester.Interfaces
{
    public interface ITestRunService
    {
        Task<IEnumerable<TestRunResult>> RunTests(IEnumerable<SettingsFile> settings);
    }
}

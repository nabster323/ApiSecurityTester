using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSecurityTester.Dtos
{
    public class TestRunResult
    {
        public SettingsFile SettingsFile { get; set; }
        public IEnumerable<TestItemRunResult> TestItemRunResults { get; set; }
    }
}

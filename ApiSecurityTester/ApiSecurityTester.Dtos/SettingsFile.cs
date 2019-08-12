using System;
using System.Collections.Generic;

namespace ApiSecurityTester.Dtos
{
    public class SettingsFile
    {
        public string Name { get; set; }
        public string BaseUri { get; set; }
        public bool OnlyHttps { get; set; }
        public List<TestItem> Tests { get; set; }
    }
}

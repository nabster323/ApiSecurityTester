using System;
using System.Collections.Generic;
using System.Text;

namespace ApiSecurityTester.Dtos
{
    public class TestItem
    {
        public string EndPoint { get; set; }
        public string ExpectedHttpResponseContentType { get; set; }
        public int ExpectedHttpResponseCode { get; set; }
        public List<string> Verbs { get; set; }
    }
}

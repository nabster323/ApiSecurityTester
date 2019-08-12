using System.Collections.Generic;

namespace ApiSecurityTester.Dtos
{
    public class TestItemRunResult
    {
        public TestItem TestItem { get; private set; }
        public TestItemRunResult(TestItem testItem)
        {
            TestItem = testItem;
        }

        public bool Pass { get; set; }
        public IEnumerable<string> Failures { get; set; }
        public IEnumerable<string> Passes { get; set; }
    }
}

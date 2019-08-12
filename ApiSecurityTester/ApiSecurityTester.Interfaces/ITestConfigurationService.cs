using ApiSecurityTester.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiSecurityTester.Interfaces
{
    public interface ITestConfigurationService
    {
        Task<IEnumerable<SettingsFile>> LoadConfigurationsForTests(string location);
    }
}

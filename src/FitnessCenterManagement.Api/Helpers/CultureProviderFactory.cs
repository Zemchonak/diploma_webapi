using System.Collections.Generic;
using System.Globalization;

namespace FitnessCenterManagement.Api.Helpers
{
    public interface ICultureProviderFactory
    {
        IReadOnlyCollection<CultureInfo> GetCultures();
    }

    public class CultureProviderFactory : ICultureProviderFactory
    {
        public IReadOnlyCollection<CultureInfo> GetCultures()
        {
            return new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("ru-RU"),
                new CultureInfo("be-BY"),
            }.AsReadOnly();
        }
    }
}
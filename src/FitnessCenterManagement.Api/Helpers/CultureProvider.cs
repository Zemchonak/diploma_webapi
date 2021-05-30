using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FitnessCenterManagement.Api.Helpers
{
    public interface ICultureProvider
    {
        IReadOnlyCollection<CultureInfo> Cultures { get; }

        string GetFullCulture(string twoLetteredCulture);
    }

    internal class CultureProvider : ICultureProvider
    {
        public CultureProvider(ICultureProviderFactory factory)
        {
            Cultures = factory.GetCultures();
        }

        public IReadOnlyCollection<CultureInfo> Cultures { get; set; }

        public string GetFullCulture(string twoLetteredCulture)
        {
            var value = Cultures.FirstOrDefault(one => one.Name
                .Contains(twoLetteredCulture, System.StringComparison.InvariantCultureIgnoreCase));
            return (value is null) ? twoLetteredCulture : value.Name;
        }
    }
}
using System.Threading.Tasks;
using Prism.AppModel;
using Gem.Bindings;

namespace Gem
{
    public class DefaultInitializer : AppInitializer
    {
        public DefaultInitializer(GemAppOptions appOptions, ApplicationStore applicationStore, ViewModelBaseServices viewModelBaseServices) : base(appOptions, applicationStore, viewModelBaseServices)
        {
        }

        public override async Task InitializeAsync()
        {
            
        }
    }
}

// ========================================================================================================================================================
// author      : david sexton (@sextondjc | sextondjc.com)
// modified    : 2020.06.21 (19:59)
// site        : https://www.github.com/syrx
// ========================================================================================================================================================

using Microsoft.Extensions.DependencyInjection;

namespace Syrx.Extensions
{
    public class SyrxOptionsBuilder
    {
        public IServiceCollection ServiceCollection { get; }

        public SyrxOptionsBuilder(IServiceCollection services)
        {
            ServiceCollection = services ?? new ServiceCollection();
        }
    }
}
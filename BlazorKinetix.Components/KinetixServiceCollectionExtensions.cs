using BlazorKinetix.Core;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorKinetix.Components;

/// <summary>Registers BlazorKinetix services.</summary>
public static class KinetixServiceCollectionExtensions
{
    /// <summary>Registers the text catalog used by BlazorKinetix components.</summary>
    public static IServiceCollection AddBlazorKinetix(this IServiceCollection services, Action<KinetixTexts>? configure = null)
    {
        var texts = new KinetixTexts();
        configure?.Invoke(texts);
        services.AddSingleton(texts);
        return services;
    }
}

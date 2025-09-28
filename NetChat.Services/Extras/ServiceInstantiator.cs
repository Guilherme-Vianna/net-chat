using Microsoft.Extensions.DependencyInjection;
using NetChat.Services.Interfaces;

namespace NetChat.Services.Extras;

public static class ServiceInstantiator
{
    public static T GetService<T>(IServiceProvider serviceProvider)
    {
        var service = serviceProvider.GetService<T>();
        return service ?? throw new Exception("Error when instance the service");
    }
}
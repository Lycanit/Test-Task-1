public static class HandlerRegistrationExtension
{

    public static void RegisterHandlers(this IServiceCollection services)
    {
        var handerInterface = typeof(IHandler);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => p.IsClass && handerInterface.IsAssignableFrom(p));
        foreach (var type in types)
        {
            services.AddSingleton(handerInterface, type);
        }
    }
}
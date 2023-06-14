using Xm.TestTask.Events;

public class HandlerProvider : IHandlerProvider
{
    private readonly IReadOnlyDictionary<string, IHandler> handlersMap;

    public HandlerProvider(IEnumerable<IHandler> handlers)
    {
        var map = new Dictionary<string, IHandler>();
        foreach (var handler in handlers)
        {
            if (map.ContainsKey(handler.DataType))
            {
                throw new ApplicationException($"A duplicate DataType = {handler.DataType} has been found");
            }
            map[handler.DataType] = handler;
        }
        handlersMap = map;
    }

    public IHandler Provide(string dataType)
    {
        IHandler handler;
        if (!handlersMap.TryGetValue(dataType, out handler))
        {
            throw new ArgumentException($"There are no providers registered for type named {dataType}");
        }
        return handler;
    }

}
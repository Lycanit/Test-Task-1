namespace Xm.TestTask.Events
{
    public class EventListener
    {
        private readonly IEventBus _eventBus;
        private readonly IHandlerProvider _handlerProvider;

        public EventListener(IEventBus eventBus, IHandlerProvider handlerProvider)
        {
            _eventBus = eventBus;
            _handlerProvider = handlerProvider;
        }

        public void StartListening()
        {
            Task.Run(async () =>
            {
                await foreach (var @event in _eventBus.ListenAsync())
                {
                    try
                    {
                        var handler = _handlerProvider.Provide(@event.DataType);
                        handler.Handle(@event.Body);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            });
        }
    }
}
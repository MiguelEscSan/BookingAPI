using BookingApiRest.core.shared.application;

namespace BookingApiRest.core.shared.infrastructure
{
    public class InMemoryEventBus : EventBus
    {
        private readonly List<Delegate> _handlers = new List<Delegate>();

        public void Publish<T>(T eventItem)
        {
            foreach (var handler in _handlers.OfType<Action<T>>())
            {
                handler(eventItem);
            }
        }

        public void Subscribe<T>(Action<T> handler)
        {
            _handlers.Add(handler);
        }
    }
}

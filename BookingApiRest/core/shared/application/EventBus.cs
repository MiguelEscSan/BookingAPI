namespace BookingApiRest.core.shared.application
{
    public interface EventBus
    {
        void Publish<T>(T eventItem);
        void Subscribe<T>(Action<T> handler);

    }
}

namespace BookingApiRest.core.shared.application.results
{
    public class IntResult
    {
        public int Capacity { get; set; }

        public IntResult(int capacity)
        {
            Capacity = capacity;
        }
    }
}

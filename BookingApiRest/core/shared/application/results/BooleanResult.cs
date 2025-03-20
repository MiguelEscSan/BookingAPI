namespace BookingApiRest.core.shared.application.results
{
    public class BooleanResult
    {
        public bool IsSuccess { get; set; }

        public BooleanResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }


}

using BookingApiRest;

public interface Command<T, G> 
    where T : class
    where G : class, T
{
    Result<G> Run(T request);
}

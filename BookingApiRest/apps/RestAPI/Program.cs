using BookingApiRest.core.BookingApp.hotel.application;
using BookingApiRest.Infrastructure.Repositories;
using BookingApp.Hotel.Application.Ports;

namespace BookingApiRest;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddSingleton<HotelRepository, InMemoryHotelRepository>();
        builder.Services.AddSingleton<HotelService>();

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}
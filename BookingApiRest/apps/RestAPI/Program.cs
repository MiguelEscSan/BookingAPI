using BookingApiRest.core.BookingApp.company.application;
using BookingApiRest.core.BookingApp.company.application.ports;
using BookingApiRest.core.BookingApp.company.infrastructure;
using BookingApiRest.core.BookingApp.hotel.application;
using BookingApiRest.core.BookingApp.policy.application;
using BookingApiRest.core.BookingApp.policy.application.DTO;
using BookingApiRest.core.BookingApp.policy.infrastructure;
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

        builder.Services.AddSingleton<CompanyRepository, InMemoryCompanyRepository>();
        builder.Services.AddSingleton<CompanyService>();

        builder.Services.AddSingleton<PolicyRepository, InMemoryPolicyRepository>();
        builder.Services.AddSingleton<PolicyService>();

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}
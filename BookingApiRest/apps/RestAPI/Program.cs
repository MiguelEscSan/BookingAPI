using BookingApiRest.core.BookingApp.company.application.ports;
using BookingApiRest.core.BookingApp.company.application;
using BookingApiRest.core.BookingApp.company.infrastructure;
using BookingApiRest.core.BookingApp.hotel.application;
using BookingApiRest.core.BookingApp.policy.application.DTO;
using BookingApiRest.core.BookingApp.policy.application.handler;
using BookingApiRest.core.BookingApp.policy.application;
using BookingApiRest.core.BookingApp.policy.infrastructure;
using BookingApiRest.core.shared.application;
using BookingApiRest.core.shared.infrastructure;
using BookingApiRest.Infrastructure.Repositories;
using BookingApp.Hotel.Application.Ports;
using BookingApiRest.core.BookingApp.booking.application.ports;
using BookingApiRest.core.BookingApp.booking.infrastructure;
using BookingApiRest.core.BookingApp.booking.application;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddSingleton<EventBus, InMemoryEventBus>();

        // Repositorios
        builder.Services.AddSingleton<HotelRepository, InMemoryHotelRepository>();
        builder.Services.AddSingleton<HotelService>();

        builder.Services.AddSingleton<CompanyRepository, InMemoryCompanyRepository>();
        builder.Services.AddSingleton<CompanyService>();

        builder.Services.AddSingleton<PolicyRepository, InMemoryPolicyRepository>();
        builder.Services.AddSingleton<PolicyService>();

        builder.Services.AddSingleton<BookingRepository, InMemoryBookingRepository>();
        builder.Services.AddSingleton<BookingService>();

        // EventHandlers
        builder.Services.AddSingleton<IEventHandler, NewEmployeeHandler>();
        builder.Services.AddSingleton<IEventHandler, DeleteEmployeeHandler>();

        var app = builder.Build();

        // Suscribir los handlers al EventBus
        var eventBus = app.Services.GetRequiredService<EventBus>();
        var handlers = app.Services.GetServices<IEventHandler>();

        foreach (var handler in handlers)
        {
            eventBus.Subscribe(handler);
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}

using BookingApiRest.core.BookingApp.company.application.ports;
using BookingApiRest.core.BookingApp.company.infrastructure;
using BookingApiRest.Infrastructure.Repositories;
using BookingApp.Hotel.Application.Ports;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApiRest.Test
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        public InMemoryHotelRepository HotelRepository { get; private set; }
        public InMemoryEmployeeRepository EmployeeRepository { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(HotelRepository));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }
                HotelRepository = new InMemoryHotelRepository();
                services.AddSingleton<HotelRepository>(HotelRepository);

            });
        }
    }
}

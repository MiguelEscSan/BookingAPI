using BookingApiRest.core.BookingApp.company.application;
using BookingApiRest.core.BookingApp.company.application.ports;
using BookingApiRest.core.BookingApp.company.domain;
using BookingApiRest.core.shared.exceptions;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace BookingApiRest.Test.company.application;
public class CompanyServiceShould {

    private EmployeeRepository _employeeRepository;
    private CompanyService _companyService;

    [SetUp]
    public void SetUp()
    {
        _employeeRepository = Substitute.For<EmployeeRepository>();
        _companyService = new CompanyService(_employeeRepository);
    }

    [Test]
    public void create_a_single_employee()
    {
        var employeeId = Guid.NewGuid().ToString();
        var companyId = Guid.NewGuid().ToString();
        var employee = new Employee(employeeId, companyId);

        _companyService.AddEmployee(employeeId, companyId);
        var validation = Arg.Is<Employee>(employee => employee.Id == employeeId && employee.CompanyId == companyId);
        
        _employeeRepository.Received().Save(validation);
    }
}


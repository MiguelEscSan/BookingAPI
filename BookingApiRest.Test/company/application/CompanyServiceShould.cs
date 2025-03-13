using BookingApiRest.core.BookingApp.company.application;
using BookingApiRest.core.BookingApp.company.application.ports;
using BookingApiRest.core.BookingApp.company.domain;
using BookingApiRest.core.shared.application;
using BookingApiRest.core.shared.exceptions;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace BookingApiRest.Test.company.application;
public class CompanyServiceShould {

    private CompanyRepository _companyRepository;
    private CompanyService _companyService;
    private EventBus _eventBus;

    [SetUp]
    public void SetUp()
    {
        _companyRepository = Substitute.For<CompanyRepository>();
        _eventBus = Substitute.For<EventBus>();

        _companyService = new CompanyService(_companyRepository, _eventBus);
    }

    [Test]
    public void create_a_single_employee()
    {
        var employeeId = Guid.NewGuid().ToString();
        var companyId = Guid.NewGuid().ToString();

        _companyService.AddEmployee(companyId, employeeId);

        _companyRepository.Received().Save(companyId, Arg.Is<Employee>(e => e.Id == employeeId));
    }

    [Test]
    public void not_allow_creating_employee_that_already_exists()
    {
        var employeeId = Guid.NewGuid().ToString();
        var companyId = Guid.NewGuid().ToString();

        _companyRepository.Exists(employeeId).Returns(true);

        Should.Throw<EmployeeAlreadyExistsException>(() => _companyService.AddEmployee(companyId, employeeId));

        _companyRepository.DidNotReceive().Save(Arg.Any<string>(), Arg.Any<Employee>());
    }


    [Test]
    public void delete_existing_employee()
    {
        var employeeId = Guid.NewGuid().ToString();
        var companyId = Guid.NewGuid().ToString();
        var Employee = new Employee(employeeId, companyId);
        _companyRepository.GetById(employeeId).Returns(Employee);

        _companyService.DeleteEmployee(employeeId);

        _companyRepository.Received().Delete(employeeId);
    }

    [Test]
    public void not_allow_deleting_an_employee_that_does_not_exist()
    {
        var employeeId = Guid.NewGuid().ToString();
        _companyRepository.Exists(employeeId).Returns(false);

        Should.Throw<EmployeeNotFoundException>(() => _companyService.DeleteEmployee(employeeId));

        _companyRepository.DidNotReceive().Delete(Arg.Any<string>());
    }
}


using BookingApiRest.core.BookingApp.company.application;
using BookingApiRest.core.BookingApp.company.application.ports;
using BookingApiRest.core.BookingApp.company.domain;
using BookingApiRest.core.shared.application;
using BookingApiRest.core.shared.exceptions;
using NSubstitute;
using Shouldly;

namespace BookingApiRest.Test.company;
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

}


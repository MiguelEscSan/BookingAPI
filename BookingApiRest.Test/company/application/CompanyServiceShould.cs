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
        var employee = new Employee(companyId, employeeId);

        _companyService.AddEmployee(companyId, employeeId);
        var validation = Arg.Is<Employee>(employee => employee.Id == employeeId && employee.CompanyId == companyId);
        
        _employeeRepository.Received().Save(validation);
    }

    [Test]
    public void not_allow_creating_employe_that_already_exist()
    {
        var employeeId = Guid.NewGuid().ToString();
        var companyId = Guid.NewGuid().ToString();
        var employee = new Employee(companyId, employeeId);
        _employeeRepository.Exists(employeeId).Returns(true);
        
        Should.Throw<EmployeeAlreadyExistsException>(() => _companyService.AddEmployee(companyId, employeeId));

        _employeeRepository.DidNotReceive().Save(Arg.Any<Employee>());
    }

    [Test]
    public void delete_existing_employee()
    {
        var employeeId = Guid.NewGuid().ToString();
        _employeeRepository.Exists(employeeId).Returns(true);

        _companyService.DeleteEmployee(employeeId);

        _employeeRepository.Received().Delete(employeeId);
    }

    [Test]
    public void not_allow_deleting_an_employee_that_does_not_exist()
    {
        var employeeId = Guid.NewGuid().ToString();
        _employeeRepository.Exists(employeeId).Returns(false);

        Should.Throw<EmployeeNotFoundException>(() => _companyService.DeleteEmployee(employeeId));

        _employeeRepository.DidNotReceive().Delete(Arg.Any<string>());
    }
}


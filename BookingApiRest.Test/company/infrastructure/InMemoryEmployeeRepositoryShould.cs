using BookingApiRest.core.BookingApp.company.domain;
using BookingApiRest.core.BookingApp.company.infrastructure;
using BookingApiRest.core.shared.exceptions;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace BookingApiRest.Test.company.infrastructure;
public class InMemoryEmployeeRepositoryShould
{

    private InMemoryCompanyRepository _inMemoryEmployeeRepository;
    private string employeeId;
    private string companyId;

    [SetUp]
    public void SetUp()
    {
        _inMemoryEmployeeRepository = new InMemoryCompanyRepository();
        employeeId = Guid.NewGuid().ToString();
        companyId = Guid.NewGuid().ToString();
    }

    [Test]
    public void save_a_single_employee()
    {
        var employee = new Employee(employeeId, companyId);

        _inMemoryEmployeeRepository.Save(companyId, employee);
        var result = _inMemoryEmployeeRepository._companies[companyId][0];

        result.Id.ShouldBe(employeeId);
    }

    [Test]
    public void check_if_an_employee_exists_by_id()
    {
        var employee = new Employee(employeeId, companyId);
        _inMemoryEmployeeRepository.Save(companyId, employee);

        var result = _inMemoryEmployeeRepository.Exists(employeeId);

        result.ShouldBeTrue();
    }

    [Test]
    public void check_if_an_employee_does_not_exist_by_id()
    {
        var result = _inMemoryEmployeeRepository.Exists(employeeId);
        result.ShouldBeFalse();
    }

    [Test]
    public void delete_an_employee()
    {
        var employee = new Employee(employeeId, companyId);
        _inMemoryEmployeeRepository.Save(companyId, employee);

        _inMemoryEmployeeRepository.Delete(employeeId);

        _inMemoryEmployeeRepository._companies[companyId].Count.ShouldBe(0);
    }

    [Test]
    public void return_an_existing_employee_by_id()
    {
        var employee = new Employee(employeeId, companyId);
        _inMemoryEmployeeRepository.Save(companyId, employee);

        var result = _inMemoryEmployeeRepository.GetById(employeeId);

        result.Id.ShouldBe(employeeId);
    }
}


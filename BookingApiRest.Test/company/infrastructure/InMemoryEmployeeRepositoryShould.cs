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

    [SetUp]
    public void SetUp()
    {
        _inMemoryEmployeeRepository = new InMemoryCompanyRepository();
    }

    [Test]
    public void save_a_single_employee()
    {
        var employee = new Employee("1");

        _inMemoryEmployeeRepository.Save("1", employee);
        var result = _inMemoryEmployeeRepository._companies["1"][0];

        result.Id.ShouldBe("1");
    }

    [Test]
    public void check_if_an_employee_exists_by_id()
    {
        var employee = new Employee("1");
        _inMemoryEmployeeRepository.Save("1", employee);

        var result = _inMemoryEmployeeRepository.Exists("1");

        result.ShouldBeTrue();
    }

    [Test]
    public void check_if_an_employee_does_not_exist_by_id()
    {
        var result = _inMemoryEmployeeRepository.Exists("1");
        result.ShouldBeFalse();
    }

    [Test]
    public void delete_an_employee()
    {
        var employee = new Employee("1");
        _inMemoryEmployeeRepository.Save("1", employee);

        _inMemoryEmployeeRepository.Delete("1");

        _inMemoryEmployeeRepository._companies["1"].Count.ShouldBe(0);
    }

    [Test]
    public void return_an_existing_employee_by_id()
    {
        var employee = new Employee("1");
        _inMemoryEmployeeRepository.Save("1", employee);

        var result = _inMemoryEmployeeRepository.GetById("1");

        result.Id.ShouldBe("1");
    }
}


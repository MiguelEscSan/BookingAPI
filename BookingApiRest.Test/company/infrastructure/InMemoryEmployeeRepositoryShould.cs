using BookingApiRest.core.BookingApp.company.domain;
using BookingApiRest.core.BookingApp.company.infrastructure;
using BookingApiRest.core.shared.exceptions;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace BookingApiRest.Test.company.infrastructure;
public class InMemoryEmployeeRepositoryShould
{

    private InMemoryEmployeeRepository _inMemoryEmployeeRepository;

    [SetUp]
    public void SetUp()
    {
        _inMemoryEmployeeRepository = new InMemoryEmployeeRepository();
    }

    [Test]
    public void save_a_single_employee()
    {
        var employee = new Employee("1", "1");

        _inMemoryEmployeeRepository.Save(employee);
        var result = _inMemoryEmployeeRepository._employees[0];

        result.Id.ShouldBe("1");
        result.CompanyId.ShouldBe("1");
    }
}


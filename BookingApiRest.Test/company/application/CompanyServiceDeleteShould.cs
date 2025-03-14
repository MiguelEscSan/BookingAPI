using BookingApiRest.core.BookingApp.company.application.ports;
using BookingApiRest.core.BookingApp.company.application;
using BookingApiRest.core.shared.application;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApiRest.core.shared.exceptions;
using Shouldly;
using BookingApiRest.core.BookingApp.company.domain;

namespace BookingApiRest.Test.company.application
{
    public class CompanyServiceDeleteShould
    {
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
}

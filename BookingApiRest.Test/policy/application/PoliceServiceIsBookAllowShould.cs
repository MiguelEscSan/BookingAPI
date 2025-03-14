using BookingApiRest.core.BookingApp.company.application.ports;
using BookingApiRest.core.BookingApp.company.domain;
using BookingApiRest.core.BookingApp.policy.application.DTO;
using BookingApiRest.core.BookingApp.policy.application;
using BookingApiRest.Core.Shared.Domain;
using BookingApiRest.core.shared.exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApiRest.core.shared.application;
using NSubstitute;
using Shouldly;
namespace BookingApiRest.Test.policy
{
    class PoliceServiceIsBookAllowShould
    {

        private PolicyRepository _policyRepository;
        private PolicyService _policyService;
        private CompanyRepository _companyRepository;
        private EventBus _eventBus;

        private string companyId;
        private string employeeId;
        private RoomType roomType;

        [SetUp]
        public void SetUp()
        {
            _eventBus = Substitute.For<EventBus>();
            _policyRepository = Substitute.For<PolicyRepository>();
            _companyRepository = Substitute.For<CompanyRepository>();

            _policyService = new PolicyService(_policyRepository, _eventBus, _companyRepository);

            companyId = Guid.NewGuid().ToString();
            employeeId = Guid.NewGuid().ToString();
            roomType = RoomType.Standard;
        }

        [Test]
        public void check_if_booking_is_allow_for_an_employee()
        {
            _policyRepository.EmployeePolicyExists(employeeId).Returns(true);
            _policyRepository.IsEmployeePolicyDefault(employeeId).Returns(false);
            _policyRepository.CheckEmployeePolicy(employeeId, roomType).Returns(true);

            var result = _policyService.IsBookingAllowed(employeeId, roomType);

            result.ShouldBeTrue();
        }

        [Test]
        public void not_allow_booking_of_room_not_allowed_for_employee()
        {
            _policyRepository.EmployeePolicyExists(employeeId).Returns(true);
            _policyRepository.IsEmployeePolicyDefault(employeeId).Returns(false);
            _policyRepository.CheckEmployeePolicy(employeeId, roomType).Returns(false);

            var result = _policyService.IsBookingAllowed(employeeId, roomType);

            result.ShouldBeFalse();
        }

        [Test]
        public void not_allow_booking_for_employee_that_does_not_exist()
        {
            _policyRepository.EmployeePolicyExists(employeeId).Returns(false);

            Assert.Throws<EmployeeNotFoundException>(() => _policyService.IsBookingAllowed(employeeId, roomType));
        }

        [Test]
        public void check_the_company_policy()
        {
            _policyRepository.EmployeePolicyExists(employeeId).Returns(true);
            _policyRepository.IsEmployeePolicyDefault(employeeId).Returns(true);
            _companyRepository.GetCompanyIdByEmployeeId(employeeId).Returns(companyId);
            _policyRepository.CheckCompanyPolicy(companyId, roomType).Returns(true);

            var result = _policyService.IsBookingAllowed(employeeId, roomType);

            result.ShouldBeTrue();
        }

        [Test]
        public void not_allow_booking_if_company_has_a_different_policy()
        {
            _policyRepository.EmployeePolicyExists(employeeId).Returns(true);
            _policyRepository.IsEmployeePolicyDefault(employeeId).Returns(true);
            _companyRepository.GetCompanyIdByEmployeeId(employeeId).Returns(companyId);
            _policyRepository.CheckCompanyPolicy(companyId, roomType).Returns(false);

            var result = _policyService.IsBookingAllowed(employeeId, roomType);

            result.ShouldBeFalse();
        }
    }
}

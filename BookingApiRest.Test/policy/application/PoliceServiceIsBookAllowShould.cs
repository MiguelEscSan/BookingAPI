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
using BookingApiRest.core.BookingApp.policy.application.requests;
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

            _policyService = new PolicyService(_policyRepository, _eventBus);

            companyId = Guid.NewGuid().ToString();
            employeeId = Guid.NewGuid().ToString();
            roomType = RoomType.Standard;
        }

        [Test]
        public async Task check_if_booking_is_allow_for_an_employee()
        {
            _policyRepository.EmployeePolicyExists(employeeId).Returns(true);
            _policyRepository.IsEmployeePolicyDefault(employeeId).Returns(false);
            _policyRepository.CheckEmployeePolicy(employeeId, roomType).Returns(true);

            var result = await _policyService.IsBookingAllowed(employeeId, roomType);

            result.GetValue().IsSuccess.ShouldBeTrue();
        }

        [Test]
        public async Task not_allow_booking_of_room_not_allowed_for_employee()
        {
            _policyRepository.EmployeePolicyExists(employeeId).Returns(true);
            _policyRepository.IsEmployeePolicyDefault(employeeId).Returns(false);
            _policyRepository.CheckEmployeePolicy(employeeId, roomType).Returns(false);

            var result = await _policyService.IsBookingAllowed(employeeId, roomType);

            result.GetValue().IsSuccess.ShouldBeFalse();
        }

        [Test]
        public async Task not_allow_booking_for_employee_that_does_not_exist()
        {
            _policyRepository.EmployeePolicyExists(employeeId).Returns(false);

            var result = await _policyService.IsBookingAllowed(employeeId, roomType);

            result.IsSuccess.ShouldBeFalse();
            result.GetError().ShouldBeOfType<EmployeeNotFoundException>();
        }


        [Test]
        public async Task check_the_company_policy()
        {
            _policyRepository.EmployeePolicyExists(employeeId).Returns(true);
            _policyRepository.IsEmployeePolicyDefault(employeeId).Returns(true);
            _eventBus.PublishAndWait<GetCompanyIdByEmployeeIdRequest, string>(Arg.Any<GetCompanyIdByEmployeeIdRequest>())
                .Returns(Task.FromResult(new Result<string>(companyId, true)));
            _policyRepository.CheckCompanyPolicy(companyId, roomType).Returns(true);

            var result = await _policyService.IsBookingAllowed(employeeId, roomType);

            result.GetValue().IsSuccess.ShouldBeTrue();
        }


        [Test]
        public async Task not_allow_booking_if_company_has_a_different_policy()
        {
            _policyRepository.EmployeePolicyExists(employeeId).Returns(true);
            _policyRepository.IsEmployeePolicyDefault(employeeId).Returns(true);
            _eventBus.PublishAndWait<GetCompanyIdByEmployeeIdRequest, string>(Arg.Any<GetCompanyIdByEmployeeIdRequest>())
                .Returns(Task.FromResult(new Result<string>(companyId, true)));
            _policyRepository.CheckCompanyPolicy(companyId, roomType).Returns(false);

            var result = await _policyService.IsBookingAllowed(employeeId, roomType);

            result.GetValue().IsSuccess.ShouldBeFalse();
        }
    }
}

using BookingApiRest.core.BookingApp.policy.application;
using BookingApiRest.core.BookingApp.policy.controller.DTO.request;
using BookingApiRest.core.shared.exceptions;
using BookingApiRest.Core.Shared.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BookingApiRest.core.BookingApp.policy.controller;

[ApiController]
[Route("api/policy")]
public class PolicyController : ControllerBase
{
    private readonly PolicyService _policyService;

    public PolicyController(PolicyService policyService)
    {
        _policyService = policyService;
    }

    [HttpPut("company/{companyId}")]
    public IActionResult SetCompanyPolicy(string companyId, [FromBody] CreatePolicyDTO request)
    {
        RoomType roomType = Enum.Parse<RoomType>(request.RoomType);
        _policyService.SetCompanyPolicy(companyId, roomType);
        return Ok();
    }

    [HttpPut("employee/{employeeId}")]
    public IActionResult SetEmployeePolicy(string employeeId, [FromBody] CreatePolicyDTO request)
    {
        try
        {
            RoomType roomType = Enum.Parse<RoomType>(request.RoomType);
            _policyService.SetEmployeePolicy(employeeId, roomType);
            return Ok();
        }
        catch (EmployeeNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("booking/{createdEmployeeId}/{roomType}")]
    public IActionResult IsBookingAllow(string createdEmployeeId, string roomTypeRequest)
    {
        RoomType RoomType = Enum.Parse<RoomType>(roomTypeRequest);
        var isAllow = _policyService.IsBookingAllowed(createdEmployeeId, RoomType);
        return Ok(isAllow);
    }
}


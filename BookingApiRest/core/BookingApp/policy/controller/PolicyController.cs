﻿using BookingApiRest.core.BookingApp.policy.application;
using BookingApiRest.core.BookingApp.policy.controller.DTO.request;
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
}


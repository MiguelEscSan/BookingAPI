using BookingApiRest.core.BookingApp.company.application;
using BookingApiRest.core.BookingApp.company.application.ports;
using BookingApiRest.core.BookingApp.company.controller.DTO.request;
using Microsoft.AspNetCore.Mvc;

namespace BookingApiRest.core.BookingApp.company.controller;

[ApiController]
[Route("api/company")]
public class CompanyController: ControllerBase {

    private readonly EmployeeRepository _employeeRepository;
    private readonly CompanyService _companyService;

    public CompanyController(CompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpPost("employee")]
    public IActionResult CreateEmployee([FromBody] CreateEmployeeDTO request)
    {
        _companyService.AddEmployee(request.employeeId, request.companyId);
        return Ok();
    }

}

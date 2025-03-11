using BookingApiRest.core.BookingApp.company.application;
using BookingApiRest.core.BookingApp.company.application.ports;
using BookingApiRest.core.BookingApp.company.controller.DTO.request;
using BookingApiRest.core.shared.exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BookingApiRest.core.BookingApp.company.controller;

[ApiController]
[Route("api/company")]
public class CompanyController: ControllerBase {

    private readonly CompanyService _companyService;

    public CompanyController(CompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpPost("employee")]
    public IActionResult CreateEmployee([FromBody] CreateEmployeeDTO request)
    {
        try
        {
            _companyService.AddEmployee(request.companyId, request.employeeId);
            return Ok();
        }
        catch (EmployeeAlreadyExistsException e)
        {
            return Conflict(e.Message);
        }
    }

    [HttpDelete("employee/{employeeId}")]
    public IActionResult DeleteEmployee(string employeeId)
    {
        try
        {
            _companyService.DeleteEmployee(employeeId);
            return Ok();
        }
        catch (EmployeeNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

}

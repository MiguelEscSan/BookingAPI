﻿using BookingApiRest.core.BookingApp.company.application.ports;
using System.ComponentModel.Design;
using BookingApiRest.core.BookingApp.company.domain;
using BookingApiRest.core.shared.exceptions;
using BookingApiRest.core.shared.application;

namespace BookingApiRest.core.BookingApp.company.application;
public class CompanyService {

    private readonly CompanyRepository _employeeRepository;
    private readonly EventBus _eventBus;

    public CompanyService(CompanyRepository employeeRepository, EventBus eventBus)
    {
        _employeeRepository = employeeRepository;
        this._eventBus = eventBus;
    }   

    public void AddEmployee(string companyId, string employeeId)
    {
        if(_employeeRepository.Exists(employeeId))
        {
            throw new EmployeeAlreadyExistsException($"Employee with id {employeeId} already exists");
        }

        var Employee = new Employee(employeeId);
        _employeeRepository.Save(companyId, Employee);
    }

    public void DeleteEmployee(string employeeId)
    {
        if (_employeeRepository.Exists(employeeId) is false)
        {
            throw new EmployeeNotFoundException($"Employee with id {employeeId} not found");
        }

        _employeeRepository.Delete(employeeId);
    }
}


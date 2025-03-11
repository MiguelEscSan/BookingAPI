﻿using BookingApiRest.core.BookingApp.company.domain;

namespace BookingApiRest.core.BookingApp.company.application.ports;
public interface EmployeeRepository
{
    void Save(Employee employee);
    bool Exists(string id);
    void Delete(string id);
}


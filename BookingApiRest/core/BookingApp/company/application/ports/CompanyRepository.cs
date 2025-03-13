using BookingApiRest.core.BookingApp.company.domain;

namespace BookingApiRest.core.BookingApp.company.application.ports;
public interface CompanyRepository
{
    void Save(string companyId, Employee employee); 
    bool Exists(string id);
    void Delete(string id);
    Employee GetById(string id);
    string GetCompanyIdByEmployeeId(string employeeId);
}


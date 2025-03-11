using BookingApiRest.core.BookingApp.policy.domain;

namespace BookingApiRest.core.BookingApp.policy.application.DTO;
public interface PolicyRepository {
    void Save(string id, Policy policy);
    
}

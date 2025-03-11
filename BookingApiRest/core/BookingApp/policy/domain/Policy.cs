using BookingApiRest.Core.Shared.Domain;

namespace BookingApiRest.core.BookingApp.policy.domain;
public  class Policy
{
    public string Id { get; set; }
    public RoomType RoomType { get; set; }
    public PolicyType PolicyType { get; set; }

    public Policy(string id, RoomType roomType, PolicyType policyType)
    {
        Id = id;
        RoomType = roomType;
        PolicyType = policyType;
    }
}

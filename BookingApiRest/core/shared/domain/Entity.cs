using System;
namespace BookingApiRest.core.shared.domain;


public class Entity
{
    private string _id;

    public Entity(string id)
    {
        this._id = id;
    }

    public string GetId()
    {
        return _id;
    }

    public void SetId(string id)
    {
        _id = id;
    }
}


using System;
using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Hairdresser: Entity
{
    public string Name { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }

    public Hairdresser()
    {

    }

    public Hairdresser(string name, string latitude, string longitude): this()
    {
        Name = name;
        Latitude = latitude;
        Longitude = longitude;
    }
}


using System;
using System.ComponentModel.DataAnnotations;

namespace Spaceship.Infrastructure.Persistence.Entities;

public class WeatherEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Data { get; set; }
    public long Unix { get; set; }
}
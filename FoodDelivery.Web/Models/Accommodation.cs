using System;
using System.Collections.Generic;

namespace FoodDelivery.Web.Models;

public partial class Accommodation
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public int? AccommodationType { get; set; }

    public decimal PricePerNight { get; set; }

    public int MaxNumberOfRooms { get; set; }

    public virtual ICollection<TravelPackage> TravelPackages { get; set; } = new List<TravelPackage>();
}

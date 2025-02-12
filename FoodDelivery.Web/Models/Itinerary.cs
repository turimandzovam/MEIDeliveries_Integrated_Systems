using System;
using System.Collections.Generic;

namespace FoodDelivery.Web.Models;

public partial class Itinerary
{
    public Guid Id { get; set; }

    public string? Description { get; set; }

    public int DayNumber { get; set; }

    public Guid TravelPackageId { get; set; }

    public virtual TravelPackage TravelPackage { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace FoodDelivery.Web.Models;

public partial class Booking
{
    public Guid Id { get; set; }

    public Guid TravelPackageId { get; set; }

    public string UserId { get; set; } = null!;

    public int NumberOfRooms { get; set; }

    public decimal FullPrice { get; set; }

    public virtual TravelPackage TravelPackage { get; set; } = null!;

    public virtual AspNetUser User { get; set; } = null!;
}

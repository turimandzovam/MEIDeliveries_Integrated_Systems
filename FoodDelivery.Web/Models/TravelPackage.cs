using System;
using System.Collections.Generic;

namespace FoodDelivery.Web.Models;

public partial class TravelPackage
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int NumberOfNights { get; set; }

    public DateTime DepartureDate { get; set; }

    public Guid? AccommodationId { get; set; }

    public int? AvailableRooms { get; set; }

    public string? Image { get; set; }

    public virtual Accommodation? Accommodation { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Itinerary> Itineraries { get; set; } = new List<Itinerary>();
}

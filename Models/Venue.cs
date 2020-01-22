using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace bands_in_town_api.Models
{
  public class Venue
  {
    public int Id { get; set; }
    public int EventId { get; set; }
    [JsonIgnore]
    public Event Event { get; set; }
    public string VenueName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
  }
}
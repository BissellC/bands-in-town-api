using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace bands_in_town_api.Models
{
  public class Event
  {
    public int Id { get; set; }
    public int ArtistId { get; set; }
    [JsonIgnore]
    public Artist Artist { get; set; }
    public string Year { get; set; }
    public string Month { get; set; }
    public string Day { get; set; }
    public string DayOfWeek { get; set; }
    public string Time { get; set; }
    public Venue Venue { get; set; }
  }
}
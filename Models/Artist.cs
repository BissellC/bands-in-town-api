using System;
using System.Collections.Generic;

namespace bands_in_town_api.Models
{
  public class Artist
  {
    public int Id { get; set; }
    public string ArtistName { get; set; }
    public string Genres { get; set; }
    public string MainGenre { get; set; }
    public string Hometown { get; set; }
    public string Website { get; set; }
    public string ArtistPic { get; set; }
    public int Followers { get; set; }
    public List<Event> Events { get; set; } = new List<Event>();
  }
}
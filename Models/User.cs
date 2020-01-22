using System.Collections.Generic;

namespace bands_in_town_api.Models
{
  public class User
  {
    public int Id { get; set; }
    public string Username { get; set; }
    public string HashedPassword { get; set; }
    public string State { get; set; }
    public List<TrackArtist> TrackedArtists { get; set; } = new List<TrackArtist>();

  }
}
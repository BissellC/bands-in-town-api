using System.Collections.Generic;

namespace bands_in_town_api.Models
{
  public class TrackArtist
  {
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int ArtistId { get; set; }
    public Artist Artist { get; set; }

  }
}
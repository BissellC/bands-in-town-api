using System;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using bands_in_town_api.Models;

namespace bands_in_town_api.Models
{
  public partial class DatabaseContext : DbContext
  {

    public DbSet<Artist> Artists { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Venue> Venues { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<TrackArtist> TrackArtists { get; set; }
    private string ConvertPostConnectionToConnectionString(string connection)
    {
      var _connection = connection.Replace("postgres://", String.Empty);
      var output = Regex.Split(_connection, ":|@|/");
      return $"server={output[2]};database={output[4]};User Id={output[0]}; password={output[1]}; port={output[3]}";
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        var envConn = Environment.GetEnvironmentVariable("DATABASE_URL");

        var conn = "server=localhost;database=bands_in_townDatabase";
        if (envConn != null)
        {
          conn = ConvertPostConnectionToConnectionString(envConn);
        }
        optionsBuilder.UseNpgsql(conn);
      }
    }

    public DbSet<bands_in_town_api.Models.User> User { get; set; }

    public DbSet<bands_in_town_api.Models.TrackArtist> TrackArtist { get; set; }



  }
}

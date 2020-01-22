using System;

namespace bands_in_town_api.ViewModels
{
  public class AuthenticatedData
  {
    public string Username { get; set; }
    public int UserId { get; set; }
    public string State { get; set; }
    public string Token { get; set; }
    public DateTime ExpirationTime { get; set; }

  }
}
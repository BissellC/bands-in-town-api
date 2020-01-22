using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bands_in_town_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace bands_in_town_api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SearchController : ControllerBase
  {
    private readonly DatabaseContext _context;

    public SearchController(DatabaseContext context)
    {
      this._context = context;
    }

    [HttpGet]
    public async Task<ActionResult> SearchArtists([FromQuery]string searchTerm)
    {
      var results = _context.Artists.Where(artist => artist.ArtistName.ToLower().Contains(searchTerm.ToLower()));
      var query = new Search
      {
        SearchTerm = searchTerm
      };
      return Ok(results);
    }
  }
}
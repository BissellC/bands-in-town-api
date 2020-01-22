using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bands_in_town_api.Models;

namespace bands_in_town_api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TrackArtistController : ControllerBase
  {
    private readonly DatabaseContext _context;

    public TrackArtistController(DatabaseContext context)
    {
      _context = context;
    }

    // GET: api/TrackArtist
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TrackArtist>>> GetTrackArtist()
    {
      return await _context.TrackArtist.ToListAsync();
    }

    // GET: api/TrackArtist/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TrackArtist>> GetTrackArtist(int id)
    {
      var trackArtist = await _context.TrackArtist.FindAsync(id);

      if (trackArtist == null)
      {
        return NotFound();
      }

      return trackArtist;
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<TrackArtist>> GetArtistsForUser(int userId)
    {
      var trackArtist = await _context.TrackArtist.Where(i => i.UserId == userId).Include(i => i.Artist).ThenInclude(i => i.Events).ThenInclude(i => i.Venue).ToListAsync();

      if (trackArtist == null)
      {
        return NotFound();
      }

      return Ok(trackArtist);
    }

    // PUT: api/TrackArtist/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTrackArtist(int id, TrackArtist trackArtist)
    {
      if (id != trackArtist.Id)
      {
        return BadRequest();
      }

      _context.Entry(trackArtist).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!TrackArtistExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/TrackArtist
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPost]
    public async Task<ActionResult<TrackArtist>> PostTrackArtist(TrackArtist trackArtist)
    {
      var duplicateCheck = _context.TrackArtist.Any(t => t.UserId == trackArtist.UserId && t.ArtistId == trackArtist.ArtistId);

      if (duplicateCheck)
      {
        return NotFound();
      }
      else
      {
        _context.TrackArtist.Add(trackArtist);
        await _context.SaveChangesAsync();
      }
      return CreatedAtAction("GetTrackArtist", new { id = trackArtist.Id }, trackArtist);
    }

    // DELETE: api/TrackArtist/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<TrackArtist>> DeleteTrackArtist(int id)
    {
      var trackArtist = await _context.TrackArtist.FindAsync(id);
      if (trackArtist == null)
      {
        return NotFound();
      }

      _context.TrackArtist.Remove(trackArtist);
      await _context.SaveChangesAsync();

      return trackArtist;
    }

    private bool TrackArtistExists(int id)
    {
      return _context.TrackArtist.Any(e => e.Id == id);
    }
  }
}

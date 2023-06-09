﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using solita_assignment.Classes;
using solita_assignment.Models;

namespace solita_assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationsController : ControllerBase
    {
        private readonly JourneyContext _context;

        public StationsController(JourneyContext context)
        {
            _context = context;
        }

        // GET: api/Stations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Station>>> GetStations()
        {
            if (_context.Stations == null)
            {
                return NotFound();
            }
            return await _context.Stations.ToListAsync();
        }

        // GET: api/Stations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Station>> GetStation(Guid id)
        {
            if (_context.Stations == null)
            {
                return NotFound();
            }
            var station = await _context.Stations.FindAsync(id);

            if (station == null)
            {
                return NotFound();
            }

            // Could be possible to make a group by query but didn't have enough time to sort out an answer!
            var departureCount = _context.Journeys
                .Where(j => station.IdInt == j.DepartureStationId).Count();

            var returnCount = _context.Journeys
                .Where(j => station.IdInt == j.ReturnStationId).Count();

            return Ok(new StationCountResponse
            {
                DepartureStationCount = departureCount,
                ReturnStationCount = returnCount,
                Station = station           
            });
        }

        // PUT: api/Stations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStation(Guid id, Station station)
        {
            if (id != station.StationId)
            {
                return BadRequest();
            }

            _context.Entry(station).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StationExists(id))
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

        // POST: api/Stations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Station>> PostStation(StationDto stationDto)
        {
            if (_context.Stations == null)
            {
                return Problem("Entity set 'StationContext.Stations'  is null.");
            }
            var station = new Station
            {
                StationId = Guid.NewGuid(),
                AddressFinnish = stationDto.AddressFinnish,
                AddressSwedish = stationDto.AddressSwedish,
                Capacity = stationDto.Capacity,
                CityFinnish = stationDto.CityFinnish,
                CitySwedish = stationDto.CitySwedish,
                IdInt   = stationDto.IdInt,
                LocationX = stationDto.LocationX,
                LocationY = stationDto.LocationY,
                NameEnglish = stationDto.NameEnglish,
                NameFinnish = stationDto.NameFinnish,
                NameSwedish = stationDto.NameSwedish,
                Operator    = stationDto.Operator
            };
            _context.Stations.Add(station);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStation), new { id = station.StationId }, station);
        }

        // DELETE: api/Stations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStation(Guid id)
        {
            if (_context.Stations == null)
            {
                return NotFound();
            }
            var station = await _context.Stations.FindAsync(id);
            if (station == null)
            {
                return NotFound();
            }

            _context.Stations.Remove(station);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StationExists(Guid id)
        {
            return (_context.Stations?.Any(e => e.StationId == id)).GetValueOrDefault();
        }
    }
}

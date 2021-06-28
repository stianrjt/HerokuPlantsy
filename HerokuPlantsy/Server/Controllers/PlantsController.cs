using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Plantsy.Server.Data;
using Plantsy.Server.Models;
using HerokuPlantsy.Shared;

namespace Plantsy.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlantsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Plants
        [HttpGet]
        public async Task<IActionResult> GetPlants()
        {
            var result = await _context.Plants
                .Include(x => x.ChangeLog)
                .Include(y => y.Image)
                .Include(z => z.WaterLog)
                .ToListAsync();
            return Ok(result);
        }

        // GET: api/Plants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plant>> GetPlant(Guid id)
        {
            var plant = await _context.Plants
                .Include(x => x.ChangeLog)
                .Include(x => x.Image)
                .Include(x => x.WaterLog).FirstOrDefaultAsync(x => x.ID == id);

            if (plant == null)
            {
                return NotFound();
            }

            return plant;
        }

        // PUT: api/Plants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlant(Guid id, Plant plant)
        {
            if (id != plant.ID)
            {
                return BadRequest();
            }

            var plantToUpdate = await _context.Plants.FirstOrDefaultAsync(x => x.ID == id);
            plantToUpdate.PlantName = plant.PlantName;
            plantToUpdate.PlantType = plant.PlantType;
            plantToUpdate.Info = plant.Info;
            plantToUpdate.LastWatered = plant.LastWatered;
            plantToUpdate.WaterLog = plant.WaterLog;
            plantToUpdate.Image = plant.Image;
         
            _context.Entry(plantToUpdate).State = EntityState.Modified;
            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantExists(id))
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

        // POST: api/Plants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Plant>> PostPlant(Plant plant)
        {
            _context.Plants.Add(plant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlant", new { id = plant.ID }, plant);
        }

        // DELETE: api/Plants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlant(Guid id)
        {
            var plant = await _context.Plants.FindAsync(id);
            if (plant == null)
            {
                return NotFound();
            }

            _context.Plants.Remove(plant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlantExists(Guid id)
        {
            return _context.Plants.Any(e => e.ID == id);
        }

    }
}

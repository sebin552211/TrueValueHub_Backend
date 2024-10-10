using Microsoft.AspNetCore.Mvc;
using TrueValueHub.Data;
using TrueValueHub.Interfaces;
using TrueValueHub.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;


namespace TrueValueHub.Repositories
{
    public class PartRepository : IPartRepository
    {
       
            private readonly ApiDbContext _context;
        private readonly IMapper _mapper;

        public PartRepository(ApiDbContext context, IMapper mapper)
            {
                _context = context;
            _mapper = mapper;
        }


            public async Task<IEnumerable<Part>> GetAllParts()
            {
                return await _context.Parts.ToListAsync();
            }


            public async Task<Part> GetPartByInternalPartNo(string id)
            {
            return await _context.Parts.Include(p => p.Manufacturings).FirstOrDefaultAsync(p => p.InternalPartNumber == id);
            }


            public async Task AddPart(Part part)
            {
                await _context.Parts.AddAsync(part);
                await _context.SaveChangesAsync();
            }


        public async Task<bool> UpdatePart(Part part, string internalPartNumber)
        {
            if (internalPartNumber != part.InternalPartNumber)
            {
                return false;
            }

            var existingPart = await _context.Parts.FirstOrDefaultAsync(p => p.InternalPartNumber == internalPartNumber);
            if (existingPart == null)
            {
                Console.WriteLine("No part found with the given internalPartNumber.");
                return false;
            }

            // Use AutoMapper to map properties from part to existingPart
            _mapper.Map(part, existingPart);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error updating database: {ex.Message}");
                return false;
            }
        }


        public async Task<Part> GetPartById(int partId)
        {
            return await _context.Parts.FirstOrDefaultAsync(p => p.PartId == partId);
        }


        public async Task<int> AddManufacturingToPart(int partId, Manufacturing newManufacturing)
        {
            var part = await GetPartById(partId); // Ensure this is an asynchronous call
            if (part == null)
            {
                return -1; // Indicate that the part was not found
            }

            try
            {
                newManufacturing.PartId = partId; // Ensure PartId is set before adding
                await _context.Manufacturings.AddAsync(newManufacturing);
                await _context.SaveChangesAsync();

                // Return the newly created manufacturingId
                return newManufacturing.ManufacturingId; // Ensure ManufacturingId is set after saving
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error updating database: {ex.Message}");
                return -1; // Indicate an error occurred
            }
        }


        public async Task<IEnumerable<Manufacturing>> GetManufacturingsByPartId(int partId)
        {
            // Fetch the part including its manufacturings
            var part = await _context.Parts.Include(p => p.Manufacturings)
                                             .FirstOrDefaultAsync(p => p.PartId == partId);
            return part?.Manufacturings ?? Enumerable.Empty<Manufacturing>(); // Return an empty list if no manufacturings found
        }


        public async Task<bool> UpdateManufacturing(int partId, int manufacturingId, Manufacturing updatedManufacturing)
        {
            // Fetch the part along with its manufacturings
            var part = await _context.Parts.Include(p => p.Manufacturings)
                                             .FirstOrDefaultAsync(p => p.PartId == partId);

            if (part == null)
            {
                return false; // Part not found
            }

            // Find the manufacturing entry that needs to be updated
            var manufacturing = part.Manufacturings.FirstOrDefault(m => m.ManufacturingId == manufacturingId);
            if (manufacturing == null)
            {
                return false; // Manufacturing not found
            }

            // Update manufacturing properties using AutoMapper
            // Ensure the mapping is set up to ignore key properties if needed
            _mapper.Map(updatedManufacturing, manufacturing);

            // Attempt to save changes
            try
            {
                // Check the state of the manufacturing entity
                _context.Entry(manufacturing).State = EntityState.Modified; // Mark as modified
                await _context.SaveChangesAsync();
                return true; // Update successful
            }
            catch (DbUpdateException ex)
            {
                // Log the exception with more details if necessary
                Console.WriteLine($"Error updating database: {ex.Message}");
                // Optionally, log ex.InnerException for more details on what went wrong
                return false; // Indicate failure
            }
        }



        public async Task<bool> DeleteManufacturing(int partId, int manufacturingId)
        {
            var part = await _context.Parts.Include(p => p.Manufacturings)
                                             .FirstOrDefaultAsync(p => p.PartId == partId);

            if (part == null)
            {
                return false; // Part not found
            }

            var manufacturing = part.Manufacturings.FirstOrDefault(m => m.ManufacturingId == manufacturingId);
            if (manufacturing == null)
            {
                return false; // Manufacturing not found
            }

            try
            {
                _context.Manufacturings.Remove(manufacturing);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error updating database: {ex.Message}");
                return false;
            }
        }

    }
}

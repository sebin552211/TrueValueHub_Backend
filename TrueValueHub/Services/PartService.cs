using System.Collections.Generic;
using System.Threading.Tasks;
using TrueValueHub.Interfaces;
using TrueValueHub.Models;

namespace TrueValueHub.Services
{
    public class PartService : IPartService
    {
        private readonly IPartRepository _partRepository;

        public PartService(IPartRepository partRepository)
        {
            _partRepository = partRepository;
        }

        public async Task<IEnumerable<Part>> GetAllParts()
        {
            return await _partRepository.GetAllParts();
        }

        public async Task<Part> GetPartByInternalPartNo(string id)
        {
            return await _partRepository.GetPartByInternalPartNo(id);
        }

        public async Task AddPart(Part part)
        {
            await _partRepository.AddPart(part);
        }

        public async Task<bool> UpdatePart(Part part, string internalPartNumber)
        {
            return await _partRepository.UpdatePart(part, internalPartNumber);
        }

        public async Task<int> AddManufacturingToPart(int partId, Manufacturing newManufacturing)
        {
            return await _partRepository.AddManufacturingToPart(partId, newManufacturing);
        }

        public async Task<IEnumerable<Manufacturing>> GetManufacturingsByPartId(int partId)
        {
            return await _partRepository.GetManufacturingsByPartId(partId);
        }

        public async Task<bool> UpdateManufacturing(int partId, int manufacturingId, Manufacturing updatedManufacturing)
        {
            return await _partRepository.UpdateManufacturing(partId, manufacturingId, updatedManufacturing);
        }

        public async Task<bool> DeleteManufacturing(int partId, int manufacturingId)
        {
            return await _partRepository.DeleteManufacturing(partId, manufacturingId);
        }
    }
}

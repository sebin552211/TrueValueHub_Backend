using System.Collections.Generic;
using System.Threading.Tasks;
using TrueValueHub.Models;

namespace TrueValueHub.Interfaces
{
    public interface IPartService
    {
        Task<IEnumerable<Part>> GetAllParts();
        Task<Part> GetPartByInternalPartNo(string id);
        Task AddPart(Part part);
        Task<bool> UpdatePart(Part part, string internalPartNumber);
        Task<int> AddManufacturingToPart(int partId, Manufacturing newManufacturing);
        Task<IEnumerable<Manufacturing>> GetManufacturingsByPartId(int partId);
        Task<bool> UpdateManufacturing(int partId, int manufacturingId, Manufacturing updatedManufacturing);
        Task<bool> DeleteManufacturing(int partId, int manufacturingId);
    }
}

using TrueValueHub.Models;

namespace TrueValueHub.Interfaces
{
    public interface IPartRepository
    {
        Task<IEnumerable<Part>> GetAllParts();
        Task<Part> GetPartByInternalPartNo(string id);
        Task AddPart(Part part);
        Task<bool> UpdatePart(Part part,string internalPartNumber);
        Task<bool> AddManufacturingToPart(int partId, Manufacturing newManufacturing);

        Task<IEnumerable<Manufacturing>> GetManufacturingsByPartId(int partId); // New method for fetching manufacturings
        Task<bool> UpdateManufacturing(int partId, int manufacturingId, Manufacturing updatedManufacturing);
        Task<bool> DeleteManufacturing(int partId, int manufacturingId);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using TrueValueHub.Models;

namespace TrueValueHub.Interfaces
{
    public interface IProjectRepository
    {
        Task AddProjectAsync(Project project);
        Task<IEnumerable<Project>> GetProjectsAsync(int skip, int take, string sortField, string sortOrder);
        Task<int> GetTotalCountAsync();
        Task<Project> GetProjectByIdAsync(int projectId);
        Task<Project> UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(int projectId);
        Task<IEnumerable<Project>> SearchProjectsByNameAsync(string projectName);
    }
}

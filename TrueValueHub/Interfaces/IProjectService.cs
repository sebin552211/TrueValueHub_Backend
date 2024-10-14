using TrueValueHub.Models;

namespace TrueValueHub.Interfaces
{
    public interface IProjectService
    {
        Task<Project> CreateProjectAsync(ProjectDto projectDto);
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<Project> GetProjectByIdAsync(int projectId);
        Task<Project> UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(int projectId);
    }
}

using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueValueHub.Interfaces;
using TrueValueHub.Models;

namespace TrueValueHub.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IMapper mapper, IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<Project> CreateProjectAsync(ProjectDto projectDto)
        {
            // Map ProjectDto to Project using AutoMapper
            var project = _mapper.Map<Project>(projectDto);

            // If Parts are included, map them as well
            if (projectDto.Parts != null)
            {
                project.Parts = _mapper.Map<List<Part>>(projectDto.Parts);
            }

            await _projectRepository.AddProjectAsync(project);
            return project;
        }


        public async Task<IEnumerable<Project>> GetProjectsAsync(int skip, int take, string sortField, string sortOrder)
        {
            return await _projectRepository.GetProjectsAsync(skip, take, sortField, sortOrder);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _projectRepository.GetTotalCountAsync();
        }

        public async Task<Project> GetProjectByIdAsync(int projectId)
        {
            return await _projectRepository.GetProjectByIdAsync(projectId);
        }

        public async Task<Project> UpdateProjectAsync(Project project)
        {
            return await _projectRepository.UpdateProjectAsync(project);
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            await _projectRepository.DeleteProjectAsync(projectId);
        }

        public async Task<IEnumerable<Project>> SearchProjectsByNameAsync(string projectName)
        {
            return await _projectRepository.SearchProjectsByNameAsync(projectName);
        }
    }
}

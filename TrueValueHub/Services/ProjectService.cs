using System.Collections.Generic;
using System.Threading.Tasks;
using TrueValueHub.Interfaces;
using TrueValueHub.Models;
using TrueValueHub.Repositories;

namespace TrueValueHub.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Project> CreateProjectAsync(ProjectDto projectDto)
        {
            var project = new Project
            {
                ProjectName = projectDto.ProjectName,
                ProjectDescription = projectDto.Description,
                ProjectCreatedDate = projectDto.CreatedDate,
                Parts = projectDto.Parts?.Select(partDto => new Part
                {
                    InternalPartNumber = partDto.InternalPartNumber,
                    SupplierName = partDto.SupplierName,
                    DeliverySiteName = partDto.DeliverySiteName,
                    DrawingNumber = partDto.DrawingNumber,
                    IncoTerms = partDto.IncoTerms,
                    AnnualVolume = partDto.AnnualVolume,
                    BomQty = partDto.BomQty,
                    DeliveryFrequency = partDto.DeliveryFrequency,
                    LotSize = partDto.LotSize,
                    ManufacturingCategory = partDto.ManufacturingCategory,
                    PackagingType = partDto.PackagingType,
                    ProductLifeRemaining = partDto.ProductLifeRemaining,
                    PaymentTerms = partDto.PaymentTerms,
                    LifetimeQuantityRemaining = partDto.LifetimeQuantityRemaining
                }).ToList()
            };

            await _projectRepository.AddProjectAsync(project);

            return project;
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _projectRepository.GetProjectsAsync();
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
    }
}

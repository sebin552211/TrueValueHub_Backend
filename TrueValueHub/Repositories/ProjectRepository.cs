using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueValueHub.Data;
using TrueValueHub.Interfaces;
using TrueValueHub.Models;

namespace TrueValueHub.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApiDbContext _context;

        public ProjectRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task AddProjectAsync(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync(int skip, int take, string sortField, string sortOrder)
        {
            IQueryable<Project> query = _context.Projects.Include(p => p.Parts);

            // Sorting logic
            if (!string.IsNullOrEmpty(sortField))
            {
                switch (sortField.ToLower())
                {
                    case "projectid":
                        query = sortOrder?.ToLower() == "asc" ? query.OrderBy(p => p.ProjectId) : query.OrderByDescending(p => p.ProjectId);
                        break;
                    case "projectname":
                        query = sortOrder?.ToLower() == "asc" ? query.OrderBy(p => p.ProjectName) : query.OrderByDescending(p => p.ProjectName);
                        break;
                    case "projectdescription":
                        query = sortOrder?.ToLower() == "asc" ? query.OrderBy(p => p.ProjectDescription) : query.OrderByDescending(p => p.ProjectDescription);
                        break;
                    case "projectcreateddate":
                        query = sortOrder?.ToLower() == "asc" ? query.OrderBy(p => p.ProjectCreatedDate) : query.OrderByDescending(p => p.ProjectCreatedDate);
                        break;
                    default:
                        break;
                }
            }

            return await query.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Projects.CountAsync();
        }

        public async Task<Project> GetProjectByIdAsync(int projectId)
        {
            return await _context.Projects.Include(p => p.Parts)
                                          .FirstOrDefaultAsync(p => p.ProjectId == projectId);
        }

        public async Task<Project> UpdateProjectAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            var project = await _context.Projects.FindAsync(projectId);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Project>> SearchProjectsByNameAsync(string projectName)
        {
            return await _context.Projects
                                 .Where(p => p.ProjectName.Contains(projectName))
                                 .Include(p => p.Parts)
                                 .ToListAsync();
        }
    }
}

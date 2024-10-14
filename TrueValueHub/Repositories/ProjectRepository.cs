using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            return await _context.Projects
                .Include(p => p.Parts)
                .ToListAsync();
        }

        public async Task<Project> GetProjectByIdAsync(int projectId)
        {
            return await _context.Projects.Include(p => p.Parts).FirstOrDefaultAsync(p => p.ProjectId == projectId);
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
    }
}

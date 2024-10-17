using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueValueHub.Interfaces;
using TrueValueHub.Models;

namespace TrueValueHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // POST: api/Project
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectDto projectDto)
        {
            if (projectDto == null)
            {
                return BadRequest("Project data is null.");
            }

            try
            {
                var result = await _projectService.CreateProjectAsync(projectDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Project
        [HttpGet]
        public async Task<IActionResult> GetProjects([FromQuery] int skip = 0, [FromQuery] int take = 10, [FromQuery] string sortField = null, [FromQuery] string sortOrder = null)
        {
            try
            {
                var projects = await _projectService.GetProjectsAsync(skip, take, sortField, sortOrder);
                var totalCount = await _projectService.GetTotalCountAsync();

                return Ok(new
                {
                    values = projects,
                    totalCount = totalCount
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Project/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectById(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        // PUT: api/Project/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] Project project)
        {
            if (id != project.ProjectId)
            {
                return BadRequest();
            }

            var updatedProject = await _projectService.UpdateProjectAsync(project);
            return Ok(updatedProject);
        }

        // DELETE: api/Project/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            await _projectService.DeleteProjectAsync(id);
            return NoContent();
        }

        // GET: api/Project/search?name={projectName}
        [HttpGet("search")]
        public async Task<IActionResult> SearchProjectsByName([FromQuery] string name)
        {
            try
            {
                var projects = await _projectService.SearchProjectsByNameAsync(name);
                return Ok(projects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

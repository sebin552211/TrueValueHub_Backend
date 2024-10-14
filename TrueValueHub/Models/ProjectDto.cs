namespace TrueValueHub.Models
{
    public class ProjectDto
    {
        public string ProjectName { get; set; } // Name of the project
        public string Description { get; set; } // Description of the project
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; // Automatically set created date
        public List<PartDto> Parts { get; set; } // List of parts associated with this project
    }

}

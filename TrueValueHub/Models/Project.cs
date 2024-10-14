using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrueValueHub.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [StringLength(100)]
        public string ProjectName { get; set; }

        [StringLength(500)]
        public string ProjectDescription { get; set; }

        public DateTime ProjectCreatedDate { get; set; } = DateTime.Now;

        // One-to-Many relationship with Part
        public virtual ICollection<Part> Parts { get; set; } = new List<Part>();
    }
}

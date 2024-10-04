using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TrueValueHub.Models
{
    public class Manufacturing
    {
        [Key]
        public int ManufacturingId { get; set; }


        [StringLength(100)]
        public string ProcessType { get; set; }


        [StringLength(100)]
        public string SubProcessType { get; set; }


        [StringLength(100)]
        public string MachineDetails { get; set; }


        [StringLength(100)]
        public string MachineDescription { get; set; }


        public int Cost { get; set; }


        [StringLength(100)]
        public string MachineName { get; set; }


        [StringLength(100)]
        public string MCAutomation { get; set; }

 
        [StringLength(100)]
        public string MachineEfficiency { get; set; }


        public int ToolingCost { get; set; }


        public int LoadingTime { get; set; }


        [ForeignKey("Part")]
        public int PartId { get; set; }

        public virtual Part Part { get; set; }
    }
}

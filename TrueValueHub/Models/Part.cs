using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrueValueHub.Models
{
    public class Part
    {
        [Key]
        public int PartId { get; set; }

        [StringLength(100)]
        public string InternalPartNumber { get; set; }

        [StringLength(100)]
        public string SupplierName { get; set; }

        [StringLength(100)]
        public string DeliverySiteName { get; set; }

        [StringLength(50)]
        public string DrawingNumber { get; set; }

        [StringLength(10)]
        public string IncoTerms { get; set; }

        public int AnnualVolume { get; set; }
        public int BomQty { get; set; }
        public int DeliveryFrequency { get; set; }
        public int LotSize { get; set; }

        [StringLength(50)]
        public string ManufacturingCategory { get; set; }

        [StringLength(50)]
        public string PackagingType { get; set; }

        public int ProductLifeRemaining { get; set; }

        [StringLength(20)]
        public string PaymentTerms { get; set; }

        public int LifetimeQuantityRemaining { get; set; }

        // One-to-Many Relationship: Part belongs to one Project
        public int ProjectId { get; set; } // Foreign Key to Project
        public virtual Project Project { get; set; }

        public virtual ICollection<Manufacturing> Manufacturings { get; set; } = new List<Manufacturing>();
    }
}

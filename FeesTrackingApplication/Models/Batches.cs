using System.ComponentModel.DataAnnotations;

namespace FeesTrackingApplication.Models
{
    public class Batches
    {
        [Key]
        public int BatchId { get; set; }
        public string BatchName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

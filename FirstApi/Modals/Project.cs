using System.ComponentModel.DataAnnotations.Schema;

namespace FirstApi.Modals
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int OwnerId { get; set; }
        public List<int> AssignedTo { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Budget { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }
        public string Category { get; set; }
        public List<string> Tags { get; set; }
        public int ClientId { get; set; }
        public float ProgressPercentage { get; set; }
        public bool IsArchived { get; set; }
        public List<string> Documents { get; set; }
    }
}

using System.Collections.Generic;

namespace FirstApi.Models
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
        public List<int> AssignedTo { get; set; } = new List<int>();
        public decimal Budget { get; set; }
        public decimal Cost { get; set; }
        public string Category { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public int ClientId { get; set; }
        public float ProgressPercentage { get; set; }
        public bool IsArchived { get; set; }
        public List<string> Documents { get; set; } = new List<string>();
        public List<Issue> Issues { get; set; } = new List<Issue>();
        public List<User> Users { get; set; } = new List<User>();
    }
}

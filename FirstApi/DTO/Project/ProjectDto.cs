    using FirstApi.Models;

    namespace FirstApi.DTO.Project
    {
        public class ProjectDTO
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int StatusId { get; set; }
            public string StatusTitle { get; set; } // Title of the associated Status
            public int PriorityId { get; set; }
            public string PriorityTitle { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime LastUpdatedDate { get; set; }
            public int OwnerId { get; set; }
            public List<int> AssignedTo { get; set; }
            public decimal Budget { get; set; }
            public decimal Cost { get; set; }
            public string Category { get; set; }
            public List<string> Tags { get; set; }
            public int ClientId { get; set; }
            public float ProgressPercentage { get; set; }
            public bool IsArchived { get; set; }
            public List<string> Documents { get; set; }
        }

    }

namespace FirstApi.Modals
{
    public class Issue
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public List<int> AssignedTo { get; set; }
        public int CreatedBy { get; set; }
        public List<string> Tags { get; set; }
        public double EstimatedHours { get; set; }
    }

}

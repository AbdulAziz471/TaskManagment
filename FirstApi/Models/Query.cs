namespace FirstApi.Models
{
    public class Query
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
        public int PriorityId { get; set; }
        public virtual Priority Priority { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public int AssignedUserId { get; set; }  
        public virtual User AssignedUser { get; set; }
    }
}

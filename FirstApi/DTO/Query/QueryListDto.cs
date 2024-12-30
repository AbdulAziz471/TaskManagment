namespace FirstApi.DTO.Query
{
    public class QueryListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StatusName { get; set; }
        public string PriorityName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string CreatedByName { get; set; }
        public string AssignedUserName { get; set; }
        public string ProjectName { get; set; }
    }
}

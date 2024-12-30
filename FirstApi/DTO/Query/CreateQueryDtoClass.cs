using FirstApi.Models;

namespace FirstApi.DTO.Query
{
    public class CreateQueryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public int PriorityId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ProjectId { get; set; }
        public int CreatedById { get; set; } 
        public int AssignedUserId { get; set; }
    }

}

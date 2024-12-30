namespace FirstApi.DTO.Team
{
    public class CreateTeamDto
    {

        public string TeamName { get; set; } // Team's Name
        public string Description { get; set; } // Team's Description
        public List<int> UserIds { get; set; } = new List<int>();
    }
}

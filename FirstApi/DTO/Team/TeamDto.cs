namespace FirstApi.DTO.Team
{
    public class TeamDto
    {
        public string TeamName { get; set; } // Team's Name
        public string Description { get; set; } // Team's Description
        public List<UserTeam> Members { get; set; } = new List<UserTeam>(); // List of team members
    }
}

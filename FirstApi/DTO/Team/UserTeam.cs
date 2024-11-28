namespace FirstApi.DTO.Team
{
    public class UserTeam
    {
        public int UserId { get; set; } // User's ID
        public string Name { get; set; } // User's Name
        public string Email { get; set; } // User's Email
        public List<RoleTeam> Roles { get; set; } = new List<RoleTeam>(); // List of Roles
        public List<ProjectTeam> Projects { get; set; } = new List<ProjectTeam>(); // List of Projects
    }
}

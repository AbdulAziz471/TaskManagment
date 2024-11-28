namespace FirstApi.Modals
{
    public class Team
    {
        internal object Projects;

        public int Id { get; set; } // Primary Key
        public string TeamName { get; set; } // Name of the team
        public string Description { get; set; } // Optional description of the team

        // Navigation property for related Users
        public ICollection<User> Members { get; set; } // One-to-Many with Users
    }
}

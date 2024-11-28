namespace FirstApi.Modals
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<Project> Projects { get; set; } = new List<Project>();
        public ICollection<Role> Roles { get; set; } = new List<Role>();
        public int? TeamId { get; set; }
        public Team Team { get; set; }
    }

}

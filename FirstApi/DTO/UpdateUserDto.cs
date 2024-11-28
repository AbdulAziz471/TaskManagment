namespace FirstApi.DTO
{
    public class UpdateUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; } // Role (Admin or Employee)
    }
}

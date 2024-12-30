using FirstApi.DTO.Project;
using FirstApi.DTO.Team;
using FirstApi.Models;

namespace FirstApi.DTO.Dashboard
{
    public class DashboardDto
    {
        public List<ProjectDTO> Projects { get; set; }
        public List<User> Users { get; set; }
        public List<TeamDto> Teams { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMA_Warehouse.Shared.DTOs
{
    public class UserDTO
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Role Role { get; set; } = Role.Guest;
    }

    public enum Role
    {
        Guest,
        Employee,
        Coordinator,
        Adminitrator
    }
}

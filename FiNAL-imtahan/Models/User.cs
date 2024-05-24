using Microsoft.AspNetCore.Identity;

namespace FiNAL_imtahan.Models
{
    public class User:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}

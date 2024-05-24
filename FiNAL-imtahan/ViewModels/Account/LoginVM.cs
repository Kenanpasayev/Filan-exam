using System.ComponentModel.DataAnnotations;

namespace FiNAL_imtahan.ViewModels.Account
{
    public class LoginVM
    { 
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}


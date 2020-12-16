using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Usuario
    {         
        public string Email { get; set; }
        public string Senha { get; set; }

        [NotMapped]
        public string Token { get; set; }

        [NotMapped]
        public string RefreshToken { get; set; }
    }
}

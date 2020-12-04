using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Usuario
    {
        public string Login { get; set; }
        public string Senha { get; set; }                
        public string Token { get; set; }
    }
}

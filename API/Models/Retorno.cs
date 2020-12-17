using System.Collections.Generic;

namespace API.Models
{
    public class Retorno<T>
    {
        public string mensagem { get; set; }
        public T dados { get; set; }
        public List<string> erros { get; set; }
    }
}

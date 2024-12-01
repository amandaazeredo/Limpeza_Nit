using System.Net.Http.Headers;

namespace API.Domain.Model

{
    public class InfoValidacao
    {
        public string Nome { get; set; } = string.Empty;
        public string Uf { get; set; } = string.Empty;
        public string Ddd { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Base { get; set; } = string.Empty;
        public string Data { get; set; } = string.Empty;
        public string Checa { get; set; } = string.Empty;
    }
}

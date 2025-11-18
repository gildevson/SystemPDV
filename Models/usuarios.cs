using System.Text.Json.Serialization;

namespace FinanblueBackend.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        // Senha enviada pelo cliente (no registro)
        public string? Senha { get; set; }

        // Senha hash armazenada no banco (nunca enviada ao cliente)
        [JsonIgnore]
        public string? SenhaHash { get; set; }
    }
}

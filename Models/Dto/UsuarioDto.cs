using Newtonsoft.Json;

namespace MagicVilla_MVC.Models.Dto
{
    public class UsuarioDto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string Rol { get; set; }
    }
}

namespace MagicVilla_MVC.Models.Dto
{
    public class RegistroRequestDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; } = "user";
    }
}

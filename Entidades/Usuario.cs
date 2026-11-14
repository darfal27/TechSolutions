// Proyecto: Entidades (solo clases de datos, sin lógica)
namespace Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string ClaveHash { get; set; }
        public string Sal { get; set; }
        public string Rol { get; set; }
        public int IntentosFallidos { get; set; }
        public bool Bloqueado { get; set; }
    }
}

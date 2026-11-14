namespace Entidades
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public override string ToString() { return Nombre + " (" + Documento + ")"; }   // lo usa el ComboBox
    }
}

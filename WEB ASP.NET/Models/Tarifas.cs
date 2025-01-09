namespace WEB_ASP.NET.Models
{
    public class Tarifas
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ICollection<string> Atributos { get; set; } = new List<string>();
        public decimal Precio { get; set; }
    }
}

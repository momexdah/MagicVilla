using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.DTO
{
    public class VillaDTO
    {
        public int id { get; set; }
        
        [Required]
        [MaxLength(30)]
        public string nombre { get; set; }
        public string detalle { get; set; }
        [Required]
        public double tarifa { get; set; }
        public int ocupantes { get; set; }
        public int metros_cuadrados { get; set; }
        public string imagen_url { get; set; }
        public string amenidad { get; set; }
    }
}

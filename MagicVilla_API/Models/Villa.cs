using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_API.Models
{
    public class Villa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string nombre { get; set; }
        public string detalle { get; set; }
        [Required]
        public double Tarifa { get; set; }
        public int ocupantes { get; set; }
        public int metros_cuadrados { get; set; }
        public string imagen_url { get; set; }
        public string amenidad { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_actualizacion { get; set; }
    }
}

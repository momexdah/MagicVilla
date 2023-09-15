using MagicVilla_API.DTO;

namespace MagicVilla_API.Datos
{
    public static class VillaStore
    {
        public static List<VillaDTO> VillaList { get; set; } = new List<VillaDTO>
        {
            new VillaDTO{id=1, nombre="prueba1", ocupantes = 20, metros_cuadrados=200},
            new VillaDTO{id=2, nombre="prueba2", ocupantes = 30, metros_cuadrados=300}
        };
    }
}

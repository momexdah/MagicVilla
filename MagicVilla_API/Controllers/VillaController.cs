using MagicVilla_API.Datos;
using MagicVilla_API.DTO;
using MagicVilla_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _context;

        public VillaController(ILogger<VillaController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.LogInformation("Obtener las Villas");
            return Ok(_context.Villas.ToList());
        }

        [HttpGet("GetById", Name = "GetVillaById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> GetVillaById(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al traer Villa con ID" + id);
                return BadRequest();
            }

            // var villa = VillaStore.VillaList.FirstOrDefault(v => v.id == id); 
            var villa = _context.Villas.FirstOrDefault(v => v.id == id);

            if (villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> SetVilla([FromBody] VillaDTO villaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Villas.FirstOrDefault(v => v.nombre.ToLower() == villaDto.nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe");
                return BadRequest(ModelState);
            }

            if (villaDto == null)
            {
                return BadRequest();
            }
            if (villaDto.id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            // villa.id = VillaStore.VillaList.OrderByDescending(v => v.id).FirstOrDefault().id + 1;
            // VillaStore.VillaList.Add(villa);

            Villa modelo = new()
            {
                nombre = villaDto.nombre,
                detalle = villaDto.detalle,
                imagen_url = villaDto.imagen_url,
                ocupantes = villaDto.ocupantes,
                Tarifa = villaDto.tarifa,
                metros_cuadrados = villaDto.metros_cuadrados,
                amenidad = villaDto.amenidad
            };
            _context.Villas.Add(modelo);
            _context.SaveChanges();

            return CreatedAtRoute("GetVillaById", new { id = villaDto.id }, villaDto);
        }

        [HttpDelete("DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = _context.Villas.FirstOrDefault(villa => villa.id == id);

            if (villa == null)
            {
                return NotFound();
            }

            // VillaStore.VillaList.Remove(villa);

            _context.Villas.Remove(villa);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
        {
            if(villaDTO == null || id == villaDTO.id)
            {
                return BadRequest();
            }

            // var villa = VillaStore.VillaList.FirstOrDefault(villa => villa.id == id);
            // villa.nombre = villaDTO.nombre;
            // villa.ocupantes = villaDTO.ocupantes;
            // villa.metros_cuadrados = villaDTO.metros_cuadrados;

            Villa modelo = new()
            {
                id = villaDTO.id,
                nombre = villaDTO.nombre,
                detalle = villaDTO.detalle,
                imagen_url = villaDTO.imagen_url,
                ocupantes = villaDTO.ocupantes,
                Tarifa = villaDTO.tarifa,
                metros_cuadrados = villaDTO.metros_cuadrados,
                amenidad = villaDTO.amenidad
            };

            _context.Update(modelo);
            _context.SaveChanges();
            
            return NoContent();
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            // var villa = VillaStore.VillaList.FirstOrDefault(villa => villa.id == id);
            var villa = _context.Villas.AsNoTracking().FirstOrDefault(v => v.id == id);

            
            VillaDTO villaDto = new()
            {
                id = villa.id,
                nombre = villa.nombre,
                detalle = villa.detalle,
                imagen_url = villa.imagen_url,
                ocupantes = villa.ocupantes,
                tarifa = villa.Tarifa,
                metros_cuadrados = villa.metros_cuadrados,
                amenidad = villa.amenidad
            };

            if (villa == null)
            {
                return BadRequest();
            }
            
            patchDTO.ApplyTo(villaDto ,ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa modelo = new()
            {
                id = villaDto.id,
                nombre = villaDto.nombre,
                detalle = villaDto.detalle,
                imagen_url = villaDto.imagen_url,
                ocupantes = villaDto.ocupantes,
                Tarifa = villaDto.tarifa,
                metros_cuadrados = villaDto.metros_cuadrados,
                amenidad = villaDto.amenidad
            };

            _context.Villas.Update(modelo);
            _context.SaveChanges();
            
            return NoContent();
        }
    }
}

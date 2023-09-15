using MagicVilla_API.Datos;
using MagicVilla_API.DTO;
using MagicVilla_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;

        public VillaController(ILogger<VillaController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.LogInformation("Obtener las Villas");
            return Ok(VillaStore.VillaList);
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

            var villa = VillaStore.VillaList.FirstOrDefault(v => v.id == id);

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
        public ActionResult<VillaDTO> SetVilla([FromBody] VillaDTO villa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (VillaStore.VillaList.FirstOrDefault(v => v.nombre.ToLower() == villa.nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe");
                return BadRequest(ModelState);
            }

            if (villa == null)
            {
                return BadRequest();
            }
            if (villa.id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            villa.id = VillaStore.VillaList.OrderByDescending(v => v.id).FirstOrDefault().id + 1;
            VillaStore.VillaList.Add(villa);

            return CreatedAtRoute("GetVillaById", new { id = villa.id }, villa);
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
            var villa = VillaStore.VillaList.FirstOrDefault(villa => villa.id == id);

            if (villa == null)
            {
                return NotFound();
            }

            VillaStore.VillaList.Remove(villa);

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

            var villa = VillaStore.VillaList.FirstOrDefault(villa => villa.id == id);
            villa.nombre = villaDTO.nombre;
            villa.ocupantes = villaDTO.ocupantes;
            villa.metros_cuadrados = villaDTO.metros_cuadrados;

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

            var villa = VillaStore.VillaList.FirstOrDefault(villa => villa.id == id);
            
            patchDTO.ApplyTo(villa ,ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}

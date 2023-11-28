using CriptoControl.Domain.Interfaces;
using CriptoControl.Model;
using CriptoControl.Model.DTO.Cripto;
using Microsoft.AspNetCore.Mvc;

namespace CriptoControl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PortifolioController : ControllerBase
    {
        public readonly ICriptoRepository _cripto;

        public PortifolioController(ICriptoRepository cripto)
        {
            _cripto = cripto;
        }

        #region Get List of Criptos
        /// <summary>
        /// Get List of Criptos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Cripto>))]
        public async Task<IEnumerable<Cripto>> GetAll()
        {
            return await _cripto.GetAll();
        }
        #endregion

        #region Get Individual Cripto
        /// <summary>
        /// Get Individual Cripto
        /// </summary>
        /// <param name="id">The id of the Cripto</param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetCripto")]
        [ProducesResponseType(200, Type = typeof(Cripto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesDefaultResponseType]
        public async Task<Cripto> GetProject(int id)
        {
            return await _cripto.Get(id);
        }
        #endregion

        #region Create Cripto
        /// <summary>
        /// Create Cripto
        /// </summary>
        /// <param name="cripto">Object of the Cripto</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Cripto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProject([FromBody] Cripto cripto)
        {
            if (cripto == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _cripto.Add(cripto))
            {
                ModelState.AddModelError("", $"Something went wrong when you trying to save {cripto.Name}");
                return StatusCode(500, ModelState);
            }

            //return CreatedAtRoute("GetCripto", new { version = HttpContext.GetRequestedApiVersion().ToString(), id = projectsObj.Id }, projectsObj);
            return Ok(cripto);
        }
        #endregion

        #region Update Cripto
        /// <summary>
        /// Update Cripto
        /// </summary>
        /// <param name="cripto">Object of the Cripto</param>
        /// <returns></returns>
        [HttpPatch(Name = "UpdateCripto")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCripto([FromBody] Cripto cripto)
        {
            if (cripto == null)
            {
                return BadRequest(ModelState);
            }

            if (!await _cripto.Update(cripto))
            {
                ModelState.AddModelError("", $"Something went wrong when you trying to update {cripto.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        #endregion

        #region Delete Cripto
        /// <summary>
        /// Delete Cripto
        /// </summary>
        /// <param name="id">Id of the Cripto</param>
        /// <returns></returns>
        [HttpDelete("{id:int}", Name = "DeleteCripto")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCripto(int id)
        {
            if (!(await _cripto.Exists(id)))
            {
                return NotFound();
            }

            //var criptoDto = await _cripto.Get(id);

            if (!(await _cripto.Remove(id)))
            {
                ModelState.AddModelError("", $"Something went wrong when you trying to delete id {id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        #endregion
    }
}
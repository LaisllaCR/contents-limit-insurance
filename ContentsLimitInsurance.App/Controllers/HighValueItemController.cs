using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContentsLimitInsurance.App.Dtos;
using ContentsLimitInsurance.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContentsLimitInsurance.App.Controllers
{
    [Route("api/high-value-items")]
    [ApiController]
    public class HighValueItemController : ControllerBase
    {
        private readonly IHighValueItemsService _highValueItemService;

        public HighValueItemController(IHighValueItemsService highValueItemService)
        {
            _highValueItemService = highValueItemService ?? throw new ArgumentNullException(nameof(highValueItemService));
        }

        [HttpGet("user/{id}", Name = "GetHighValueItemsByUser")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<HighValueItemDto>>> GetHighValueItemsByUser([FromRoute]int id)
        {
            try
            {
                return Ok(_highValueItemService.GetHighValueItemsByUser(id));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("categories/user/{id}", Name = "GetHighValueItemsPerCategoriesByUser")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<CategoryWithItemsDto>>> GetHighValueItemsPerCategoriesByUser([FromRoute] int id)
        {
            try
            {
                return Ok(_highValueItemService.GetHighValueItemsPerCategoriesByUser(id));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}", Name = "GetHighValueItem")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<HighValueItemDto>> GetHighValueItem([FromRoute] int id)
        {
            try
            {
                HighValueItemDto highValueItemDto = _highValueItemService.GetHighValueItemDto(id);

                if (highValueItemDto == null)
                {
                    return NotFound();
                }

                return Ok(highValueItemDto);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost(Name = "PostHighValueItem")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<HighValueItemDto>> PostHighValueItem([FromBody] HighValueItemDto highValueItemDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                HighValueItemDto createdHighValueItemDto = _highValueItemService.AddHighValueItem(highValueItemDto);

                return CreatedAtAction(nameof(PostHighValueItem), new { id = createdHighValueItemDto.HighValueItemId }, createdHighValueItemDto);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}", Name = "DeleteHighValueItem")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<HighValueItemDto>> DeleteHighValueItem([FromRoute] int id)
        {
            if (!HighValueItemExists(id))
            {
                return NotFound();
            }

            try
            {
                var result = _highValueItemService.DeleteHighValueItem(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private bool HighValueItemExists(int id)
        {
            try
            {
                var result = _highValueItemService.HighValueItemExists(id);
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}

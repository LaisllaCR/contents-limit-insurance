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

        [HttpGet("user/{id}", Name = "GetAllHighValueItemsByUser")]
        public ActionResult<IEnumerable<HighValueItemDto>> GetAllHighValueItemsByUser([FromRoute]int id)
        {
            try
            {
                return Ok(_highValueItemService.GetAllHighValueItemsByUser(id));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("categories/user/{id}", Name = "GetHighValueItemsPerCategories")]
        public ActionResult<IEnumerable<CategoryPerUserDto>> GetHighValueItemsPerCategories([FromRoute] int id)
        {
            try
            {
                return Ok(_highValueItemService.GetHighValueItemsPerCategories(id));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}", Name = "GetHighValueItem")]
        public IActionResult GetHighValueItem([FromRoute] int id)
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

        [HttpPost]
        public async Task<IActionResult> PostHighValueItem([FromBody] HighValueItemDto highValueItemDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                HighValueItemDto createdHighValueItemDto = _highValueItemService.AddHighValueItem(highValueItemDto);

                return CreatedAtAction("GetHighValueItem", new { id = createdHighValueItemDto.HighValueItemId }, createdHighValueItemDto);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHighValueItem([FromRoute] int id)
        {
            if (!HighValueItemExists(id))
            {
                return NotFound();
            }

            try
            {                
                return Ok(_highValueItemService.DeleteHighValueItem(id));
            }
            catch
            {
                return BadRequest();
            }
        }

        private bool HighValueItemExists(int id)
        {
            try
            {
                return _highValueItemService.HighValueItemExists(id);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}

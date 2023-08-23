using Microsoft.AspNetCore.Mvc;
using JosesBarAPI.Dtos;
using JosesBarAPI.Entities;
using JosesBarAPI.Repository;
using JosesBarAPI.Exceptions;

namespace JosesBarAPI.Controllers
{
    [ApiController]
    [Route("v1")]
    public class ProductController : ControllerBase
    {
        private IProductRepository _repository;

        public ProductController(IProductRepository productReporitory)
        {
            _repository = productReporitory;
        }


        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var response = await _repository.GetProducts();
                return Ok(response);
            }
            catch (InternalServerError)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("products/{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            try
            {
                var product = await _repository.GetProductByID(id);
                if (product == null)
                    return NoContent();
                return Ok(product);
            }
            catch (InternalServerError)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("products/filters")]
        public async Task<IActionResult> GetAsync([FromQuery] string? description)
        {
            try
            {
                var product = await _repository.GetProductByDescription(description);
                if (product == null)
                    return NoContent();
                return Ok(product);
            }
            catch (InternalServerError)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("products")]
        public async Task<IActionResult> PostAsync([FromBody] CreateProduct product)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var prod = await _repository.InsertProduct(product);
                return Created($"v1/products/{prod.Id}", prod);
            }
            catch (InternalServerError)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        [Route("products/{id}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] UpdateProduct product)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var prod = await _repository.UpdateProduct(product, id);
                if (prod == null)
                    return NotFound();
                return Ok(prod);
            }
            catch (InternalServerError)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("products/{id}")]

        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var result = false;
            try
            {
                result = await _repository.DeleteProduct(id);
            }
            catch (InternalServerError)
            {
                return StatusCode(500);
            }

            if (result == true)
                return Ok();
            else
                return NotFound();
        }
    }
}
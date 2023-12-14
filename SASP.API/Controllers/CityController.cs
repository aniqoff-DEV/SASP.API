using Microsoft.AspNetCore.Mvc;
using SASP.API.Entities;
using SASP.API.Repositories;
using SASP.API.Repositories.Contracts;

namespace SASP.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IRepository<City> _cityRepository;

        public CityController(IRepository<City> cityRepository)
        {
            _cityRepository = cityRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            try
            {
                var cities = await _cityRepository.GetItems();

                if (cities == null)
                {
                    return NotFound();
                }
                return Ok(cities);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");

            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(City), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostCity(City city)
        {
            try
            {
                var newCity = await _cityRepository.CreateItem(city);

                if (newCity == null)
                {
                    return NoContent();
                }

                return Ok(newCity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<City>> DeleteCity(int id)
        {
            try
            {
                var city = await _cityRepository.DeleteItem(id);

                if (city == null) return NotFound();

                return Ok(city);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<City>> UpdateCity(int id, City city)
        {
            try
            {
                var cityToUpdate = await _cityRepository.UpdateItem(id, city);

                if (cityToUpdate == null) return NotFound();

                return Ok(cityToUpdate);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}

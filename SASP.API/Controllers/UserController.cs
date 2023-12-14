using Microsoft.AspNetCore.Mvc;
using SASP.API.Dtos;
using SASP.API.Entities;
using SASP.API.Extensions;
using SASP.API.Repositories.Contracts;

namespace SASP.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Country> _countryRepository;

        public UserController(IRepository<User> userRepository, IRepository<City> cityRepository, IRepository<Country> countryRepository)
        {
            _userRepository = userRepository;
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetItems();
                var cities = await _cityRepository.GetItems();
                var country = await _countryRepository.GetItems();

                if (users == null)
                {
                    return NotFound();
                }
                else
                {
                    var userDtos = users.ConvertUserToDto(cities,country);

                    return Ok(userDtos);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");

            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            try
            {
                var user = await _userRepository.GetItemById(id);

                if (user == null)
                {
                    return NotFound();
                }
                var city = await _cityRepository.GetItemById(user.CityId);
                var country = await _countryRepository.GetItemById(user.CountryId);

                if (city == null || country == null) 
                    return NotFound();

                var userDto = user.ConvertUserToDto(city,country);

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser([FromBody] User user)
        {
            try
            {
                var newCartItem = await _userRepository.CreateItem(user);

                if (newCartItem == null)
                {
                    return NoContent();
                }

                var city = await _cityRepository.GetItemById(user.CityId);
                var country = await _countryRepository.GetItemById(user.CountryId);

                if (city == null || country == null)
                    return NotFound();

                var userDto = newCartItem.ConvertUserToDto(city, country);

                return Ok(userDto);


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<UserDto>> DeleteUser(int id)
        {
            try
            {
                var user = await _userRepository.DeleteItem(id);

                if (user == null)
                {
                    return NotFound();
                }

                var city = await _cityRepository.GetItemById(user.CityId);
                var country = await _countryRepository.GetItemById(user.CountryId);

                if (city == null || country == null)
                    return NotFound();

                var userDto = user.ConvertUserToDto(city, country);

                return Ok(userDto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<UserDto>> UpdateUser(int id, User user)
        {
            try
            {
                var cartToUpdate = await _userRepository.UpdateItem(id, user);
                if (cartToUpdate == null)
                {
                    return NotFound();
                }

                var city = await _cityRepository.GetItemById(user.CityId);
                var country = await _countryRepository.GetItemById(user.CountryId);

                if (city == null || country == null)
                    return NotFound();

                var userDto = user.ConvertUserToDto(city, country);

                return Ok(userDto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}

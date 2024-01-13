using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SASP.API.Dtos;
using SASP.API.Entities;
using SASP.API.Enums;
using SASP.API.Extensions;
using SASP.API.Repositories.Contracts;
using System.Diagnostics.Metrics;

namespace SASP.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderHistoryController : ControllerBase
    {
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Issue> _issueRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Subscription> _subscriptionRepository;
        private readonly IOrderRepository _orderRepository;


        public OrderHistoryController
            (
                IOrderRepository orderRepository,
                IRepository<Issue> issueRepository,
                IRepository<User> userRepository,
                IRepository<City> cityRepository,
                IRepository<Country> countryRepository,
                IRepository<Subscription> subscriptionRepository
            )
        {
            _orderRepository = orderRepository;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
            _issueRepository = issueRepository;
            _userRepository = userRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderHistoryDto>> GetOrder(int id)
        {
            try
            {
                var order = await _orderRepository.GetItemById(id);

                if (order == null)
                {
                    return NotFound();
                }

                var user = await _userRepository.GetItemById(order.UserId);
                var issue = await _issueRepository.GetItemById(order.IssueId);

                if (user == null || issue == null)
                    return NotFound();

                var country = await _countryRepository.GetItemById(user.CountryId);
                var city = await _cityRepository.GetItemById(user.CityId);

                var orderDto = order.ConvertOrderToDto(user,issue,country,city);

                return Ok(orderDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderHistoryDto>>> GetOrders()
        {
            try
            {
                var orders = await _orderRepository.GetItems();
                var issues = await _issueRepository.GetItems();
                var users = await _userRepository.GetItems();
                var countries = await _countryRepository.GetItems();
                var cities = await _cityRepository.GetItems();


                if (orders == null)
                {
                    return NotFound();
                }
                else
                {
                    var ordersToDto = orders.ConvertOrderToDto(users, issues, countries, cities);

                    return Ok(ordersToDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");

            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderHistory), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostOrder(OrderHistory order)
        {
            try
            {
                if(order.Status != nameof(OrderStatusEnum.InProcessing))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Неверный статус");
                }
                
                var user = await _userRepository.GetItemById(order.UserId);
                var issue = await _issueRepository.GetItemById(order.IssueId);

                if (user == null || issue == null)
                    return NotFound();

                var subs = await _subscriptionRepository.GetItems();
                var isSub = subs.Where(s => s.IssueId == issue.IssueId && s.UserId == user.UserId);

                if (isSub == null) return StatusCode(StatusCodes.Status400BadRequest, "Не удалось найти подписку");

                var newOrder = await _orderRepository.CreateItem(order);

                if (newOrder == null)
                {
                    return NoContent();
                }

                var country = await _countryRepository.GetItemById(user.CountryId);
                var city = await _cityRepository.GetItemById(user.CityId);
                var orderDto = order.ConvertOrderToDto(user, issue, country, city);

                return Ok(orderDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<OrderHistoryDto>> DeleteCountry(int id)
        {
            try
            {
                var order = await _orderRepository.DeleteItem(id);

                if (order == null) return NotFound();

                var user = await _userRepository.GetItemById(order.UserId);
                var issue = await _issueRepository.GetItemById(order.IssueId);
                var country = await _countryRepository.GetItemById(user.CountryId);
                var city = await _cityRepository.GetItemById(user.CityId);

                if (user == null || issue == null)
                    return NotFound();

                var orderDto = order.ConvertOrderToDto(user, issue, country, city);

                return Ok(orderDto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<Subscription>> UpdateCountry(int id, OrderHistory order)
        {
            try
            {
                if (!OrderStatus.IsStatus(order.Status))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Неверный статус");
                }

                var orderToUpdate = await _orderRepository.UpdateItem(id, order.Status);

                if (orderToUpdate == null) return NotFound();

                var user = await _userRepository.GetItemById(order.UserId);
                var issue = await _issueRepository.GetItemById(order.IssueId);
                var country = await _countryRepository.GetItemById(user.CountryId);
                var city = await _cityRepository.GetItemById(user.CityId);

                if (user == null || issue == null)
                    return NotFound();

                var orderDto = order.ConvertOrderToDto(user, issue, country, city);

                return Ok(orderDto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SASP.API.Dtos;
using SASP.API.Entities;
using SASP.API.Extensions;
using SASP.API.Repositories;
using SASP.API.Repositories.Contracts;

namespace SASP.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly IRepository<Subscription> _subscriptionRepository;
        private readonly IRepository<Issue> _issueRepository;
        private readonly IRepository<User> _userRepository;

        const decimal tax = (decimal)1.01;
        const decimal allowance = (decimal)1.18;

        public SubscriptionController(IRepository<Subscription> subscriptionRepository, IRepository<Issue> issueRepository, IRepository<User> userRepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _issueRepository = issueRepository;
            _userRepository = userRepository;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SubscriptionDto>> GetSub(int id)
        {
            try
            {
                var sub = await _subscriptionRepository.GetItemById(id);

                if (sub == null)
                {
                    return NotFound();
                }
                var user = await _userRepository.GetItemById(sub.UserId);
                var issue = await _issueRepository.GetItemById(sub.IssueId);

                if (user == null || issue == null)
                    return NotFound();

                var subDto = sub.ConvertSubscriptionToDto(user, issue);

                return Ok(subDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscriptionDto>>> GetSubs()
        {
            try
            {
                var subs = await _subscriptionRepository.GetItems();
                var issues = await _issueRepository.GetItems();
                var users = await _userRepository.GetItems();


                if (subs == null)
                {
                    return NotFound();
                }
                else
                {
                    var subsDto = subs.ConvertSubscriptionToDto(users, issues);

                    return Ok(subsDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");

            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Subscription), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostCountry(Subscription sub)
        {
            try
            {
                sub.Price = await PriceCalculation(sub.Price, sub.EndSub,sub.StartSub);
                var newSub = await _subscriptionRepository.CreateItem(sub);

                if (newSub == null)
                {
                    return NoContent();
                }
                var user = await _userRepository.GetItemById(sub.UserId);
                var issue = await _issueRepository.GetItemById(sub.IssueId);

                if (user == null || issue == null)
                    return NotFound();

                var subDto = sub.ConvertSubscriptionToDto(user, issue);

                return Ok(subDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Subscription>> DeleteCountry(int id)
        {
            try
            {
                var sub = await _subscriptionRepository.DeleteItem(id);

                if (sub == null) return NotFound();

                var user = await _userRepository.GetItemById(sub.UserId);
                var issue = await _issueRepository.GetItemById(sub.IssueId);

                if (user == null || issue == null)
                    return NotFound();

                var subDto = sub.ConvertSubscriptionToDto(user, issue);

                return Ok(subDto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<Subscription>> UpdateCountry(int id, Subscription sub)
        {
            try
            {
                var subToUpdate = await _subscriptionRepository.UpdateItem(id, sub);

                if (subToUpdate == null) return NotFound();

                var user = await _userRepository.GetItemById(sub.UserId);
                var issue = await _issueRepository.GetItemById(sub.IssueId);

                if (user == null || issue == null)
                    return NotFound();

                var subDto = sub.ConvertSubscriptionToDto(user, issue);

                return Ok(subDto);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        private async Task<decimal> PriceCalculation(decimal priceNow, DateTime endSub, DateTime startSub)
        {
            decimal price = priceNow * (1 + 1 / (endSub.Day - startSub.Day)) * tax + allowance;
            return price;
        }
    }
}

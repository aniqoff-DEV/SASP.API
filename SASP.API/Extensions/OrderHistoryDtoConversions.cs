using SASP.API.Dtos;
using SASP.API.Entities;
using SASP.API.Enums;

namespace SASP.API.Extensions
{
    public static class OrderHistoryDtoConversions
    {        
        public static IEnumerable<OrderHistoryDto> ConvertOrderToDto
            (
            this IEnumerable<OrderHistory> orders,
            IEnumerable<User> users,
            IEnumerable<Issue> issues, 
            IEnumerable<Country> countries,
            IEnumerable<City> cities
            )
        {
            return (from order in orders
                    join user in users
                    on order.UserId equals user.UserId
                    join city in cities
                    on user.CityId equals city.CityId
                    join country in countries
                    on user.CountryId equals country.CountryId
                    join issue in issues
                    on order.IssueId equals issue.IssueId
                    select new OrderHistoryDto
                    {
                        OrderId = order.OrderId,
                        User = user.UserId + user.Name,
                        Issue = issue.Title,
                        City = city.Name,
                        Country = country.Name,
                        Status = OrderStatus.ConvertStatus(order.Status)
                    }).ToList();
        }

        public static OrderHistoryDto ConvertOrderToDto
            (
            this OrderHistory order,
            User user,
            Issue issue,
            Country country,
            City city
            )
        {
            return new OrderHistoryDto
            {
                OrderId = order.OrderId,
                User = user.UserId + user.Name,
                Issue = issue.Title,
                City = city.Name,
                Country = country.Name,
                Status = OrderStatus.ConvertStatus(order.Status)
            };
        }
    }
}

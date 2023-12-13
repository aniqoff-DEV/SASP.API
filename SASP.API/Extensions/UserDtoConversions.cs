using SASP.API.Dtos;
using SASP.API.Entities;

namespace SASP.API.Extensions
{
    public static class UserDtoConversions
    {
        public static IEnumerable<UserDto> ConvertUserToDto(this IEnumerable<User> users, IEnumerable<City> cities, IEnumerable<Country> countries)
        {
            return (from user in users
                    join city in cities
                    on user.CityId equals city.CityId
                    join country in countries
                    on user.CountryId equals country.CountryId
                    select new UserDto
                    {
                        UserId = user.UserId,
                        Name = user.Name,
                        Email = user.Email,
                        NumberPhone = user.NumberPhone,
                        City = city.Name,
                        Country = country.Name
                    }).ToList();
        }

        public static UserDto ConvertUserToDto(this User user, City city, Country country)
        {
            return new UserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                NumberPhone = user.NumberPhone,
                City = city.Name,
                Country = country.Name
            };
        }
    }
}

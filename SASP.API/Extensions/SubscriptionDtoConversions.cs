using SASP.API.Dtos;
using SASP.API.Entities;

namespace SASP.API.Extensions
{
    public static class SubscriptionDtoConversions
    {
        public static IEnumerable<SubscriptionDto> ConvertSubscriptionToDto(this IEnumerable<Subscription> subs, IEnumerable<User> users, IEnumerable<Issue> issues)
        {
            return (from sub in subs
                    join user in users
                    on sub.UserId equals user.UserId
                    join issue in issues
                    on sub.IssueId equals issue.IssueId
                    select new SubscriptionDto
                    {
                        SubscriptionId = sub.SubscriptionId,
                        Issue = issue.Title,
                        User = user.UserId + user.Name,
                        StartSub = sub.StartSub,
                        EndSub = sub.EndSub,
                        Price = sub.Price
                    }).ToList();
        }

        public static SubscriptionDto ConvertSubscriptionToDto(this Subscription sub, User user, Issue issue)
        {
            return new SubscriptionDto
            {
                SubscriptionId = sub.SubscriptionId,
                Issue = issue.Title,
                User = user.UserId + user.Name,
                StartSub = sub.StartSub,
                EndSub = sub.EndSub,
                Price = sub.Price
            };
        }
    }
}

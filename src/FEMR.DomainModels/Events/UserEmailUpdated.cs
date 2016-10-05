using FEMR.Core;

namespace FEMR.DomainModels.Events
{
    public class UserEmailUpdated : IEvent
    {
        public UserEmailUpdated(string email)
        {
            Email = email;
        }

        public string Email { get; }
    }
}

using FEMR.Core;

namespace FEMR.DomainModels.Events
{
    public class UserEmailUpdated : IEvent
    {
        public string Email { get; }

        public UserEmailUpdated(string email)
        {
            Email = email;
        }
    }
}

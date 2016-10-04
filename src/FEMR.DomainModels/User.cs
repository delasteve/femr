using System;
using System.Collections.Generic;
using System.Linq;
using FEMR.Core;
using FEMR.DomainModels.Events;

namespace FEMR.DomainModels
{
    public class User : Aggregate
    {
        public User(Guid userId, string email, string firstName, string lastName) : this()
        {
            RaiseEvent(new UserCreated(userId, email, firstName, lastName));
        }

        private User()
        {
            Register<UserCreated>(Apply);
            Register<UserEmailUpdated>(Apply);
        }

        public User(IEnumerable<IEvent> events) : this()
        {
            events
                .ToList()
                .ForEach(ApplyEvent);
        }

        public void UpdateEmail(string newEmail)
        {
            RaiseEvent(new UserEmailUpdated(newEmail));
        }

        private void Apply(UserCreated @event)
        {
            UserId = @event.UserId;
            Email = @event.Email;
            FirstName = @event.FirstName;
            LastName = @event.LastName;
        }

        private void Apply(UserEmailUpdated @event)
        {
            Email = @event.Email;
        }

        public override Guid Id => UserId;
        public Guid UserId { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
    }
}

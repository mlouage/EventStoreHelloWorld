using System;

namespace Contracts.Events
{
    public class DeposantCreatedEvent
    {
        public Guid Id { get; set; }

        public string DeposantNummer { get; set; }
    }
}
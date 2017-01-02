using System;
using System.Collections.Generic;

namespace Domain.Deposant
{
    public class DeposantState : IState
    {
        public Guid Id { get; private set; }

        public int Version { get; private set; }

        public DeposantState(IEnumerable<object> events)
        {
            if (events == null) return;
            foreach (var @event in events)
                Modify(@event);
        }

        public void Modify(object @event)
        {
            Version++;
            RedirectToWhen.InvokeEvent(this, @event);
        }
    }
}
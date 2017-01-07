using System;
using System.Collections.Generic;
using Domain.Deposant;

namespace Domain
{
    public class AggregateFactory
    {
        public T Create<T>(List<object> events) where T : class, IAggregate
        {
            //return (T)Activator.CreateInstance(typeof(T));

            if (typeof(T) == typeof(Deposant.Deposant))
            {
                var state = new DeposantState(events);
                return new Deposant.Deposant(state) as T;
            }

            throw new ArgumentException("Unknown aggregate type");
        }
    }
}
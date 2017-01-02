using System;
using System.Collections.Generic;

namespace Domain
{
    public class AggregateFactory
    {
        public T Create<T>(List<object> events) where T : IAggregate
        {
            return (T)Activator.CreateInstance(typeof(T));
        }
    }
}
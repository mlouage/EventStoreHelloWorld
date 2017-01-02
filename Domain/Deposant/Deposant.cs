using System;
using System.Collections.Generic;

namespace Domain.Deposant
{
    public class Deposant : AggregateBase<DeposantState>
    {
        public Deposant(DeposantState state) : base(state)
        {
        }
    }
}
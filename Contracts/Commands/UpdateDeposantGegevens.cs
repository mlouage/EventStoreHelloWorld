using System;
using Entities.Gegevens;

namespace Contracts.Commands
{
    public class UpdateDeposantGegevens
    {
        public Guid Id { get; set; }

        public Gegevens DeposantGegevens { get; set; }
    }
}
using System;
using Entities.Rekening;

namespace Contracts.Commands
{
    public class UpdateDeposantRekening
    {
        public Guid Id { get; set; }

        public Rekening Rekening { get; set; }
    }
}
using System;
using Entities.Aanspraak;

namespace Contracts.Commands
{
    public class BepaalDeposantAanspraak
    {
        public Guid Id { get; set; }

        public Aanspraak Aanspraak { get; set; }
    }
}
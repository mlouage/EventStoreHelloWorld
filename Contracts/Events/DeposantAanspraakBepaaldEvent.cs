using System;
using Entities.Aanspraak;

namespace Contracts.Events
{
    public class DeposantAanspraakBepaaldEvent
    {
        public Guid Id { get; set; }

        public Aanspraak Aanspraak { get; set; }
    }
}
using System;
using Entities.BesluitInformatie;

namespace Contracts.Events
{
    public class DeposantBesluitInformatieBepaaldEvent
    {
        public Guid Id { get; set; }

        public BesluitInformatie BesluitInformatie { get; set; }
    }
}
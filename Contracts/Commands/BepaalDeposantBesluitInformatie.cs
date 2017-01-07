using System;
using Entities.BesluitInformatie;

namespace Contracts.Commands
{
    public class BepaalDeposantBesluitInformatie
    {
        public Guid Id { get; set; }

        public BesluitInformatie BesluitInformatie { get; set; }
    }
}
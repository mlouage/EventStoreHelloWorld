using System;
using Contracts.Commands;

namespace Domain.Services.Deposant
{
    public interface IDeposantService
    {
        Guid When(CreateDeposant command);
        void When(UpdateDeposantGegevens command);
        void When(UpdateDeposantRekening command);
        void When(BepaalDeposantAanspraak command);
        void When(BepaalDeposantBesluitInformatie command);
    }
}
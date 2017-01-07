using System;
using Contracts.Commands;
using Domain.Services.Deposant;

namespace Domain.Deposant
{
    public class DeposantService : IDeposantService
    {
        private readonly IRepository _repository;

        public DeposantService(IRepository repository)
        {
            _repository = repository;
        }

        public Guid When(CreateDeposant command)
        {
            var id = Guid.NewGuid();
            Execute(id, deposant => deposant.Create(id, command.DeposantNummer));

            return id;
        }

        public void When(UpdateDeposantGegevens command)
        {
            var gegevens = command.DeposantGegevens;
            Execute(command.Id, deposant =>
            {
                deposant.UpdateGegevens(
                    gegevens.Naam, 
                    gegevens.Voornaam, 
                    gegevens.Adres, 
                    gegevens.Huisnummer, 
                    gegevens.Postcode, 
                    gegevens.Gemeente, 
                    gegevens.Land);
            });
        }

        public void When(UpdateDeposantRekening command)
        {
            var rekening = command.Rekening;
            Execute(command.Id, deposant =>
            {
                deposant.UpdateRekening(
                    rekening.RekeningNummer,
                    rekening.Bic,
                    rekening.Saldo,
                    rekening.Valuta);
            });
        }

        public void When(BepaalDeposantAanspraak command)
        {
            var aanspraak = command.Aanspraak;
            Execute(command.Id, deposant =>
            {
                deposant.BepaalAanspraak(
                    aanspraak.Saldo,
                    aanspraak.Bepaald    
                );
            });
        }

        public void When(BepaalDeposantBesluitInformatie command)
        {
            var besluitInformatie = command.BesluitInformatie;
            Execute(command.Id, deposant =>
            {
                deposant.BepaalBesluitInformatie(
                    besluitInformatie.Besluit,
                    besluitInformatie.Aanspraak
                );
            });
        }

        private void Execute(Guid id, Action<Deposant> action)
        {
            var deposant = _repository.GetById<Deposant>(id);
            action(deposant);
            _repository.Save(deposant);
        }
    }
}

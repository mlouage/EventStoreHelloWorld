using System;
using System.Collections.Generic;
using Contracts.Events;
using Entities.Aanspraak;
using Entities.BesluitInformatie;
using Entities.Gegevens;
using Entities.Rekening;

namespace Domain.Deposant
{
    public class DeposantState : IState
    {
        public Guid Id { get; private set; }

        public int Version { get; private set; }

        public string DeposantNummer { get; private set; }

        public Gegevens DeposantGegevens{ get; private set; }

        public Rekening Rekening { get; private set; }

        public Aanspraak Aanspraak { get; private set; }

        public BesluitInformatie BesluitInformatie { get; private set; }

        public bool HasNoRekening => Rekening == null || Rekening == default(Rekening);

        public bool HasNoAanspraak => Aanspraak == null || Aanspraak == default(Aanspraak);

        public bool HasBesluitInformatie => BesluitInformatie != null && BesluitInformatie != default(BesluitInformatie);

        public DeposantState(IEnumerable<object> events)
        {
            if (events == null) return;
            foreach (var @event in events)
                Modify(@event);
        }

        public void Modify(object @event)
        {
            Version++;
            RedirectToWhen.InvokeEvent(this, @event);
        }

        private void When(DeposantCreatedEvent e)
        {
            Id = e.Id;
            DeposantNummer = e.DeposantNummer;
        }

        private void When(DeposantGegevensUpdatedEvent e)
        {
            Id = e.Id;
            DeposantGegevens = e.DeposantGegevens;
        }

        private void When(DeposantRekeningUpdatedEvent e)
        {
            Id = e.Id;
            Rekening = e.Rekening;
        }

        private void When(DeposantAanspraakBepaaldEvent e)
        {
            Id = e.Id;
            Aanspraak = e.Aanspraak;
        }

        private void When(DeposantBesluitInformatieBepaaldEvent e)
        {
            Id = e.Id;
            BesluitInformatie = e.BesluitInformatie;
        }
    }
}
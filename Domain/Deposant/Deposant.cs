using System;
using Contracts.Events;
using Entities.Aanspraak;
using Entities.BesluitInformatie;
using Entities.Gegevens;
using Entities.Rekening;

namespace Domain.Deposant
{
    public class Deposant : AggregateBase<DeposantState>
    {
        public Deposant(DeposantState state) : base(state)
        {
        }

        public void Create(Guid id, string deposantNummer)
        {
            if (State.Version > 0)
            {
                throw new InvalidOperationException("Cannot recreate a deposant");
            }

            Apply(new DeposantCreatedEvent { Id = id, DeposantNummer = deposantNummer });
        }

        public void UpdateGegevens(string naam, string voornaam, string adres, string huisnummer, 
            string postcode, string gemeente, string land)
        {
            if (State.Version <= 0)
            {
                throw new InvalidOperationException("Deposant must be created first before adding gegevens");
            }

            if (State.HasBesluitInformatie)
            {
                throw new InvalidOperationException("Deposant already has besluitinformatie, no change can be made anymore");
            }

            Apply(new DeposantGegevensUpdatedEvent
            {
                Id = State.Id,
                DeposantGegevens = new Gegevens
                {
                    Naam = naam,
                    Voornaam = voornaam,
                    Adres = adres,
                    Huisnummer = huisnummer,
                    Postcode = postcode,
                    Gemeente = gemeente,
                    Land = land
                }
            });
        }

        public void UpdateRekening(string rekeningnummer, string bic, decimal saldo, string valuta)
        {
            if (State.Version <= 0)
            {
                throw new InvalidOperationException("Deposant must be created first before adding rekening");
            }

            if (State.HasBesluitInformatie)
            {
                throw new InvalidOperationException("Deposant already has besluitinformatie, no change can be made anymore");
            }

            Apply(new DeposantRekeningUpdatedEvent
            {
                Id = State.Id,
                Rekening = new Rekening
                {
                    RekeningNummer = rekeningnummer,
                    Bic = bic,
                    Saldo = saldo,
                    Valuta = valuta
                }
            });
        }

        public void BepaalAanspraak(decimal saldo, bool bepaald)
        {
            if (State.Version <= 0)
            {
                throw new InvalidOperationException("Deposant must be created first before the aanspraak can be bepaald");
            }

            if (State.HasNoRekening)
            {
                throw new InvalidOperationException("Deposant must have a rekening");
            }

            if (State.HasBesluitInformatie)
            {
                throw new InvalidOperationException("Deposant already has besluitinformatie, no change can be made anymore");
            }

            Apply(new DeposantAanspraakBepaaldEvent
            {
                Id = State.Id,
                Aanspraak = new Aanspraak
                {
                    Saldo = saldo,
                    Bepaald = bepaald
                }
            });
        }

        public void BepaalBesluitInformatie(int besluit, decimal aanspraak)
        {
            if (State.Version <= 0)
            {
                throw new InvalidOperationException("Deposant must be created first before the besluitinformatie can be bepaald");
            }

            if (State.HasNoAanspraak)
            {
                throw new InvalidOperationException("Deposant must have an aanspraak before the besluitinformatie can be bepaald");
            }

            if (State.HasBesluitInformatie)
            {
                throw new InvalidOperationException("Deposant already has besluitinformatie, no change can be made anymore");
            }

            Apply(new DeposantBesluitInformatieBepaaldEvent
            {
                Id = State.Id,
                BesluitInformatie = new BesluitInformatie
                {
                    Besluit = besluit,
                    Aanspraak = aanspraak                    
                }
            });
        }
    }
}
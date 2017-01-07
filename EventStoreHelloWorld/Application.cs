using System;
using System.Linq;
using System.Net;
using Contracts.Commands;
using Domain;
using Domain.Deposant;
using Domain.Services.Deposant;
using Entities.Gegevens;
using EventStorage;
using EventStore.ClientAPI;
using EventStoreHelloWorld.Models;
using GenFu;

namespace EventStoreHelloWorld
{
    public class Application
    {
        private readonly IRepository _repository;
        private IDeposantService _deposantService;

        public Application(IApplicationSettings applicationSettings)
        {
            _repository = InitGetEventStore(applicationSettings);
        }

        public void Run()
        {
            _deposantService = new DeposantService(_repository);

            A.Configure<Person>().Fill(p => p.Number).WithinRange(1000000, 9999999);
            var people = A.ListOf<Person>(10);

            for(int i = people.Count - 1; i >=0; i--)
            {
                Console.WriteLine(people[i]);

                people[i].Id = _deposantService.When(new CreateDeposant { DeposantNummer = people[i].Number.ToString() });
                _deposantService.When(new UpdateDeposantGegevens
                {
                    Id = people[i].Id,
                    DeposantGegevens = new Gegevens
                    {
                        Voornaam = people[i].Firstname,
                        Naam = people[i].Lastname,
                        Adres = GetAdresStraat(people[i].Address),
                        Huisnummer = GetHuisnummer(people[i].Address),
                        Postcode = string.Empty,
                        Gemeente = people[i].City,
                        Land = people[i].UsaState
                    }
                });
            }
        }

        private string GetAdresStraat(string value)
        {
            var parts = value.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            return string.Join(" ", parts.Skip(1).Take(parts.Length - 1));
        }

        private string GetHuisnummer(string value)
        {
            return value.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
        }

        private IRepository InitGetEventStore(IApplicationSettings applicationSettings)
        {
            var endpoint = GetEventStoreEndpoint(applicationSettings);
            var connection = EventStoreConnection.Create(endpoint);
            connection.ConnectAsync().Wait();
            var factory = new AggregateFactory();
            return new GesRepository(connection, factory);
        }

        private IPEndPoint GetEventStoreEndpoint(IApplicationSettings applicationSettings)
        {
            var ipAddress = IPAddress.Parse(applicationSettings.GesIpAddress);
            var endpoint = new IPEndPoint(ipAddress, applicationSettings.GesTcpIpPort);
            return endpoint;
        }
    }
}

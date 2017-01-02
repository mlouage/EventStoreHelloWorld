﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using EventStore.ClientAPI;
using Newtonsoft.Json;

namespace EventStoreHelloWorld
{
    public class GesRepository
    {
        private readonly IEventStoreConnection _connection;
        private readonly AggregateFactory _factory;

        public GesRepository(IEventStoreConnection connection, AggregateFactory factory)
        {
            _connection = connection;
            _factory = factory;
        }

        public T GetById<T>(Guid id) where T : IAggregate
        {
            var events = new List<object>();
            StreamEventsSlice currentSlice;
            var nextSliceStart = StreamPosition.Start;
            var streamName = GetStreamName<T>(id);
            do
            {
                currentSlice = _connection
                    .ReadStreamEventsForwardAsync(streamName, nextSliceStart, 200, false)
                    .Result;
                nextSliceStart = currentSlice.NextEventNumber;
                events.AddRange(currentSlice.Events.Select(x => x.DeserializeEvent()));
            } while (!currentSlice.IsEndOfStream);

            var aggregate = _factory.Create<T>(events);
            return aggregate;
        }

        public void Save(IAggregate aggregate)
        {
            var commitId = Guid.NewGuid();
            var events = aggregate.GetUncommittedEvents().ToArray();

            if (events.Any() == false)
                return;

            var streamName = GetStreamName(aggregate.GetType(), aggregate.Id);
            var originalVersion = aggregate.Version - events.Count();
            var expectedVersion = originalVersion == 0 ? ExpectedVersion.NoStream : originalVersion - 1;
            var commitHeaders = new Dictionary<string, object>
            {
                {"CommitId", commitId},
                {"AggregateClrType", aggregate.GetType().AssemblyQualifiedName}
            };
            var eventsToSave = events.Select(e => e.ToEventData(commitHeaders)).ToList();

            _connection.AppendToStreamAsync(streamName, expectedVersion, eventsToSave).Wait();

            aggregate.ClearUncommittedEvents();
        }

        

        private string GetStreamName<T>(Guid id)
        {
            return GetStreamName(typeof(T), id);
        }

        private string GetStreamName(Type type, Guid id)
        {
            return string.Format("{0}-{1}", type.Name, id);
        }
    }
}

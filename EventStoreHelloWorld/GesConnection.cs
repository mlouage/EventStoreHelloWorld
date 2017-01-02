using System.Net;
using EventStore.ClientAPI;

namespace EventStoreHelloWorld
{
    public static class GesConnection
    {
        private const int Defaultport = 1113;

        public static IEventStoreConnection Create()
        {
            var settings = ConnectionSettings.Create()
               .UseConsoleLogger();

            return EventStoreConnection.Create(settings, new IPEndPoint(IPAddress.Loopback, Defaultport));
        }
    }
}

using System.Net;
using Domain;
using EventStorage;
using EventStore.ClientAPI;
using StructureMap;

namespace EventStoreHelloWorld
{
    public class ConsoleRegistry : Registry
    {
        public ConsoleRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });          
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using EventStoreHelloWorld.Models;
using GenFu;
using StructureMap;

namespace EventStoreHelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = Container.For<ConsoleRegistry>();
            var app = container.GetInstance<Application>();

            app.Run();            
        }
    }
}

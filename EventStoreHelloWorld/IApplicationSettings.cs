using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventStoreHelloWorld
{
    public interface IApplicationSettings
    {
        string GesIpAddress { get; }
        int GesHttpPort { get; }
        int GesTcpIpPort { get; }
        string GesUserName { get; }
        string GesPassword { get; }
    }
}

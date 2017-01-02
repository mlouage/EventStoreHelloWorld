using System;

namespace Domain
{
    public interface IState
    {
        Guid Id { get; }
        int Version { get; }
        void Modify(object e);
    }
}
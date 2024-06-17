using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public sealed class ProcedureCreatedEvent : BaseEvent
    {

        public ProcedureCreatedEvent(Procedure procedure)
        {
            Procedure = procedure;
        }

        public Procedure Procedure { get; }
    }
}

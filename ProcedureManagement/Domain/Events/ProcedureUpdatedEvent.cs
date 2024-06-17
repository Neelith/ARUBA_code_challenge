using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class ProcedureUpdatedEvent : BaseEvent
    {
        public ProcedureUpdatedEvent(Procedure procedure)
        {
            Procedure = procedure;
        }

        public Procedure Procedure { get; }
    }
}

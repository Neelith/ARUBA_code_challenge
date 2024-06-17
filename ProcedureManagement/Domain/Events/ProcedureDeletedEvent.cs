using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class ProcedureDeletedEvent : BaseEvent
    {
        public ProcedureDeletedEvent(Procedure procedure)
        {
            Procedure = procedure;
        }

        public Procedure Procedure { get; }
    }
}

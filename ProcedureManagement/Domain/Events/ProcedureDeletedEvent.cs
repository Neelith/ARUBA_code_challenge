using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

using Domain.Common;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProcedureStatusHistoryRecord : BaseAuditableEntity
    {
        public int ProcedureId { get; init; }
        public ProcedureStatus ProcedureStatus { get; init; }

        public static ProcedureStatusHistoryRecord Create(int procedureId, ProcedureStatus procedureStatus)
        {
            return new ProcedureStatusHistoryRecord(procedureId, procedureStatus);
        }

        private ProcedureStatusHistoryRecord(int procedureId, ProcedureStatus procedureStatus)
        {
            this.ProcedureStatus = procedureStatus;
            this.ProcedureId = procedureId;
            this.CreatedOn = DateTime.UtcNow;
        }
    }
}

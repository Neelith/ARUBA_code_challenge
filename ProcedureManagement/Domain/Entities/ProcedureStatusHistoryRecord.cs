using Domain.Common;
using Domain.Enum;

namespace Domain.Entities
{
    public class ProcedureStatusHistoryRecord : BaseAuditableEntity
    {
        public int ProcedureId { get; internal init; }
        public ProcedureStatus ProcedureStatus { get; internal init; }

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

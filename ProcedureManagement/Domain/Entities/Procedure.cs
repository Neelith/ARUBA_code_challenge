using Domain.Common;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Procedure : BaseAuditableEntity
    {
        private ProcedureStatus _currentProcedureStatus;

        public ProcedureStatus CurrentProcedureStatus 
        { 
            get => _currentProcedureStatus; 
            set => SetProcedureStatus(value);
        }

        private Queue<ProcedureStatusHistoryRecord> _statusHistory;

        public IReadOnlyCollection<ProcedureStatusHistoryRecord> StatusHistory => _statusHistory.ToList().AsReadOnly();

        public static Procedure Create()
        {
            return new Procedure();
        }

        private Procedure()
        {
            _currentProcedureStatus = ProcedureStatus.Created;
            _statusHistory = new Queue<ProcedureStatusHistoryRecord>();
            CreatedOn = DateTime.UtcNow;
        }

        public void SetProcedureStatus(ProcedureStatus procedureStatus)
        {
            _statusHistory.Enqueue(ProcedureStatusHistoryRecord.Create(Id, _currentProcedureStatus));
            _currentProcedureStatus = procedureStatus;
        }
    }
}

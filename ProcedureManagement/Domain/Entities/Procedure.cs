using Domain.Common;
using Domain.Constants;
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

        private List<Attachment> _attachments;

        public IReadOnlyCollection<Attachment> Attachments => _attachments.ToList().AsReadOnly();

        public static Procedure Create()
        {
            return new Procedure();
        }

        private Procedure()
        {
            _currentProcedureStatus = ProcedureStatus.Created;
            _statusHistory = new();
            _attachments = new();
            CreatedOn = DateTime.UtcNow;
        }

        public void SetProcedureStatus(ProcedureStatus procedureStatus)
        {
            _statusHistory.Enqueue(ProcedureStatusHistoryRecord.Create(Id, _currentProcedureStatus));
            _currentProcedureStatus = procedureStatus;
        }

        public void AddAttachment(byte[] content)
        {
            _attachments.Add(Attachment.Create(Id, content));
        }

        public void UpdateAttachment(int attachmentId, byte[] content)
        {
            var attachment = _attachments.Find(x => x.Id == attachmentId);

            if(attachment is null)
            {
                //TODO
                throw new Exception(DomainErrors.AttachmentIdNotFound);
            }

            attachment.Content = content;
        }
    }
}

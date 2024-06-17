using Domain.Common;
using Domain.Common.Exceptions;
using Domain.Constants;
using Domain.Enum;

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

        private List<ProcedureStatusHistoryRecord> _statusHistory;

        public IReadOnlyCollection<ProcedureStatusHistoryRecord> StatusHistory => _statusHistory.ToList().AsReadOnly();

        private List<Attachment> _attachments;

        public IReadOnlyCollection<Attachment> Attachments => _attachments.AsReadOnly();

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
            _statusHistory.Add(ProcedureStatusHistoryRecord.Create(Id, _currentProcedureStatus));
            _currentProcedureStatus = procedureStatus;
        }

        public void AddOrUpdateAttachment(byte[] content, string fileName, int? attachmentId = null)
        {
            if (!IsProcedureUpdateable())
            {
                throw new BadRequestException(DomainErrors.ProcedureNotUpdateable);
            }

            if (attachmentId.HasValue)
            {
                UpdateAttachment(attachmentId.Value, content, fileName);
                return;
            }

            AddAttachment(content, fileName);
        }

        private void AddAttachment(byte[] content, string fileName)
        {
            _attachments.Add(Attachment.Create(Id, content, fileName));
        }

        private void UpdateAttachment(int attachmentId, byte[] content, string fileName)
        {
            var attachment = _attachments.Find(x => x.Id == attachmentId);

            if (attachment is null)
            {
                throw new BadRequestException(DomainErrors.AttachmentNotFound);
            }

            attachment.SetContent(content, fileName);
        }

        private bool IsProcedureUpdateable()
        {
            return _currentProcedureStatus == ProcedureStatus.Created;
        }
    }
}

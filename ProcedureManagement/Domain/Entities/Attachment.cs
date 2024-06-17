using Domain.Common;

namespace Domain.Entities
{
    public class Attachment : BaseAuditableEntity
    {
        public int ProcedureId { get; internal init; }

        public byte[] Content { get; private set; }

        public string FileName { get; private set; }


        public void SetContent(byte[] content, string fileName)
        {
            this.LastModifiedOn = DateTime.UtcNow;
            this.Content = content;
            this.FileName = fileName;
        }

        public static Attachment Create(int procedureId, byte[] content, string fileName)
        {
            return new Attachment(procedureId, content, fileName);
        }

        private Attachment(int procedureId, byte[] content, string fileName)
        {
            this.ProcedureId = procedureId;
            this.Content = content;
            this.FileName = fileName;
            this.CreatedOn = DateTime.UtcNow;
        }
    }
}

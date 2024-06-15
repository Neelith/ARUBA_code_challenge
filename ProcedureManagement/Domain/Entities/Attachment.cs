using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Attachment : BaseAuditableEntity
    {
        public int ProcedureId { get; internal init; }

        private byte[] _content;

        public byte[] Content 
        { 
            get => _content; 
            internal set => SetContent(value);
        }

        private void SetContent(byte[] value)
        {
            this.LastModifiedOn = DateTime.UtcNow;
            this._content = value;
        }

        public static Attachment Create(int procedureId, byte[] content)
        {
            return new Attachment(procedureId, content);
        }

        private Attachment(int procedureId, byte[] content)
        {
            this.ProcedureId = procedureId;
            this._content = content;
            this.CreatedOn = DateTime.UtcNow;
        }
    }
}

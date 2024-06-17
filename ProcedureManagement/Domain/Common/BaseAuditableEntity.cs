namespace Domain.Common
{
    public abstract class BaseAuditableEntity : BaseEntity
    {
        public DateTimeOffset CreatedOn { get; internal init; }

        public DateTimeOffset? LastModifiedOn { get; internal set; }
    }
}

using Application.Commons.Constants;
using Application.Commons.Interfaces;
using Application.Services.FileReaderService;
using Domain.Common.Exceptions;
using Domain.Entities;
using Domain.Enum;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Procedures.Commands
{
    public record UpdateProcedureCommand(int procedureId, ProcedureStatus? newStatus, int? attachmentId, IFormFile? newAttachment) : IRequest;

    public class UpdateProcedureCommandHandler : IRequestHandler<UpdateProcedureCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileReaderService _fileReaderService;

        public UpdateProcedureCommandHandler(IApplicationDbContext context, IFileReaderService fileReaderService)
        {
            _context = context;
            _fileReaderService = fileReaderService;
        }

        public async Task Handle(UpdateProcedureCommand request, CancellationToken cancellationToken)
        {
            if (request.newStatus is null && request.newAttachment is null && request.attachmentId is null)
            {
                throw new BadRequestException(ApplicationErrors.BadRequest);
            }

            Procedure? entity = await _context.Procedures
                .Include(procedure => procedure.Attachments)
                .FirstOrDefaultAsync(procedure => procedure.Id == request.procedureId, cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException(request.procedureId.ToString(), nameof(Procedure));
            }

            bool hasNewAttachment = request.newAttachment is not null;
            if (hasNewAttachment)
            {
                byte[] content = await _fileReaderService.ReadFileContentAsync(request.newAttachment!);
                entity.AddOrUpdateAttachment(content, request.newAttachment!.FileName, request.attachmentId);
            }

            bool hasNewStatus = request.newStatus.HasValue;
            if (hasNewStatus)
            {
                entity.CurrentProcedureStatus = request.newStatus!.Value;
            }

            entity.AddDomainEvent(new ProcedureUpdatedEvent(entity));

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

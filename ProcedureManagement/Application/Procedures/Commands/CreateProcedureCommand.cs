using Application.Commons.Interfaces;
using Application.Services.FileReaderService;
using Domain.Entities;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Procedures.Commands
{
    public record CreateProcedureCommand(IFormFile? attachment) : IRequest<Procedure>;

    public class CreateProcedureCommandHandler : IRequestHandler<CreateProcedureCommand, Procedure>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileReaderService _fileReaderService;

        public CreateProcedureCommandHandler(IApplicationDbContext context, IFileReaderService fileReaderService)
        {
            _context = context;
            _fileReaderService = fileReaderService;
        }

        public async Task<Procedure> Handle(CreateProcedureCommand request, CancellationToken cancellationToken)
        {
            var entity = Procedure.Create();

            bool isFileUploaded = request.attachment is not null ? true : false;

            if (isFileUploaded)
            {
                byte[] content = await _fileReaderService.ReadFileContentAsync(request.attachment!);
                entity.AddOrUpdateAttachment(content, request.attachment!.FileName);
            }

            entity.AddDomainEvent(new ProcedureCreatedEvent(entity));

            _context.Procedures.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}

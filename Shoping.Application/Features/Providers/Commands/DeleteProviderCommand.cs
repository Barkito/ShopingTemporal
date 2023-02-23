using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;

namespace Shoping.Application.Features.Providers.Commands
{
    public class DeleteProviderCommand : IRequest
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string RUC { get; set; } = default!;
        public int Number { get; set; }
        public string Email { get; set; } = default!;
        public string Direction { get; set; } = default!;
    }

    public class DeleteProviderCommandHandler : IRequestHandler<DeleteProviderCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProviderCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteProviderCommand request, CancellationToken cancellationToken)
        {
            var ProviderId = request.Id.FromHashId();
            var Provider = await _unitOfWork.Provider.GetAsync(P => P.Id == ProviderId);
            _unitOfWork.Provider.Remove(Provider);

            if (Provider is null)
            {
                throw new NotFoundException();
            }

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }

    public class DeleteProviderValidator : AbstractValidator<DeleteProviderCommand>
    {
        public DeleteProviderValidator()
        {
            RuleFor(r => r.Id).NotNull().NotEmpty();
        }
    }
}

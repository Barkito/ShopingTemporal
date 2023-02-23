using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;

namespace Shoping.Application.Features.Providers.Commands;

public class UpdateProviderCommand : IRequest
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string RUC { get; set; } = default!;
    public int Number { get; set; }
    public string Email { get; set; } = default!;
    public string Direction { get; set; } = default!;
}

public class UpdateProviderCommandHandler : IRequestHandler<UpdateProviderCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProviderCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateProviderCommand request, CancellationToken cancellationToken)
    {
        var ProviderId = request.Id.FromHashId();
        var Provider = await _unitOfWork.Provider.GetAsync(P => P.Id == ProviderId);

        if (Provider is null)
        {
            throw new NotFoundException();
        }

        Provider.Name = request.Name;
        Provider.RUC = request.RUC;
        Provider.Number = request.Number;
        Provider.Email = request.Email;
        Provider.Direction = request.Direction;

    await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}

public class UpdateProviderValidator : AbstractValidator<UpdateProviderCommand>
{
    public UpdateProviderValidator()
    {
        RuleFor(r => r.Id).NotNull().NotEmpty();
        RuleFor(r => r.Name).NotNull();
        RuleFor(r => r.RUC).NotNull();
        RuleFor(r => r.Number).NotNull();
        RuleFor(r => r.Email).NotNull();
        RuleFor(r => r.Direction).NotNull();
    }
}
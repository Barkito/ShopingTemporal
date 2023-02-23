using MediatR;
using Shoping.Domain.Entities;
using AutoMapper;
using FluentValidation;
using Shoping.Application.Common.Interfaces;

namespace Shoping.Application.Features.Providers.Commands;

public class CreateProviderCommand : IRequest
{
    public string Name { get; set; } = default!;
    public string RUC { get; set; } = default!;
    public int Number { get; set; }
    public string Email { get; set; } = default!;
    public string Direction { get; set; } = default!;
}

public class CreateProviderCommandHandler : IRequestHandler<CreateProviderCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProviderCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateProviderCommand request, CancellationToken cancellationToken)
    {
        var newProvider = _mapper.Map<Provider>(request);

        _unitOfWork.Provider.Add(newProvider);
        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}
public class CreateProviderCommandMapper : Profile
{
    public CreateProviderCommandMapper() =>
        CreateMap<CreateProviderCommand, Provider>();
}

public class CreateProviderValidator : AbstractValidator<CreateProviderCommand>
{
    public CreateProviderValidator()
    {
        RuleFor(r => r.Name).NotNull();
        RuleFor(r => r.RUC).NotNull();
        RuleFor(r => r.Number).NotNull();
        RuleFor(r => r.Email).NotNull();
        RuleFor(r => r.Direction).NotNull();
    }
}

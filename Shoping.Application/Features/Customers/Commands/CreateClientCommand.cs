using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Application.Features.Customers.Commands;
public class CreateClientCommand : IRequest
{
    public string Name { get; set; } = default!;
    public string Ruc { get; set; } = default!;
    public string Number { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Direction { get; set; } = default!;
}

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateClientCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)

    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {

        var newClient = _mapper.Map<Client>(request);

        this._unitOfWork.Client.Add(newClient);
        await this._unitOfWork.CommitAsync();

        return Unit.Value;
    }
}

public class CreateClientCommandMapper : Profile
{
    public CreateClientCommandMapper() =>
        CreateMap<CreateClientCommand, Client>();
}

public class CreateClientValidator : AbstractValidator<CreateClientCommand>
{
    public CreateClientValidator()
    {
        RuleFor(r => r.Name).NotNull();
        RuleFor(r => r.Ruc).NotNull();
        RuleFor(r => r.Number).NotNull();
        RuleFor(r => r.Email).NotNull();
        RuleFor(r => r.Direction).NotNull();

    }
}

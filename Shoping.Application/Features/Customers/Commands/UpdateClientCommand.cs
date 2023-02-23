using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Application.Features.Customers.Commands;

public class UpdateClientCommand : IRequest
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Ruc { get; set; } = default!;
    public string Number { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Direction { get; set; } = default!;
}

public class UpdateClientCommandHandler : IRequest<UpdateClientCommand>
{
    public readonly IMapper _mapper;
    public readonly IUnitOfWork _unitOfWork;

    public UpdateClientCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var ClientId = request.Id.FromHashId();
        var Client = await _unitOfWork.Client.GetAsync(C => C.Id == ClientId);

        if (Client == null)
        {
            throw new NotFoundException();
        }

        Client.Name = request.Name;
        await this._unitOfWork.CommitAsync();

        return Unit.Value;

    }
}

public class UpdateClientValidator : AbstractValidator<UpdateClientCommand>
{
    public UpdateClientValidator()
    {
        RuleFor(r => r.Id).NotNull().NotEmpty();
        RuleFor(r => r.Name).NotNull();
        RuleFor(r => r.Ruc).NotNull();
        RuleFor(r => r.Number).NotNull();
        RuleFor(r => r.Email).NotNull();
        RuleFor(r => r.Direction).NotNull();
    }
}


using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;

namespace Shoping.Application.Features.Sales.Commands;

public class UpdateSaleCommand : IRequest
{
    public string Id { get; set; } = default!;
    public string Number { get; set; } = default!;
    public DateTime Date { get; set; }
    public int Client_Id { get; set; }
}

public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSaleCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var SaleId = request.Id.FromHashId();
        var Sale = await _unitOfWork.Sale.GetAsync(S => S.Id==SaleId);

        if (Sale is null)
        {
            throw new NotFoundException();
        }

        Sale.Number = request.Number;
        Sale.Date = request.Date;
        Sale.Client_Id = request.Client_Id;

        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}

public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
{
    public UpdateSaleValidator()
    {
        RuleFor(r => r.Id).NotNull().NotEmpty();
        RuleFor(r => r.Number).NotNull();
        RuleFor(r => r.Date).NotNull();
        RuleFor(r => r.Client_Id).NotNull().GreaterThan(0);
    }
}

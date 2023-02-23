using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;

namespace Shoping.Application.Features.Purchases.Commands;

public class UpdatePurchaseCommand : IRequest
{
    public string Id { get; set; } = default!;
    public string Number { get; set; } = default!;
    public DateTime Date { get; set; }
    public int Provider_Id { get; set; }
    public int Created_By { get; set; }
    public DateTime Created_At { get; set; }
    public int Modified_By { get; set; }
    public DateTime Modified_At { get; set; }
}

public class UpdatePurchaseCommandHandler : IRequestHandler<UpdatePurchaseCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePurchaseCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdatePurchaseCommand request, CancellationToken cancellationToken)
    {
        var PurchaseId = request.Id.FromHashId();
        var Purchase = await _unitOfWork.Purchase.GetAsync(P => P.Id == PurchaseId);

        if (Purchase is null)
        {
            throw new NotFoundException();
        }

        Purchase.Number = request.Number;
        Purchase.Date = request.Date;
        Purchase.Provider_Id = request.Provider_Id;
        Purchase.Created_By = request.Created_By;
        Purchase.Created_At = request.Created_At;
        Purchase.Modified_By = request.Modified_By;
        Purchase.Modified_At = request.Modified_At;

        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}

public class UpdatePurchaseValidator : AbstractValidator<UpdatePurchaseCommand>
{
    public UpdatePurchaseValidator()
    {
        RuleFor(r => r.Id).NotNull().NotEmpty();
        RuleFor(r => r.Number).NotNull();
        RuleFor(r => r.Date).NotNull();
        RuleFor(r => r.Provider_Id).NotNull().GreaterThan(0);
        RuleFor(r => r.Created_By).NotNull().GreaterThan(0);
        RuleFor(r => r.Created_At).NotNull();
        RuleFor(r => r.Modified_By).NotNull().GreaterThan(0);
        RuleFor(r => r.Modified_At).NotNull();
    }
}
using MediatR;
using Shoping.Domain.Entities;
using AutoMapper;
using FluentValidation;
using Shoping.Application.Common.Interfaces;
using Shoping.Application.DTOs;

namespace Shoping.Application.Features.Purchases.Commands;

public class CreatePurchaseCommand : IRequest
{
    public string Number { get; set; } = default!;
    public DateTime Date { get; set; }
    public int Provider_Id { get; set; }
    public List<PurchaseDetailCreateDTO> PurchaseDetails { get; set; } = new List<PurchaseDetailCreateDTO>();
}

public class CreatePurchaseCommandHandler : IRequestHandler<CreatePurchaseCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePurchaseCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreatePurchaseCommand request, CancellationToken cancellationToken)
    {
        var newPurchase = _mapper.Map<Purchase>(request);
        newPurchase.Number = Guid.NewGuid().ToString();
        _unitOfWork.Purchase.Add(newPurchase);

        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}
public class CreatePurchaseCommandMapper : Profile
{
    public CreatePurchaseCommandMapper()
    {
        CreateMap<CreatePurchaseCommand, Purchase>();
        CreateMap<PurchaseDetailCreateDTO, PurchaseDetail>();
    }
}

public class CreatePurchaseValidator : AbstractValidator<CreatePurchaseCommand>
{
    public CreatePurchaseValidator()
    {
        RuleFor(r => r.Number).NotNull();
        RuleFor(r => r.Date).NotNull();
        RuleFor(r => r.Provider_Id).NotNull();
        RuleFor(r => r.PurchaseDetails.Count).NotEqual(0);

    }
}

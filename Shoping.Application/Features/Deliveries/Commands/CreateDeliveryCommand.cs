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

namespace Shoping.Application.Features.Deliveries.Commands;
public class CreateDeliveryCommand : IRequest
{
    public string UserilId { get; set; } = default!;
    public string DeliveryDetailId { get; set; } = default!;
    public string SalelId { get; set; } = default!;
}

public class CreateDeliveryCommandHandler : IRequestHandler<CreateDeliveryCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDeliveryCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

     public async Task<Unit> Handle(CreateDeliveryCommand request , CancellationToken cancellationToken)
     {
        var newDelivery = _mapper.Map<Delivery>(request);  
        this._unitOfWork.Delivery.Add(newDelivery);
        await this._unitOfWork.CommitAsync();
        return Unit.Value;
     }

}

public class CreateDeliveryCommandMapper : Profile
{
    public CreateDeliveryCommandMapper() =>
        CreateMap<CreateDeliveryCommand,Delivery>();
}

public class CreateDeliveryValidator : AbstractValidator<CreateDeliveryCommand>
{
    public CreateDeliveryValidator()
    {
        RuleFor(r => r.UserilId).NotNull();
        RuleFor(r => r.DeliveryDetailId).NotNull();
        RuleFor(r => r.SalelId).NotNull();
    }
}

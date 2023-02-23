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

namespace Shoping.Application.Features.DeliveiesDetails.Commands;
public class CreateDeliveryDetailCommand
{
    public int Price { get; set; }
    public int Quality { get; set; }
}

public class CreateDeliveryDetailCommandHandler : IRequest<CreateDeliveryDetailCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDeliveryDetailCommandHandler(IMapper mapper , IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handler(CreateDeliveryDetailCommand request , CancellationToken cancellationToken)
    {
        var newDeliveryDetail = _mapper.Map<DeliveryDetail>(request);
        this._unitOfWork.DeliveryDetail.Add(newDeliveryDetail);
        await this._unitOfWork.CommitAsync();
        return Unit.Value;
    }
}

public class CreateDeliveryDetailCommandMapper : Profile
{
    public CreateDeliveryDetailCommandMapper() =>
        CreateMap<CreateDeliveryDetailCommand, DeliveryDetail>();
}

public class CreateDeliveryDetailValidator : AbstractValidator<CreateDeliveryDetailCommand>
{
    public CreateDeliveryDetailValidator() 
    {
        RuleFor(r => r.Price).NotNull();
        RuleFor(r => r.Quality).NotNull();
    }
}


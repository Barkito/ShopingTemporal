using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Application.Features.DeliveiesDetails.Commands;
public class UpdateDeliveryDetailCommand : IRequest 
{
    public string Id { get; set; } = default!;
    public string Price { get; set; } = default!;
    public string Quality { get; set; } = default!;
}

public class UpdateDeliveryDetailCommandHandler : IRequest<UpdateDeliveryDetailCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitofWork;

    public UpdateDeliveryDetailCommandHandler (IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitofWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateDeliveryDetailCommand request , CancellationToken cancellationToken)
    {
        var DeliveryDetailId = request.Id.FromHashId();
        var DeliveryDetail = await _unitofWork.DeliveryDetail.GetAsync(D => D.Id == DeliveryDetailId);

        if (DeliveryDetail is null)
        {
            throw new NotFoundException();
        }

        DeliveryDetail.Price = Convert.ToInt32(request.Price);
        DeliveryDetail.Quality = Convert.ToInt32(request.Quality);

        return Unit.Value;
    }
}

public class UpdateDeliveryDetailValidator : AbstractValidator<UpdateDeliveryDetailCommand>
{
    public UpdateDeliveryDetailValidator()
    {
        RuleFor(r => r.Id).NotNull().NotEmpty();
        RuleFor(r => r.Price).NotNull().NotEmpty();
        RuleFor(r => r.Quality).NotNull().NotEmpty();
    }
}


using AutoMapper;
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

namespace Shoping.Application.Features.Deliveries.Queries;
public class GetDeliveryQuery : IRequest<GetDeliveryQueryResponse>
{
    public string Id { get; set; } = string.Empty;
}

public class GetDeliveryQueryHandler : IRequestHandler<GetDeliveryQuery, GetDeliveryQueryResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetDeliveryQueryHandler (IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }   

    public async Task<GetDeliveryQueryResponse> Handle(GetDeliveryQuery request , CancellationToken cancellationToken)

    {
        var Delivery = _unitOfWork.Delivery.GetAsync(D => D.Id == request.Id.FromHashId() && D.IsDeteleted==false);

        if (Delivery is null)
        {
            throw new NotFoundException(nameof(Delivery), request.Id);
        }

        return _mapper.Map<GetDeliveryQueryResponse>(Delivery);
    }
}

public class GetDeliveryQueryResponse
{
    public string Id { get; set; } = default!;
    public string UserilId { get; set; } = default!;
    public string DeliveryDetailId { get; set; } = default!;
    public string SalelId { get; set; } = default!;
}

public class GetDeliveryProfile : Profile
{
    public GetDeliveryProfile() =>
        CreateMap<Delivery, GetDeliveryQueryResponse>()
        .ForMember(dest =>
        dest.Id,
            opt => opt.MapFrom(mf => mf.Id.ToHashId()));
}

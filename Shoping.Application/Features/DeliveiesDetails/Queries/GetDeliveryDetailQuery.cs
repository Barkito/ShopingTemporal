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

namespace Shoping.Application.Features.DeliveiesDetails.Queries;
public class GetDeliveryDetailQuery : IRequest<GetDeliveryDetailQueryResponse>
{
    public string Id { get; set; } = string.Empty;
}

public class GetDeliveryDetailQueryHandler : IRequestHandler<GetDeliveryDetailQuery ,GetDeliveryDetailQueryResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetDeliveryDetailQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
            _unitOfWork = unitOfWork;

    }

    public async Task<GetDeliveryDetailQueryResponse> Handle(GetDeliveryDetailQuery request , CancellationToken cancellationToken)
    {
        var DeliveryDetail = _unitOfWork.DeliveryDetail.GetAsync(DD => DD.Id == request.Id.FromHashId());
        if (DeliveryDetail is null)
        {
           throw new NotFoundException(nameof(DeliveryDetail), request.Id);
        }

        return _mapper.Map<GetDeliveryDetailQueryResponse>(DeliveryDetail);
    }
}

public class GetDeliveryDetailQueryResponse
{
    public string Id { get; set; } = default!;
    public string Price { get; set; } = default!;
    public string Quality { get; set; } = default!;
}


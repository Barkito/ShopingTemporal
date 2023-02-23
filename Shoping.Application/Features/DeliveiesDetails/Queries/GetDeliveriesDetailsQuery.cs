using AutoMapper;
using MediatR;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shoping.Application.Features.Caterories.Queries.GetCategoriesQueryHandler;
using static Shoping.Application.Features.DeliveiesDetails.Queries.GetDeliveriesDetailsQueryHandler;

namespace Shoping.Application.Features.DeliveiesDetails.Queries;
public class GetDeliveriesDetailsQuery : IRequest<List<GetDeliveriesDetailsQueryResponse>>
{

}

public class GetDeliveriesDetailsQueryHandler : IRequestHandler<GetDeliveriesDetailsQuery, List<GetDeliveriesDetailsQueryResponse>>
{ 
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public  GetDeliveriesDetailsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetDeliveriesDetailsQueryResponse>> Handle(GetDeliveriesDetailsQuery request , CancellationToken cancellationToken)
    {
        var DeliveriesDetails = await _unitOfWork.DeliveryDetail.GetAllAsync();

        return _mapper.Map<List<GetDeliveriesDetailsQueryResponse>>(DeliveriesDetails);
    }

    public class GetDeliveriesDetailsQueryResponse
    {
        public string Id { get; set; } = default!;
        public string Price { get; set; } = default!;
        public string Stock { get; set; } = default!;
    }

    public class GetDeliveriesDetailsQueryProfile : Profile
    {
        public GetDeliveriesDetailsQueryProfile() =>
         CreateMap<DeliveryDetail, GetDeliveriesDetailsQueryResponse>()
           .ForMember(dest =>
               dest.Id,
               opt => opt.MapFrom(mf => mf.Id.ToHashId()));
    }
}
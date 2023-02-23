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
using static Shoping.Application.Features.Deliveries.Queries.GetDeliveriesQueryHandler;

namespace Shoping.Application.Features.Deliveries.Queries;

public class GetDeliveriesQuery : IRequest<List<GetDeliveriesQueryResponse>>
{

}

public class GetDeliveriesQueryHandler : IRequestHandler<GetDeliveriesQuery, List<GetDeliveriesQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uniOfWork;

    public GetDeliveriesQueryHandler (IMapper mapper, IUnitOfWork uniOfWork)
    {
        _mapper = mapper;
        _uniOfWork = uniOfWork;
    }

    public async Task<List<GetDeliveriesQueryResponse>> Handle(GetDeliveriesQuery request , CancellationToken cancellationToken)
    {
        var Deliveries = await _uniOfWork.Delivery.GetAllAsync();
        return _mapper.Map<List<GetDeliveriesQueryResponse>>(Deliveries);
    }
    public class GetDeliveriesQueryResponse
    {
        public string Id { get; set; } = default!;
        public string UserilId { get; set; } = default!;
        public string DeliveryDetailId { get; set; } = default!;
        public string SalelId { get; set; } = default!;
    }

    public class GetDeliveriesQueryProfile : Profile
    {
        public GetDeliveriesQueryProfile() =>
            CreateMap<Delivery, GetDeliveriesQueryResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(mf => mf.Id.ToHashId()));
    }
}

using AutoMapper;
using MediatR;
using Shoping.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shoping.Application.Features.Caterories.Queries;
using Shoping.Domain.Entities;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Exceptions;
using static Shoping.Application.Features.Inventory.Queries.GetInventoriesQueryHandler;

namespace Shoping.Application.Features.Inventory.Queries;

public class GetInventoriesQuery : IRequest<List<GetInventoriesQueryResponse>>
{

}

public class GetInventoriesQueryHandler : IRequestHandler<GetInventoriesQuery, List<GetInventoriesQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;


    public GetInventoriesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetInventoriesQueryResponse>> Handle(GetInventoriesQuery request, CancellationToken cancellationToken)
    {

        var Inventories = await _unitOfWork.Inventary.GetAllAsync(I => I.IsDeteleted == false);
        return _mapper.Map<List<GetInventoriesQueryResponse>>(Inventories);

    }

    public class GetInventoriesQueryResponse
    {
        public string Id { get; set; } = default!;
        public string Price { get; set; } = default!;
        public string Stock { get; set; } = default!;
        public string Product_Id { get; set; } = default!;

    }

    public class GetInventoriesQueryProfile : Profile
    {
        public GetInventoriesQueryProfile() =>
              CreateMap<Inventary, GetInventoriesQueryResponse>()
                .ForMember(dest =>
                    dest.Id,
                    opt => opt.MapFrom(mf => mf.Id.ToHashId()));

    }
}



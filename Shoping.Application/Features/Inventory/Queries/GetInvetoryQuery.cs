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

namespace Shoping.Application.Features.Inventory.Queries;

    public class GetInventoryQuery : IRequest<GetInventoryQueryResponse>
{
    public string Id { get; set; } = string.Empty;
}

public class GetInventoryQueryHandler : IRequestHandler<GetInventoryQuery, GetInventoryQueryResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetInventoryQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<GetInventoryQueryResponse> Handle(GetInventoryQuery request, CancellationToken cancellationToken)
    {

        var Inventary = _unitOfWork.Inventary.GetAsync(I => I.Id == request.Id.FromHashId() && I.IsDeteleted == false);

        if (Inventary is null)
        {
            throw new NotFoundException(nameof(Inventary), request.Id);
        }

        return _mapper.Map<GetInventoryQueryResponse>(Inventary);
    }
}

public class GetInventoryQueryResponse
{
    public string Id { get; set; } = default!;
    public string Price { get; set; } = default!;
    public string Stock { get; set; } = default!;
    public string Product_Id { get; set; } = default!;

}

public class GetInventaryQueryProfile : Profile
{
    public GetInventaryQueryProfile() =>
        CreateMap<Inventary, GetInventoryQueryResponse>()
            .ForMember(dest =>
                dest.Id,
                opt => opt.MapFrom(mf => mf.Id.ToHashId()));

}


using AutoMapper;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain;
using Shoping.Domain.Entities;

namespace Shoping.Application.Features.Purchases.Queries;

public class GetPurchaseQuery : IRequest<GetPurchaseQueryResponse>
{
    public string Id { get; set; } = string.Empty;
}

public class GetPurchaseQueryHandler : IRequestHandler<GetPurchaseQuery, GetPurchaseQueryResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetPurchaseQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<GetPurchaseQueryResponse> Handle(GetPurchaseQuery request, CancellationToken cancellationToken)
    {
        var Purchase = await _unitOfWork.Purchase.GetAsync(p => p.Id==request.Id.FromHashId());

        if (Purchase is null)
        {
            throw new NotFoundException(nameof(Purchase), request.Id);
        }

        return _mapper.Map<GetPurchaseQueryResponse>(Purchase);
    }
}

public class GetPurchaseQueryResponse
{
    public string Id { get; set; } = default!;
    public string Number { get; set; } = default!;
    public DateTime Date { get; set; }
    public int Provider_Id { get; set; }
    public int Created_By { get; set; }
    public DateTime Created_At { get; set; }
    public int Modified_By { get; set; }
    public DateTime Modified_At { get; set; }
}

public class GetPurchaseQueryProfile : Profile
{
    public GetPurchaseQueryProfile() =>
        CreateMap<Purchase, GetPurchaseQueryResponse>()
            .ForMember(dest =>
                dest.Id,
                opt => opt.MapFrom(mf => mf.Id.ToHashId()));

}
using AutoMapper;
using MediatR;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain;
using Shoping.Domain.Entities;
using static Shoping.Application.Features.Purchases.Queries.GetPurchasesQueryHandler;

namespace Shoping.Application.Features.Purchases.Queries;

public class GetPurchasesQuery : IRequest<List<GetPurchasesQueryResponse>>
{

}

public class GetPurchasesQueryHandler : IRequestHandler<GetPurchasesQuery, List<GetPurchasesQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetPurchasesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetPurchasesQueryResponse>> Handle(GetPurchasesQuery request, CancellationToken cancellationToken)
    {
        var Purchases = await _unitOfWork.Purchase.GetAllAsync();
        return _mapper.Map<List<GetPurchasesQueryResponse>>(Purchases);
    }
    
    public class GetPurchasesQueryResponse
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

    public class GetPurchasesQueryProfile : Profile
    {
        public GetPurchasesQueryProfile() =>
            CreateMap<Purchase, GetPurchasesQueryResponse>()
                .ForMember(dest => 
                    dest.Id,
                    opt => opt.MapFrom(mf => mf.Id.ToHashId()));
    }
}
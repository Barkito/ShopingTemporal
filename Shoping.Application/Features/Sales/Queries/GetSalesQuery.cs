using AutoMapper;
using MediatR;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;
using static Shoping.Application.Features.Sales.Queries.GetSalesQueryHandler;

namespace Shoping.Application.Features.Sales.Queries;

public class GetSalesQuery : IRequest<List<GetSalesQueryResponse>>
{

}

public class GetSalesQueryHandler : IRequestHandler<GetSalesQuery, List<GetSalesQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetSalesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetSalesQueryResponse>> Handle(GetSalesQuery request, CancellationToken cancellationToken)
    {
        var Sales = await _unitOfWork.Sale.GetAllAsync();
        return _mapper.Map<List<GetSalesQueryResponse>>(Sales);
    }
    
    public class GetSalesQueryResponse
    {
        public string Id { get; set; } = default!;
        public string Number { get; set; } = default!;
        public DateTime Date { get; set; }
        public int Client_Id { get; set; }
    }

    public class GetSalesQueryProfile : Profile
    {
        public GetSalesQueryProfile() =>
            CreateMap<Sale, GetSalesQueryResponse>()
                .ForMember(dest => 
                    dest.Id,
                    opt => opt.MapFrom(mf => mf.Id.ToHashId()));
    }
}
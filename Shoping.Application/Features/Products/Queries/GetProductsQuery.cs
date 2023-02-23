using AutoMapper;
using MediatR;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;
using static Shoping.Application.Features.Products.Queries.GetProductsQueryHandler;

namespace Shoping.Application.Features.Products.Queries;

public class GetProductsQuery : IRequest<List<GetProductsQueryResponse>>
{

}

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<GetProductsQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetProductsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetProductsQueryResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Product.GetAllAsync(p=>p.IsDeteleted==false);
        return _mapper.Map<List<GetProductsQueryResponse>>(products);
    }
    
    public class GetProductsQueryResponse
    {
        public string Id { get; set; } = default!;
        public int Code { get; set; }
        public string Name { get; set; } = default!;
        public string CategoryName { get; set; }= default!;
        public decimal Price { get; set; }
    }

    public class GetProductsQueryProfile : Profile
    {
        public GetProductsQueryProfile() =>
            CreateMap<Product, GetProductsQueryResponse>()
                .ForMember(dest => 
                    dest.Id,
                    opt => opt.MapFrom(mf => mf.Id.ToHashId()));
    }
}
using AutoMapper;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;

namespace Shoping.Application.Features.Products.Queries;

public class GetProductQuery : IRequest<GetProductQueryResponse>
{
    public string Id { get; set; } = string.Empty;
}

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, GetProductQueryResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetProductQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<GetProductQueryResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var Product = _unitOfWork.Product.GetAsync(p => p.Id==request.Id.FromHashId() && p.IsDeteleted==false);

        if (Product is null)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }

        return _mapper.Map<GetProductQueryResponse>(Product);
    }
}

public class GetProductQueryResponse
{
    public string Id { get; set; } = default!;
    public int Code { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Category_Id { get; set; }
    public decimal Price { get; set; }
}

public class GetProductQueryProfile : Profile
{
    public GetProductQueryProfile() =>
        CreateMap<Product, GetProductQueryResponse>()
            .ForMember(dest =>
                dest.Id,
                opt => opt.MapFrom(mf => mf.Id.ToHashId()));

}
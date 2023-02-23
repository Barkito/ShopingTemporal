using AutoMapper;
using MediatR;
using Shoping.Application.Common.Helpers;

using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;
using static Shoping.Application.Features.Caterories.Queries.GetCategoriesQueryHandler;

namespace Shoping.Application.Features.Caterories.Queries;

public class GetCategoriesQuery : IRequest<List<GetCategoriesQueryResponse>>
{

}

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<GetCategoriesQueryResponse>>
{
  private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
  

   public GetCategoriesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

   public async Task<List<GetCategoriesQueryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken){

    var  Categories= await _unitOfWork.Category.GetAllAsync();
      return _mapper.Map<List<GetCategoriesQueryResponse>>(Categories);

}

public class GetCategoriesQueryResponse
{
   public string Id { get; set; } = default!;
   public string Name { get; set; } = default!;

}

public class GetCategoriesQueryProfile : Profile
{
   public GetCategoriesQueryProfile() =>
         CreateMap<Category, GetCategoriesQueryResponse>()
           .ForMember(dest =>
               dest.Id,
               opt => opt.MapFrom(mf => mf.Id.ToHashId()));

}
}
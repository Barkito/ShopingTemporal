using AutoMapper;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;

namespace Shoping.Application.Features.Categories.Queries;

public class GetCategoryQuery : IRequest<GetCategoryQueryResponse>
{
   public string Id { get; set; }=string.Empty;
}

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, GetCategoryQueryResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

   public GetCategoryQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
   public async Task<GetCategoryQueryResponse> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
   {
    
             var Category = _unitOfWork.Category.GetAsync( c=>c.Id==request.Id.FromHashId());

       if (Category is null)
       {
           throw new NotFoundException(nameof(Category), request.Id);
       }

       return _mapper.Map<GetCategoryQueryResponse>(Category);
   }
}

public class GetCategoryQueryResponse
{
   public string Id { get; set; } = default!;
   public string Name { get; set; } = default!;

}

public class GetCategoryQueryProfile : Profile
{
   public GetCategoryQueryProfile() =>
       CreateMap<Category, GetCategoryQueryResponse>()
           .ForMember(dest =>
               dest.Id,
               opt => opt.MapFrom(mf => mf.Id.ToHashId()));

}
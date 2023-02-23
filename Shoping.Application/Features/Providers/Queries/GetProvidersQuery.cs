using AutoMapper;
using MediatR;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;
using static Shoping.Application.Features.Providers.Queries.GetProvidersQueryHandler;

namespace Shoping.Application.Features.Providers.Queries;

public class GetProvidersQuery : IRequest<List<GetProvidersQueryResponse>>
{

}

public class GetProvidersQueryHandler : IRequestHandler<GetProvidersQuery, List<GetProvidersQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetProvidersQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetProvidersQueryResponse>> Handle(GetProvidersQuery request, CancellationToken cancellationToken)
    {
        var Providers = await _unitOfWork.Provider.GetAllAsync();
        return _mapper.Map<List<GetProvidersQueryResponse>>(Providers);
    }
    
    public class GetProvidersQueryResponse
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string RUC { get; set; } = default!;
        public int Number { get; set; }
        public string Email { get; set; } = default!;
        public string Direction { get; set; } = default!;
    }

    public class GetProvidersQueryProfile : Profile
    {
        public GetProvidersQueryProfile() =>
            CreateMap<Provider, GetProvidersQueryResponse>()
                .ForMember(dest => 
                    dest.Id,
                    opt => opt.MapFrom(mf => mf.Id.ToHashId()));
    }
}
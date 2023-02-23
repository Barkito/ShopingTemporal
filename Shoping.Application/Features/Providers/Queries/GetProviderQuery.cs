using AutoMapper;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;

namespace Shoping.Application.Features.Providers.Queries;

public class GetProviderQuery : IRequest<GetProviderQueryResponse>
{
    public string Id { get; set; } = string.Empty;
}

public class GetProviderQueryHandler : IRequestHandler<GetProviderQuery, GetProviderQueryResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetProviderQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<GetProviderQueryResponse> Handle(GetProviderQuery request, CancellationToken cancellationToken)
    {
        var Provider = await _unitOfWork.Provider.GetAsync(p => p.Id==request.Id.FromHashId());

        if (Provider is null)
        {
            throw new NotFoundException(nameof(Provider), request.Id);
        }

        return _mapper.Map<GetProviderQueryResponse>(Provider);
    }
}

public class GetProviderQueryResponse
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string RUC { get; set; } = default!;
    public int Number { get; set; }
    public string Email { get; set; } = default!;
    public string Direction { get; set; } = default!;
}

public class GetProviderQueryProfile : Profile
{
    public GetProviderQueryProfile() =>
        CreateMap<Provider, GetProviderQueryResponse>()
            .ForMember(dest =>
                dest.Id,
                opt => opt.MapFrom(mf => mf.Id.ToHashId()));

}
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

namespace Shoping.Application.Features.Customers.Queries;
public class GetClientyQuery : IRequest<GetClientyQueryResponse>
{
    public string Id { get; set; } = default!;
}

public class GetClientyQueryHandler : IRequestHandler<GetClientyQuery, GetClientyQueryResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public  GetClientyQueryHandler (IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }   

    public async Task<GetClientyQueryResponse> Handle(GetClientyQuery request, CancellationToken cancellationToken)
    {
        var Clienty = _unitOfWork.Client.GetAsync(C => C.Id == request.Id.FromHashId());
        if(Clienty is null)
        {
            throw new NotFoundException(nameof(Category), request.Id);
        }
        return _mapper.Map<GetClientyQueryResponse>(Clienty);
    }
}
public class GetClientyQueryResponse
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Ruc { get; set; } = default!;
    public string Number { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Direction { get; set; } = default!;
}

public class GetClientyQueryProfile :Profile
{
    public GetClientyQueryProfile() =>
        CreateMap<Client, GetClientyQueryResponse>()
        .ForMember(dest =>
               dest.Id,
               opt => opt.MapFrom(mf => mf.Id.ToHashId()));
}


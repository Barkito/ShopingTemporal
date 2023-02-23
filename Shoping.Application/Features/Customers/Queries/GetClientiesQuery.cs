using AutoMapper;
using MediatR;
using Shoping.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shoping.Application.Features.Customers.Queries.GetClientiesQueryHadler;

namespace Shoping.Application.Features.Customers.Queries;

public class GetClientiesQuery : IRequest<List<GetClientiesQueryHadler.GetClientiesQueryResponse>>
{

}

public class GetClientiesQueryHadler : IRequestHandler<GetClientiesQuery,List<GetClientiesQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetClientiesQueryHadler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetClientiesQueryResponse>> Handle(GetClientiesQuery request, CancellationToken cancellationToken)
    {

        var Clienties = await _unitOfWork.Client.GetAllAsync();
        return _mapper.Map<List<GetClientiesQueryResponse>>(Clienties);
    }

    public class GetClientiesQueryResponse
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Ruc { get; set; } = default!;
        public string Number { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Direction { get; set; } = default!;

    }
}


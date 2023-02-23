using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Interfaces;
using Shoping.Application.Features.Categories.Commands;
using Shoping.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Application.Features.Inventory.Commands; 
public class CreateInventoryCommand : IRequest
{
    public string Price { get; set; } = default!;
    public string Stock { get; set; } = default!;
    public int Product_Id { get; set; } = default!;
}

public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand>
{
    //  private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateInventoryCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }


    public async Task<Unit> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
    {
        var newInventary = _mapper.Map<Inventary>(request);

        this._unitOfWork.Inventary.Add(newInventary);
        await this._unitOfWork.CommitAsync();

        return Unit.Value;
    }
}
public class CreateInventoryCommandHandlerMapper : Profile
{
    public CreateInventoryCommandHandlerMapper() =>
        CreateMap<CreateInventoryCommand, Inventary>();
}

public class CreateInventoryValidator : AbstractValidator<CreateInventoryCommand>
{
    public CreateInventoryValidator()
    {
        RuleFor(r => r.Price).NotNull();
        RuleFor(r => r.Stock).NotNull();
        RuleFor(r => r.Product_Id).NotNull();

    }
}



using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Application.Features.Categories.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Application.Features.Inventory.Commands;

public class UpdateInventoryCommand : IRequest
{
    public string Id { get; set; } = default!;
    public string Price { get; set; } = default!;
    public string Stock { get; set; } = default!;
    public string Product_Id { get; set; } = default!;
}

public class UpdateInventoryCommandHandler : IRequestHandler<UpdateInventoryCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateInventoryCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
    {
        var InventaryId = request.Id.FromHashId();
        var Inventary = await _unitOfWork.Inventary.GetAsync(C => C.Id == InventaryId);

        if (Inventary is null)
        {
            throw new NotFoundException();
        }

        Inventary.Price = Convert.ToInt32(request.Price);
        Inventary.Stock = Convert.ToInt32(request.Stock);
        Inventary.Product_Id = Convert.ToInt32(request.Product_Id);
        await this._unitOfWork.CommitAsync();

        return Unit.Value;
    }

}

public class UpdateInventoryValidator : AbstractValidator<UpdateInventoryCommand>
{
    public UpdateInventoryValidator()
    {
        RuleFor(r => r.Id).NotNull().NotEmpty();
        RuleFor(r => r.Price).NotNull().NotEmpty();
        RuleFor(r => r.Stock).NotNull().NotEmpty();
        RuleFor(r => r.Product_Id).NotNull().NotEmpty();

    }
}
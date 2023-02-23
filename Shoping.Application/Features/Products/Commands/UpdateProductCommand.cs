using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Attributes;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;

namespace Shoping.Application.Features.Products.Commands;

public class UpdateProductCommand : IRequest
{
    public string Id { get; set; } = default!;
    public int Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int Category_Id { get; set; } = default!;
    public decimal Price { get; set; } = default!;
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var ProductId = request.Id.FromHashId();
        var Product = await _unitOfWork.Product.GetAsync(P => P.Id==ProductId);

        if (Product is null)
        {
            throw new NotFoundException();
        }

        Product.Code = request.Code;
        Product.Name = request.Name;
        Product.Category_Id = request.Category_Id;
        Product.Price = request.Price;

        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(r => r.Id).NotNull().NotEmpty();
        RuleFor(r => r.Code).NotNull();
        RuleFor(r => r.Name).NotNull();
        RuleFor(r => r.Category_Id).NotNull();
        RuleFor(r => r.Price).NotNull().GreaterThan(0);
    }
}

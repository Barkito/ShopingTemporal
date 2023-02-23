using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Attributes;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;

namespace Shoping.Application.Features.Products.Commands;

public class CreateProductCommand : IRequest
{
    public int Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public int Category_Id { get; set; } = default!;
    public decimal Price { get; set; } = default!;
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }


    public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var newProduct = _mapper.Map<Product>(request);

        _unitOfWork.Product.Add(newProduct);
        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}
public class CreateProductCommandMapper : Profile
{
    public CreateProductCommandMapper() =>
        CreateMap<CreateProductCommand, Product>();
}

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(r => r.Code).NotNull();
        RuleFor(r => r.Name).NotNull();
        RuleFor(r => r.Category_Id).NotNull();
        RuleFor(r => r.Price).NotNull().GreaterThan(0);
    }
}

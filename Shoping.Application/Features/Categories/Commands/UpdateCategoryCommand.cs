using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Attributes;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;

namespace Shoping.Application.Features.Categories.Commands;


public class UpdateCategoryCommand : IRequest
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;

}

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var CategoryId = request.Id.FromHashId();
        var Category =await _unitOfWork.Category.GetAsync(C=>C.Id==CategoryId);

        if (Category is null)
        {
            throw new NotFoundException();
        }

        Category.Name = request.Name;
        await this._unitOfWork.CommitAsync();

        return Unit.Value;
    }
}

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryValidator()
    {
        RuleFor(r => r.Id).NotNull().NotEmpty();
        RuleFor(r => r.Name).NotNull().NotEmpty();
       
    }
}

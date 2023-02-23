using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Attributes;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain.Entities;

namespace Shoping.Application.Features.Categories.Commands;


public class CreateCategoryCommand : IRequest
{
    public string Name { get; set; } = default!;
   
}


public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
{
 //  private readonly MyAppDbContext _context;
  private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

   public CreateCategoryCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }


   public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
   {
       var newCategory = _mapper.Map<Category>(request);

        this._unitOfWork.Category.Add(newCategory);
         await this._unitOfWork.CommitAsync();

       return Unit.Value;
   }
}
public class CreateCategoryCommandMapper : Profile
{
   public CreateCategoryCommandMapper() =>
       CreateMap<CreateCategoryCommand, Category>();
}

public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
{
   public CreateCategoryValidator()
   {
       RuleFor(r => r.Name).NotNull();
     
   }
}

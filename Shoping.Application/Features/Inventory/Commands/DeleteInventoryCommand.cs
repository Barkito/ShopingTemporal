using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Application.Features.Inventory.Commands
{
    public class DeleteInventoryCommand : IRequest
    {
        public string Id { get; set; } = default!;
        public string Price { get; set; } = default!;
        public string Stock { get; set; } = default!; 
        public string Product_Id { get; set; } = default!;
    }

    public class DeleteInventoryCommandHandler : IRequestHandler<DeleteInventoryCommand>
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteInventoryCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteInventoryCommand request, CancellationToken cancellationToken)
        {
            var InventoryId = request.Id.FromHashId();
            var Inventory = await _unitOfWork.Inventary.GetAsync(C => C.Id == InventoryId);
            //_unitOfWork.Inventary.Remove(Inventory);

            if (Inventory is null)
            {
                throw new NotFoundException();
            }
            Inventory.IsDeteleted = true;
            await this._unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }

    public class DeleteInventoryValidator : AbstractValidator<DeleteInventoryCommand>
    {
        public DeleteInventoryValidator()
        {
            RuleFor(r => r.Id).NotNull().NotEmpty();

        }
    }
}

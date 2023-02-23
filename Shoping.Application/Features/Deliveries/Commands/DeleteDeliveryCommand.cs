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

namespace Shoping.Application.Features.Deliveries.Commands
{
    public class DeleteDeliveryCommand : IRequest 
    {
        public string Id { get; set; } = default!;
    }

    public class DeleteDeliveryCommandHandler : IRequestHandler<DeleteDeliveryCommand> 
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDeliveryCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteDeliveryCommand request, CancellationToken cancellationToken)
        {
            var DeliveryId = request.Id.FromHashId();
            var Delivery = await _unitOfWork.Delivery.GetAsync(D => D.Id == DeliveryId);

            //_unitOfWork.Delivery.Remove(Delivery);

            if (Delivery is null)
            {
                throw new NotFoundException();
            }
            Delivery.IsDeteleted = true;

            await this._unitOfWork.CommitAsync();
            return Unit.Value;
        
        }
    }

    public class DeleteDeliveryValidator : AbstractValidator<DeleteDeliveryCommand> 
    {
        public DeleteDeliveryValidator() 
        {
            RuleFor(r => r.Id).NotNull().NotEmpty();
        }
    }
}

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

namespace Shoping.Application.Features.DeliveiesDetails.Commands
{
    public class DeleteDeliveryDetailCommand : IRequest
    {
        public string Id { get; set; } = default!;
        public string Price { get; set; } = default!;
        public string Stock { get; set; } = default!;
    }
     
    public class DeleteDeliveryDetailCommandHandler : IRequestHandler<DeleteDeliveryDetailCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDeliveryDetailCommandHandler (IMapper mapper , IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }

        public async Task<Unit> Handle(DeleteDeliveryDetailCommand request , CancellationToken cancellationToken)
        {
            var DeliveryDetailId = request.Id.FromHashId();
            var DeliveryDetail = await _unitOfWork.DeliveryDetail.GetAsync(DD => DD.Id == DeliveryDetailId);
            _unitOfWork.DeliveryDetail.Remove(DeliveryDetail);

            if (DeliveryDetail is null)
            {
                throw new NotFoundException();
            }
            await this._unitOfWork.CommitAsync();
            return Unit.Value;
        }
    }

    public class DeliveryDetailValidator : AbstractValidator<DeleteDeliveryDetailCommand>
    {
        public DeliveryDetailValidator()
        {
            RuleFor(r => r.Id).NotNull().NotEmpty();
        }
    }
}

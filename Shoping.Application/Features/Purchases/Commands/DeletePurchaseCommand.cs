using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;

namespace Shoping.Application.Features.Purchases.Commands
{
    public class DeletePurchaseCommand : IRequest
    {
        public string Id { get; set; } = default!;
        public string Number { get; set; } = default!;
        public DateTime Date { get; set; }
        public int Client_Id { get; set; }
    }

    public class DeletePurchaseCommandHandler : IRequestHandler<DeletePurchaseCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePurchaseCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeletePurchaseCommand request, CancellationToken cancellationToken)
        {
            var PurchaseId = request.Id.FromHashId();
            var Purchase = await _unitOfWork.Purchase.GetAsync(P => P.Id == PurchaseId);
            _unitOfWork.Purchase.Remove(Purchase);

            if (Purchase is null)
            {
                throw new NotFoundException();
            }

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }

    public class DeletePurchaseValidator : AbstractValidator<DeletePurchaseCommand>
    {
        public DeletePurchaseValidator()
        {
            RuleFor(r => r.Id).NotNull().NotEmpty();
        }
    }
}

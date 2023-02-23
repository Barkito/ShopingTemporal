using AutoMapper;
using FluentValidation;
using MediatR;
using Shoping.Application.Common.Exceptions;
using Shoping.Application.Common.Helpers;
using Shoping.Application.Common.Interfaces;

namespace Shoping.Application.Features.Sales.Commands
{
    public class DeleteSaleCommand : IRequest
    {
        public string Id { get; set; } = default!;
        public string Number { get; set; } = default!;
        public DateTime Date { get; set; }
        public int Client_Id { get; set; }
    }

    public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSaleCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            var SaleId = request.Id.FromHashId();
            var Sale = await _unitOfWork.Sale.GetAsync(P => P.Id == SaleId);
            _unitOfWork.Sale.Remove(Sale);

            if (Sale is null)
            {
                throw new NotFoundException();
            }

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }

    public class DeleteSaleValidator : AbstractValidator<DeleteSaleCommand>
    {
        public DeleteSaleValidator()
        {
            RuleFor(r => r.Id).NotNull().NotEmpty();
        }
    }
}

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

namespace Shoping.Application.Features.Customers.Commands
{
    public class DeleteClientCommand : IRequest
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Ruc { get; set; } = default!;
        public string Number { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Direction { get; set; } = default!;

    }

    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand>
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteClientCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var ClientId = request.Id.FromHashId();
            var Client = await _unitOfWork.Client.GetAsync(C => C.Id == ClientId);
            _unitOfWork.Client.Remove(Client);

            if (Client is null)
            {
                throw new NotFoundException();
            }

            await this._unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }

    public class DeleteClientValidator : AbstractValidator<DeleteClientCommand>
    {
        public DeleteClientValidator()
        {
            RuleFor(r => r.Id).NotNull().NotEmpty();

        }
    }
}

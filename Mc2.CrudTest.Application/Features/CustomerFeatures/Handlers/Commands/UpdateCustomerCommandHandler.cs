using AutoMapper;
using Mc2.CrudTest.Application.DTOs.CustomerDTO.Validators;
using Mc2.CrudTest.Application.Exceptions;
using Mc2.CrudTest.Application.Features.CustomerFeatures.Requests.Commands;
using Mc2.CrudTest.Application.Repository;
using Mc2.CrudTest.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Features.CustomerFeatures.Handlers.Commands
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCustomerValidator();
            var validationResult = await validator.ValidateAsync(request.UpdateCustomerDTO);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var customer = await _unitOfWork.CustomerRepository.Get(request.UpdateCustomerDTO.Id);

            if (customer is null)
                throw new NotFoundException(nameof(customer), request.UpdateCustomerDTO.Id);

            _mapper.Map(request.UpdateCustomerDTO, customer);

            await _unitOfWork.CustomerRepository.Update(customer);
            await _unitOfWork.Save();

            return Unit.Value;
       
        }
    }
}

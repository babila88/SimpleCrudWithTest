using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Mc2.CrudTest.Application.DTOs.CustomerDTO.Validators;
using Mc2.CrudTest.Application.Features.CustomerFeatures.Requests.Commands;
using Mc2.CrudTest.Application.Repository;
using Mc2.CrudTest.Application.Responses;
using Mc2.CrudTest.Domain;
using MediatR;

namespace Mc2.CrudTest.Application.Features.CustomerFeatures.Handlers.Commands
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateCustomerValidator();
            var validationResult = await validator.ValidateAsync(request.CreateCustomerDTO);

            if (validationResult.IsValid == false)
            {
                return new BaseCommandResponse()
                {
                    Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList(),
                    IsSuccess = false,
                    Message = "Create Customer Failed"
                };
            }
            else
            {
                if (CustomerEmailExist(request.CreateCustomerDTO.Email))
                {
                    return new BaseCommandResponse()
                    {
                        Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList(),
                        IsSuccess = false,
                        Message = "Email Exist"
                    };
                }
                if (CustomerBasaeInfoExist(request.CreateCustomerDTO.FirstName, request.CreateCustomerDTO.LastName, request.CreateCustomerDTO.DateOfBirth))
                {
                    return new BaseCommandResponse()
                    {
                        Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList(),
                        IsSuccess = false,
                        Message = "First Name & Last Name & Date of Birth  Exist"
                    };
                }
                var newCustomer = _mapper.Map<Customer>(request.CreateCustomerDTO);
                newCustomer = await _unitOfWork.CustomerRepository.Add(newCustomer);

                await _unitOfWork.Save();
                return new BaseCommandResponse()
                {
                    IsSuccess = true,
                    Message = "Create Customer Successfully"
                };
            }
        }

        private  bool CustomerEmailExist(string email)
        {
            var xxx = _unitOfWork.CustomerRepository.GetAll().Result.ToList();
            var result = _unitOfWork.CustomerRepository.GetAll().Result.Any(x => x.Email == email);
            return result;
        }
        private bool CustomerBasaeInfoExist(string firstName, string lastName, DateTime dateOfBirth)
        {
            return _unitOfWork.CustomerRepository.GetAll().Result
                .Any(x => x.FirstName == firstName && x.LastName== lastName && x.DateOfBirth== dateOfBirth);
        }
    }
}

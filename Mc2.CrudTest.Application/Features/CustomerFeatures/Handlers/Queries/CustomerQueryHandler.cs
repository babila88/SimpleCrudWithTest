using Mc2.CrudTest.Application.Features.CustomerFeatures.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mc2.CrudTest.Application.DTOs.CustomerDTO;
using AutoMapper;
using Mc2.CrudTest.Application.Repository;

namespace Mc2.CrudTest.Application.Features.CustomerFeatures.Handlers.Queries
{
    public class CustomerQueryHandler : IRequestHandler<CustomerQuery, CustomerResultDTO>
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        public CustomerQueryHandler(IMapper mapper, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        public async Task<CustomerResultDTO> Handle(CustomerQuery request, CancellationToken cancellationToken)
        {
            var customerResult1 = await _customerRepository.GetAll();
            var customerResult = await _customerRepository.Get(request.Id);
            return _mapper.Map<CustomerResultDTO>(customerResult);
        }
    }
}

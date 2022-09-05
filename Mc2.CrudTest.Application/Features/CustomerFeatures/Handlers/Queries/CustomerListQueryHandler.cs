using AutoMapper;
using Mc2.CrudTest.Application.DTOs.CustomerDTO;
using Mc2.CrudTest.Application.Features.CustomerFeatures.Requests.Queries;
using Mc2.CrudTest.Application.Repository;
using Mc2.CrudTest.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Features.CustomerFeatures.Handlers.Queries
{
    public class CustomerListQueryHandler : IRequestHandler<CustomerListQuery, List<CustomerResultDTO>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public CustomerListQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<List<CustomerResultDTO>> Handle(CustomerListQuery request, CancellationToken cancellationToken)
        {
            var customerList = await _customerRepository.GetAll();
            return _mapper.Map<List<CustomerResultDTO>>(customerList);
        }
    }
}

using Mc2.CrudTest.Application.DTOs.CustomerDTO;
using Mc2.CrudTest.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Features.CustomerFeatures.Requests.Commands
{
    public class CreateCustomerCommand : IRequest<BaseCommandResponse>
    {
        public CustomerResultDTO CustomerDto { get; set; }
        public CreateCustomerDTO CreateCustomerDTO { get; set; }
    }
}

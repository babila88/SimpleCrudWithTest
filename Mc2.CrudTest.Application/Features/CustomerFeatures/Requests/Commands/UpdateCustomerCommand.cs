using Mc2.CrudTest.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mc2.CrudTest.Application.DTOs.CustomerDTO;

namespace Mc2.CrudTest.Application.Features.CustomerFeatures.Requests.Commands
{
    public class UpdateCustomerCommand : IRequest<Unit>
    {
        public UpdateCustomerDTO  UpdateCustomerDTO { get; set; }
    }
}

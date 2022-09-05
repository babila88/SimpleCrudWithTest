using Mc2.CrudTest.Application.DTOs.CustomerDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.Features.CustomerFeatures.Requests.Queries
{
    public class CustomerQuery :IRequest<CustomerResultDTO>
    {
        public int Id { get; set; }
    }
}

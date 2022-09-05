using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Application.DTOs.Common
{
    public abstract class BaseDTO
    {
        public Guid Id { get; set; }
    }
}

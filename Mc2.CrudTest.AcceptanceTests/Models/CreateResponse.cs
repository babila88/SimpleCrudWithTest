using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.AcceptanceTests.Models
{
    public class CreateResponse
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Exceptions
{
    public class BadRequestExecption : PassInException
    {
        public BadRequestExecption(string message) : base(message)
        {
        }
    }
}

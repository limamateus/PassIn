using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Exceptions
{
    public class NotFoundExeption : PassInException
    {
        public NotFoundExeption(string message) : base(message)
        {
        }
    }
}

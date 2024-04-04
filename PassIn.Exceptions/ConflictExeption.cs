using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Exceptions
{
    public class ConflictExeption : PassInException
    {
        public ConflictExeption(string message) : base(message)
        {
        }
    }
}

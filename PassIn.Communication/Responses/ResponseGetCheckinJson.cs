using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Communication.Responses
{
    public class ResponseGetCheckinJson
    {
        public Guid Id { get; set; }
        public Guid Attendee_Id { get; set; }

        public string Code { get; set; }

    }
}

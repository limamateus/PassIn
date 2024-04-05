using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Application.UseCases.CheckIns.GetCheckInAttendee
{
    public class GetCheckInAttendeeUseCase
    {
        private readonly PassInDbContext _dbContext;
        public GetCheckInAttendeeUseCase()
        {
            _dbContext = new PassInDbContext();
        }
        public ResponseGetCheckinJson Excute(Guid checkinId)
        {
            var entity = _dbContext.CheckIns.FirstOrDefault(att => att.Id == checkinId);
            // Esse if irá verificar se existe um pessoa 
            if (entity == null) throw new NotFoundExeption("The checkin not found.");

         

            return new ResponseGetCheckinJson
            {
                Id = entity.Id,
                Attendee_Id = entity.Attendee_Id,
                
            };
        }


       
    }
}

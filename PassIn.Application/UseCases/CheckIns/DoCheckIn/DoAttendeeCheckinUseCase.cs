using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Application.UseCases.CheckIns.DoCheckIn
{
    public class GetAttendeeCheckinUseCase
    {
        private readonly PassInDbContext _dbContext;

        public GetAttendeeCheckinUseCase()
        {
            _dbContext = new PassInDbContext();
        }
        public ResponseRegisterJson Excute(Guid attendeeId)
        {
            Validate(attendeeId);

            var entity = new CheckIn
            {
                Attendee_Id = attendeeId, // Id
                Created_at = DateTime.UtcNow,
                Code =  attendeeId.ToString().Substring(0,4).ToUpper() + Convert.ToString(DateTime.UtcNow.ToString("mmss")),

            };
            _dbContext.CheckIns.Add(entity);
            _dbContext.SaveChanges();
            return new ResponseRegisterJson
            {
                Id = entity.Id,
                Code = entity.Code,
            };
        }

      
        private void Validate(Guid attendeeId)
        {
           var existeAttendee = _dbContext.Attendees.Any(att => att.Id == attendeeId);
            // Esse if irá verificar se existe um pessoa 
            if (existeAttendee == false) throw new NotFoundExeption("The attendee with this Id was not found.");

            var existCheckin = _dbContext.CheckIns.Any(ch => ch.Attendee_Id == attendeeId);
            // Esse if irá verificar se a pessoa já realizou o checkin 
            if (existCheckin) throw new ConflictExeption("Attendee can not do checking twice in the same event.");
          
        }
    }
}

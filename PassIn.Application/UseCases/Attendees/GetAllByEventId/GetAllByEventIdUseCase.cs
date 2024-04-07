using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Application.UseCases.Attendees.GetAllByEventId
{
    public class GetAllByEventIdUseCase
    {
        private readonly PassInDbContext _dbContext;

        public GetAllByEventIdUseCase()
        {
            _dbContext = new PassInDbContext();
        }

        public ResponseAllAttendeesJson Execute(Guid enventId)
        { // Esse var armazenar a consulta no banco onde irei buscar os evento, com os clientes e o checkin de quem fez
            var entity = _dbContext.Events.Include(ev => ev.Attendees).ThenInclude(att => att.CheckIn).FirstOrDefault(ev => ev.Id == enventId);

            if (entity is null) throw new NotFoundExeption("An envet with this id does not exist.");

            return new ResponseAllAttendeesJson
            {
                Attendees =  entity.Attendees.Select(att => new ResponseAttendeeJson
                {
                    Id = att.Id,
                    Name = att.Name,
                    Email = att.Email,
                    CreatedAt= att.Created_at,
                    CheckedInAt = att.CheckIn?.Created_at,
                    Event = new ResponseEventJson
                    {
                       
                        Title = entity.Title,
                        Details = entity.Details,


                    }

                }).ToList(),
            };
        }
    }
}

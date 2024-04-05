using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Application.UseCases.Attendees.GetById
{
    public class GetByIdEventIdUseCase
    {
        private readonly PassInDbContext _dbContext;

        public GetByIdEventIdUseCase()
        {
            _dbContext = new PassInDbContext();
        }

        public ResponseAttendeeJson Execute(Guid id)
        {
            var entity = _dbContext.Attendees.Include(ch => ch.CheckIn).FirstOrDefault(att => att.Id == id);

            if (entity is null) throw new NotFoundExeption("An attendeent with this id does not exist.");

            return new ResponseAttendeeJson
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                CreatedAt = entity.Created_at,
                CheckedInAt = entity.CheckIn?.Created_at,
            };

        }
    }
}

using PassIn.Application.UseCases.Attendees.GetById;
using PassIn.Application.UseCases.Events.Register;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Application.UseCases.Events.RegisterAttendee
{
    public class RegisterAttedeeOnEventUseCase
    {
        private readonly PassInDbContext _dbContext;

        public RegisterAttedeeOnEventUseCase()
        {

            _dbContext = new PassInDbContext();


        }
        public ResponseAttendeeJson Execute( Guid eventId, RequestRegisterEventJson request)
        {           
            Validate(request, eventId);

            

            var entity = new Infrastructure.Entities.Attendee
            { 
                Name = request.Name,
                Email = request.Email,
                Event_Id = eventId,
                Created_at = DateTime.UtcNow,               


            };

            _dbContext.Attendees.Add(entity);
            _dbContext.SaveChanges();


            var getEvent = new GetEventByIdUseCase().Execute(eventId);

            return new ResponseAttendeeJson
            {
                Id = entity.Id,
                Name= entity.Name,
                Email= entity.Email,
                CreatedAt = entity.Created_at,
                Event = new ResponseEventJson{
                    Title = getEvent.Title,
                    Details = getEvent.Details,
                    AttendeesAmount = getEvent.AttendeesAmount,
                    MaximumAttendees = getEvent.MaximumAttendees
                },                
            };
        }


        private void Validate(RequestRegisterEventJson request,Guid eventId)
        {
            var entity = _dbContext.Events.Find(eventId);

            if (entity is null) throw new NotFoundExeption("An event with this id dont exist.");

            if (string.IsNullOrWhiteSpace(request.Name)) throw new BadRequestExecption("The Name is invalid");

            if (EmailIsValid(request.Email) == false) throw new BadRequestExecption("The Email is invalid");


            var attendeeAlreadyRegistered = _dbContext.Attendees.Any(at => at.Email.Equals(request.Email) && at.Event_Id == eventId);

            if(attendeeAlreadyRegistered) throw new BadRequestExecption("You can not register twice on the same event.");

             var attendeesForEvent =   _dbContext.Attendees.Count(at => at.Event_Id == eventId);

            if(attendeesForEvent == entity.Maximum_Attendees) throw new ConflictExeption("There is no room this event.");

        }


        private bool EmailIsValid(string email)
        {
            try
            {
                new MailAddress(email);
                return true;
            }
            catch 
            {

                return false;
            }
        }
    }
}

using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Application.UseCases.CheckIns.GetCheckInByCode
{
    public class GetCheckInByCodeUseCase
    {

        private readonly PassInDbContext _dbContext;

        public GetCheckInByCodeUseCase()
        {
            _dbContext = new PassInDbContext();
        }


        public ResponseGetCheckinJson Excute(string code)
        {
            var entity = _dbContext.CheckIns.FirstOrDefault(cd => cd.Code == code.ToUpper());
            // Esse if irá verificar se existe um pessoa 
            if (entity == null) throw new NotFoundExeption("The checkin not found.");



            return new ResponseGetCheckinJson
            {
                Id = entity.Id,
                Attendee_Id = entity.Attendee_Id,
                Code = entity.Code,
            };
        }
    }
}

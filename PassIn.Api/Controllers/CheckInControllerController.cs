﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.CheckIns.DoCheckIn;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckInControllerController : ControllerBase
    {
        [HttpPost]
        [Route("{attendeeId}")]
        [ProducesResponseType(typeof(ResponseRegisterJson), StatusCodes.Status201Created)] // Caso o Checking for realizado
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)] // Caso não encontre
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)] // Caso o usuario tente realizar um checking duas vezes
        public IActionResult Checkin([FromRoute] Guid attendeeId)
        {
            var useCase = new DoAttendeeCheckinUseCase();
            var response = useCase.Excute(attendeeId);

            return Created();
        }
    }
}
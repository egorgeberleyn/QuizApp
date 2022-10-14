﻿using Microsoft.AspNetCore.Mvc;
using QuizAPI.Data.Interfaces;
using QuizAPI.Models;

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        private readonly IParticipantRepository _participantRepository;

        public ParticipantsController(IParticipantRepository participantRepository)
        {
            _participantRepository = participantRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync() =>
            Ok(await _participantRepository.GetAllParticipantsAsync());


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] int id) =>
            Ok(await _participantRepository.GetParticipantAsync(id));


        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Participant participant)
        {
            await _participantRepository.CreateParticipantAsync(participant);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] Participant participant)
        {
            var participantFromDb = await _participantRepository.GetParticipantAsync(participant.Id);
            if (participant == null)
                return NotFound();
            await _participantRepository.DeleteParticipantAsync(id);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] int id)
        {
            var participantFromdb = await _participantRepository.GetParticipantAsync(id);
            if (participantFromdb == null)
                return NotFound();
            await _participantRepository.DeleteParticipantAsync(id);
            return Ok();
        }
    }
}
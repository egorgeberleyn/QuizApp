using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            var participant = await _participantRepository.GetParticipantAsync(id);
            return participant != null
            ? Ok(participant)
            : NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Participant participant)
        {
            var participantFromDb = await _participantRepository.GetParticipantByEmailAsync(participant.Email);
            if(participantFromDb != null)
                return Ok(participantFromDb);
            await _participantRepository.CreateParticipantAsync(participant);
            return Ok();
        }

        [HttpPut("{id}")]       
        public async Task<IActionResult> UpdateAsync(int id, Result result)
        {
            if(id != result.ParticipantId)
                return BadRequest();            
            var participantFromDb = await _participantRepository.GetParticipantAsync(id);
            if (participantFromDb == null)
                return NotFound();
            participantFromDb.TimeTaken = result.TimeTaken;
            participantFromDb.Score = result.Score;
            await _participantRepository.UpdateParticipantAsync(participantFromDb);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var participantFromDb = await _participantRepository.GetParticipantAsync(id);
            if (participantFromDb == null)
                return NotFound();
            await _participantRepository.DeleteParticipantAsync(id);
            return Ok();
        }
    }
}

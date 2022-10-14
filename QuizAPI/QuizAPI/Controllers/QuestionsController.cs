using Microsoft.AspNetCore.Mvc;
using QuizAPI.Data.Interfaces;
using QuizAPI.Models;

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionsController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestions() =>
            Ok(await _questionRepository.GetAllQuestionsAsync());
        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            var question = await _questionRepository.GetQuestionAsync(id);
            return question != null
                ? Ok(question)
                : NotFound();
        }
            
        
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Question question)
        {
            var questionFromDb = await _questionRepository.GetQuestionAsync(question.Id);
            if(questionFromDb != null)
                return Ok(questionFromDb);
            await _questionRepository.CreateQuestionAsync(question);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] Question question)
        {
            var questionFromDb = await _questionRepository.GetQuestionAsync(question.Id);
            if(questionFromDb == null)
                return NotFound();
            await _questionRepository.UpdateQuestionAsync(questionFromDb);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var questionFromDb = await _questionRepository.GetQuestionAsync(id);
            if (questionFromDb == null)
                return NotFound();
            await _questionRepository.DeleteQuestionAsync(id);
            return Ok();
        }
    }
}

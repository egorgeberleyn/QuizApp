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
        public async Task<IActionResult> GetAsync([FromRoute] int id) =>        
            Ok(await _questionRepository.GetQuestionAsync(id));
        
        
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Question question)
        {
            await _questionRepository.CreateQuestionAsync(question);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] Question question)
        {
            await _questionRepository.UpdateQuestionAsync(question);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] int id)
        {
            await _questionRepository.DeleteQuestionAsync(id);
            return Ok();
        }
    }
}

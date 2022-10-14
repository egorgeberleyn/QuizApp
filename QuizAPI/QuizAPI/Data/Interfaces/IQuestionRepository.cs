using QuizAPI.Models;

namespace QuizAPI.Data.Interfaces
{
    public interface IQuestionRepository
    {
        Task CreateQuestionAsync(Question question);
        Task DeleteQuestionAsync(int id);
        Task UpdateQuestionAsync(Question question);
        
        Task<Question> GetQuestionAsync(int id);
        Task<IEnumerable<Question>> GetAllQuestionsAsync();
    }
}

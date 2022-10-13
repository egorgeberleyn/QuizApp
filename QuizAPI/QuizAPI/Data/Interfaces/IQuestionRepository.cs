using QuizAPI.Models;

namespace QuizAPI.Data.Interfaces
{
    public interface IQuestionRepository
    {
        Task Create(Question question);
        Task Delete(int id);
        Task Update(Question question);
        
        Task<Question> Get(int id);
        Task<IEnumerable<Question>> GetAll();
    }
}

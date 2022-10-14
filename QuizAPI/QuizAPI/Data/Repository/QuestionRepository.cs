using Dapper;
using QuizAPI.Data.Interfaces;
using QuizAPI.Models;

namespace QuizAPI.Data.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly DapperContext _context;

        public QuestionRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateQuestionAsync(Question question)
        {
            using var connection = _context.CreateConnection();
            var sqlQuery = "INSERT INTO Questions (Text, ImageName, Option1, Option2, Option3, Option4, Answer) VALUES(@Text, @ImageName, @Option1, @Option2, @Option3, @Option4, @Answer)";
            await connection.ExecuteAsync(sqlQuery, question);
        }

        public async Task DeleteQuestionAsync(int id)
        {
            using var connection = _context.CreateConnection();
            var sqlQuery = "DELETE FROM Questions WHERE Id = @Id";
            await connection.ExecuteAsync(sqlQuery, new { id });
        }

        public async Task UpdateQuestionAsync(Question question)
        {
            using var connection = _context.CreateConnection();
            var sqlQuery = "UPDATE Questions SET Text = @Text, ImageName = @ImageName, Option1 = @Option1, Option2 = @Option2, Option3 = @Option3, Option4 = @Option4, Answer = @Answer WHERE Id = @Id";
            await connection.ExecuteAsync(sqlQuery, question);
        }

        public async Task<Question> GetQuestionAsync(int id)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Question>("SELECT * FROM Questions WHERE Id = @Id", new { id });
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Question>("SELECT * FROM Questions");
        }
    }
}

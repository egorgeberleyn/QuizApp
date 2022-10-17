using Dapper;
using QuizAPI.Data.Interfaces;
using QuizAPI.Models;

namespace QuizAPI.Data.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly DapperContext _context;
        private const string TABLE_NAME = "questions";

        public QuestionRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task CreateQuestionAsync(Question question)
        {
            using var connection = _context.CreateConnection();
            var sqlQuery = $"INSERT INTO {TABLE_NAME} (text, image_name, option1, option2, option3, option4, answer) VALUES(@Text, @ImageName, @Option1, @Option2, @Option3, @Option4, @Answer)";
            await connection.ExecuteAsync(sqlQuery, question);
        }

        public async Task DeleteQuestionAsync(int id)
        {
            using var connection = _context.CreateConnection();
            var sqlQuery = $"DELETE FROM {TABLE_NAME} WHERE id = @Id";
            await connection.ExecuteAsync(sqlQuery, new { id });
        }

        public async Task UpdateQuestionAsync(Question question)
        {
            using var connection = _context.CreateConnection();
            var sqlQuery = $"UPDATE {TABLE_NAME} SET text = @Text, image_name = @ImageName, option1 = @Option1, option2 = @Option2, option3 = @Option3, option4 = @Option4, answer = @Answer WHERE id = @Id";
            await connection.ExecuteAsync(sqlQuery, question);
        }

        public async Task<Question> GetQuestionAsync(int id)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Question>($"SELECT * FROM {TABLE_NAME} WHERE id = @Id", new { id });
        }

        public async Task<IEnumerable<Question>> GetRandomQuestionsAsync()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Question>($"SELECT Id, text, image_name as ImageName, option1, option2, option3, option4 FROM {TABLE_NAME} ORDER BY RANDOM() LIMIT 5");
        }

        public async Task<IEnumerable<Question>> GetQuestionsWithAnswersAsync(int[] qnIds)
        {

            using var connection = _context.CreateConnection();
            var qnWithAnswers =  await connection.QueryAsync<Question>($"SELECT Id, text, image_name as ImageName, option1, option2, option3, option4, answer FROM {TABLE_NAME}");
            return qnWithAnswers.Where(q => qnIds.Contains(q.Id));
        }
    }
}

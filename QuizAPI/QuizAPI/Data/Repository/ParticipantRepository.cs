using Dapper;
using QuizAPI.Data.Interfaces;
using QuizAPI.Models;

namespace QuizAPI.Data.Repository
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly DapperContext _context;
        private const string TABLE_NAME = "participants";

        public ParticipantRepository(DapperContext context)
        {
            _context = context;
        }
        
        public async Task CreateParticipantAsync(Participant participant)
        {
            using var connection = _context.CreateConnection();
            var sqlQuery = $"INSERT INTO {TABLE_NAME} (name, email, score, time_taken) VALUES(@Name, @Email, @Score, @TimeTaken)";
            await connection.ExecuteAsync(sqlQuery, participant);
        }

        public async Task DeleteParticipantAsync(int id)
        {
            using var connection = _context.CreateConnection();
            var sqlQuery = $"DELETE FROM {TABLE_NAME} WHERE id = @Id";
            await connection.ExecuteAsync(sqlQuery, new { id });
        }

        public async Task UpdateParticipantAsync(Participant participant)
        {
            using var connection = _context.CreateConnection();
            var sqlQuery = $"UPDATE {TABLE_NAME} SET name = @Name, email = @Email, score = @Score, time_taken = @TimeTaken WHERE id = @Id";
            await connection.ExecuteAsync(sqlQuery, participant);
        }

        public async Task<Participant> GetParticipantAsync(int id)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Participant>($"SELECT * FROM {TABLE_NAME} WHERE id = @Id", new { id });
        }

        public async Task<IEnumerable<Participant>> GetAllParticipantsAsync()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Participant>($"SELECT * FROM {TABLE_NAME}");
        }
    }
}

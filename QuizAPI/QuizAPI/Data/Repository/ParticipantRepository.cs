﻿using Dapper;
using QuizAPI.Data.Interfaces;
using QuizAPI.Models;

namespace QuizAPI.Data.Repository
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly DapperContext _context;

        public ParticipantRepository(DapperContext context)
        {
            _context = context;
        }


        public async Task Create(Participant participant)
        {
            using var connection = _context.CreateConnection();
            var sqlQuery = "INSERT INTO Participants (Name, Email, Score, TimeTaken) VALUES(@Name, @Email, @Score, @TimeTaken)";
            await connection.ExecuteAsync(sqlQuery, participant);
        }

        public async Task Delete(int id)
        {
            using var connection = _context.CreateConnection();
            var sqlQuery = "DELETE FROM Participants WHERE Id = @Id";
            await connection.ExecuteAsync(sqlQuery, new { id });
        }

        public async Task Update(Participant participant)
        {
            using var connection = _context.CreateConnection();
            var sqlQuery = "UPDATE Participants SET Name = @Name, Email = @Email, Score = @Score, TimeTaken = @TimeTaken WHERE Id = @Id";
            await connection.ExecuteAsync(sqlQuery, participant);
        }

        public async Task<Participant> Get(int id)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Participant>("SELECT * FROM Participants WHERE Id = @Id", new { id });
        }

        public async Task<IEnumerable<Participant>> GetAll()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Participant>("SELECT * FROM Participants");
        }
    }
}
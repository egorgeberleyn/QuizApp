using QuizAPI.Models;

namespace QuizAPI.Data.Interfaces
{
    public interface IParticipantRepository
    {
        Task CreateParticipantAsync(Participant participant);
        Task DeleteParticipantAsync(int id);
        Task UpdateParticipantAsync(Participant participant);

        Task<Participant> GetParticipantAsync(int id);
        Task<IEnumerable<Participant>> GetAllParticipantsAsync();
    }
}

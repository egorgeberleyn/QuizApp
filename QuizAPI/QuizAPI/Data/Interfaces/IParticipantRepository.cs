using QuizAPI.Models;

namespace QuizAPI.Data.Interfaces
{
    public interface IParticipantRepository
    {
        Task Create(Participant participant);
        Task Delete(int id);
        Task Update(Participant participant);

        Task<Participant> Get(int id);
        Task<IEnumerable<Participant>> GetAll();
    }
}

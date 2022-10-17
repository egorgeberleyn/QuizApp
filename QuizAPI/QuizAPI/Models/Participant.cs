namespace QuizAPI.Models
{
    public class Participant
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int Score { get; set; }
        
        public int TimeTaken { get; set; }
    }   
}

namespace HCMQuizGame.Models
{
    public class PlayerResult
    {
        public string PlayerName { get; set; }

        public double Score { get; set; }

        public int TimeUsed { get; set; }

        public DateTime FinishedAt { get; set; }
    }
}
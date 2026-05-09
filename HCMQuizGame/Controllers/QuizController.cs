using HCMQuizGame.Data;
using HCMQuizGame.Models;
using HCMQuizGame.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;

namespace HCMQuizGame.Controllers
{
    public class QuizController : Controller
    {
        private readonly IHubContext<QuizHub> QuizHubContext;

        public QuizController(
            IHubContext<QuizHub> hubContext)
        {
            QuizHubContext = hubContext;
        }

        // ===== TRANG THAM GIA =====
        public IActionResult Join()
        {
            return View();
        }

        // ===== PHÒNG CHỜ =====
        [HttpPost]
        public IActionResult Waiting(string playerName)
        {
            HttpContext.Session
                .SetString("PlayerName", playerName);

            ViewBag.PlayerName = playerName;

            return View();
        }

        // ===== CHƠI GAME =====
        public IActionResult Play()
        {
            ViewBag.PlayerName =
                HttpContext.Session.GetString("PlayerName");

            return View();
        }

        // ===== NỘP KẾT QUẢ =====
        [HttpPost]
        public async Task<IActionResult> SubmitResult(
            string playerName,
            double score,
            int timeUsed)
        {
            // Xóa kết quả cũ của người chơi
            GameData.Leaderboard.RemoveAll(
                x => x.PlayerName == playerName
            );

            // Thêm kết quả mới
            GameData.Leaderboard.Add(new PlayerResult
            {
                PlayerName = playerName,
                Score = score,
                TimeUsed = timeUsed,
                FinishedAt = DateTime.Now
            });

            // Reload realtime leaderboard
            await QuizHubContext.Clients.All.SendAsync(
                "ReloadLeaderboard"
            );

            return Ok();
        }

        // ===== BẢNG XẾP HẠNG =====
        public IActionResult Leaderboard()
        {
            var data = GameData.Leaderboard

                .OrderByDescending(x => x.Score)

                .ThenBy(x => x.TimeUsed)

                .ThenBy(x => x.FinishedAt)

                .ToList();

            return View(data);
        }
    }
}
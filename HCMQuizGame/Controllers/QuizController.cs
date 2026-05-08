
using Microsoft.AspNetCore.Mvc;
using HCMQuizGame.Data;
using HCMQuizGame.Models;

namespace HCMQuizGame.Controllers
{
    public class QuizController : Controller
    {
        public IActionResult Join()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Waiting(string playerName)
        {
            HttpContext.Session
                .SetString("PlayerName", playerName);

            ViewBag.PlayerName = playerName;

            return View();
        }

        public IActionResult Play()
        {
            ViewBag.PlayerName =
                HttpContext.Session.GetString("PlayerName");

            return View();
        }

        [HttpPost]
        public IActionResult SubmitResult(
            string playerName,
            double score,
            int timeUsed)
        {
            GameData.Leaderboard.Add(new PlayerResult
            {
                PlayerName = playerName,
                Score = score,
                TimeUsed = timeUsed,
                FinishedAt = DateTime.Now
            });

            return Ok();
        }

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


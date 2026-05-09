
using HCMQuizGame.Data;
using HCMQuizGame.Models;
using HCMQuizGame.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;
using System;

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
public async Task<IActionResult> SubmitResult(
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

            await QuizHubContext.Clients.All.SendAsync(
                "ReloadLeaderboard"
            );

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


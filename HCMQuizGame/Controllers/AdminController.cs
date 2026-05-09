using HCMQuizGame.Data;
using HCMQuizGame.Data;

using Microsoft.AspNetCore.Mvc;
using System;

namespace HCMQuizGame.Controllers
{
    public class AdminController : Controller
    {
public IActionResult ResetLeaderboard()
        {
            GameData.Leaderboard.Clear();

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
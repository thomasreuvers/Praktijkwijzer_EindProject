using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using MaasVallei.Entities;
using MaasVallei.Models;
using MaasVallei.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MaasVallei.Controllers
{
    public class RoosterController : Controller
    {
        private readonly ScheduleService _scheduleService;
        private readonly UserService _userService;

        public RoosterController(ScheduleService scheduleService, UserService userService)
        {
            _scheduleService = scheduleService;
            _userService = userService;
        }

        /// <summary>
        /// First get the current schedule of this week.
        /// Then return a model to the view with that data.
        /// </summary>
        /// <returns></returns>
        public IActionResult Roosters()
        {
            var days = DateTime.Today.DayOfWeek - DayOfWeek.Sunday;
            var weekStart = DateTime.Today.AddDays(-days);
            var weekEnd = weekStart.AddDays(6);

            var SchedulesInDb = _scheduleService.Get().Where(x => x.RoosterDate <= weekEnd && x.RoosterDate >= weekStart);

            var schedules = new List<RoosterModel>();
            schedules.AddRange(SchedulesInDb.Select(schedule => new RoosterModel
                {EndShift = schedule.EndShift.ToLocalTime(),
                    StartShift = schedule.StartShift.ToLocalTime(),
                    Id = schedule.Id, User = _userService.Get(schedule.UserId),
                    RoosterDate = schedule.RoosterDate.ToLocalTime()}));

            return View(schedules);
        }


        /// <summary>
        /// On POST extract model data and create a new schedule.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Roosters(RoosterModel model)
        {
            if (!ModelState.IsValid) return View();

            if (model == null) return Roosters();

            var rooster = new Rooster
            {
                UserId = model.SelectedUser,
                StartShift = model.StartShift,
                EndShift = model.EndShift,
                RoosterDate = model.RoosterDate
            };

            _scheduleService.Create(rooster);

            TempData["message"] = new AlertMessage { CssClass = "alert-success", Id = string.Empty, Title = "Rooster aangemaakt.", Message = $"Het rooster voor {model.RoosterDate} is succesvol aangemaakt!" };

            return Roosters();
        }
    }
}
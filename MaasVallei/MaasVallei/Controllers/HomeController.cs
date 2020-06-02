using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MaasVallei.Models;
using MaasVallei.Services;
using Microsoft.AspNetCore.Authorization;

namespace MaasVallei.Controllers
{
    public class HomeController : Controller
    {
        private readonly ReservationService _reservationService;
        private readonly ComplaintsService _complaintsService;

        public HomeController(ReservationService reservationService, ComplaintsService complaintsService)
        {
            _reservationService = reservationService;
            _complaintsService = complaintsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Return panel view with model data.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Panel()
        {
            var currentUserReservation = _reservationService.Get().OrderByDescending(x => x.ArriveDate)
                .FirstOrDefault(x => x.UserId == User.FindFirstValue(ClaimTypes.UserData));

            var model = new PanelModel
            {
                Reservations = _reservationService.Get().Count(x => x.DepartureDate.ToLocalTime() > DateTime.UtcNow), 
            };

            if (_complaintsService.Get().Any())
            {
                model.Complaints = new List<ComplaintsModel>();
                foreach (var complaint in _complaintsService.Get())
                {
                    
                    model.Complaints.Add(new ComplaintsModel
                    {
                        DateOfCreation = complaint.DateOfCreation.ToLocalTime(),
                        Department = Enum.Parse<Departments>(complaint.Department),
                        Priority = Enum.Parse<Priority>(complaint.Priority),
                        Description = complaint.Description,
                        Title = complaint.Title,
                        EmailAddress = complaint.EmailAddress,
                        State = complaint.State,
                        UserId = complaint.UserId,
                        Id = complaint.Id
                    });
                }
            }

            if (currentUserReservation != null)
            {
                model.CurrentUserReservationModel = new ReservationModel
                {
                    ArriveDate = currentUserReservation.ArriveDate,
                    DepartureDate = currentUserReservation.DepartureDate,
                    EmailAddress = currentUserReservation.EmailAddress,
                    PhoneNumber = currentUserReservation.PhoneNumber
                };
            }

            return View(model);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MaasVallei.Entities;
using MaasVallei.Models;
using MaasVallei.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MaasVallei.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly ReservationService _reservationService;

        public ReservationController(ReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        /// <summary>
        /// Return the reservation view with all reservations made by the current logged in user.
        /// </summary>
        /// <returns></returns>
        public IActionResult Reserve()
        {
            var models = _reservationService.Get();

            var reservations = new List<ReservationModel>();
            reservations.AddRange(models.Select(model => new ReservationModel  { UserId = model.UserId, ReservationId = model.Id, ArriveDate = model.ArriveDate.ToLocalTime(), DepartureDate = model.DepartureDate.ToLocalTime(), EmailAddress = model.EmailAddress, PhoneNumber = model.PhoneNumber}));

            return View(reservations);
        }

        /// <summary>
        /// On POST extract data from model. Check what FormOption is set.
        /// On the basis of FormOption execute further actions.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Reserve(ReservationModel model)
        {
            if (model.FormOption == "DELETE" && model.ReservationId != null)
            {
                _reservationService.Delete(model.ReservationId);
                TempData["message"] = new AlertMessage { CssClass = "alert-success", Id = string.Empty, Title = "Reservering verwijderd", Message = "Uw Reservering is succesvol verwijderd!" };
                return Reserve();
            }

            if (!ModelState.IsValid) return Reserve();

            if (model.FormOption == null)
            {
                _reservationService.Create(new Reservation
                {
                    ArriveDate = model.ArriveDate,
                    DepartureDate = model.DepartureDate,
                    EmailAddress = model.EmailAddress,
                    PhoneNumber = model.PhoneNumber,
                    UserId = User.FindFirstValue(ClaimTypes.UserData)
                });

                TempData["message"] = new AlertMessage { CssClass = "alert-success", Id = string.Empty, Title = "Reservering gemaakt", Message = "Uw Reservering is succesvol aangemaakt!" };

            }
            else
            {
                var reservationToUpdate = _reservationService.Get(model.ReservationId);

                // Reservation does not exists return view
                if (reservationToUpdate == null) return Reserve();

                reservationToUpdate.EmailAddress = model.EmailAddress;
                reservationToUpdate.PhoneNumber = model.PhoneNumber;
                reservationToUpdate.ArriveDate = model.ArriveDate;
                reservationToUpdate.DepartureDate = model.DepartureDate;

                // Update reservation
                _reservationService.Update(reservationToUpdate);

                TempData["message"] = new AlertMessage { CssClass = "alert-success", Id = string.Empty, Title = "Reservering bijgewerkt.", Message = "Uw reservering is succesvol bijgewerkt!" };
            }

            return Reserve();
        }
        
        public IActionResult ReservationModal()
        {
            return View();
        }
    }
}
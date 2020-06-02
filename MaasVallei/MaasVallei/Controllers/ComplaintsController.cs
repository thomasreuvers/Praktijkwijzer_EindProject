using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MaasVallei.Entities;
using MaasVallei.Models;
using MaasVallei.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace MaasVallei.Controllers
{
    [Authorize]
    public class ComplaintsController : Controller
    {
        private readonly ComplaintsService _complaintsService;

        public ComplaintsController(ComplaintsService complaintsService)
        {
            _complaintsService = complaintsService;
        }

        /// <summary>
        /// Fetch all complaints from the database and return them in a model
        /// </summary>
        /// <returns></returns>
        public IActionResult Complainments()
        {
            var complaints = _complaintsService.Get();

            var model = new List<ComplaintsModel>();
            model.AddRange(complaints.Select( complaint => new ComplaintsModel{
                DateOfCreation = complaint.DateOfCreation.ToLocalTime(),
                Department = Enum.Parse<Departments>(complaint.Department),
                Priority = Enum.Parse<Priority>(complaint.Priority),
                Description = complaint.Description,
                Title = complaint.Title,
                EmailAddress = complaint.EmailAddress,
                State = complaint.State,
                UserId = complaint.UserId,
                Id = complaint.Id
            }));


            return View(model);
        }

        /// <summary>
        /// On POST extract data out of model.
        /// Check if form option is equal to "HANDLE".
        /// Then set Complaint state as "Afgehandeld".
        /// Else the user did want to create a complaint.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Complainments(ComplaintsModel model)
        {
            if (model.FormOption == "HANDLE" && model.Id != null)
            {
                var complaintToUpdate = _complaintsService.Get(model.Id);

                if (complaintToUpdate == null) return Complainments();

                complaintToUpdate.State = "Afgehandeld";

                _complaintsService.Update(complaintToUpdate);

                TempData["message"] = new AlertMessage { CssClass = "alert-success", Id = string.Empty, Title = "Klacht afgehandeld.", Message = "Uw hebt de klacht succesvol afgehandeld!" };

                return Complainments();
            }

            if (!ModelState.IsValid) return View();

            var complaint = new Complaint
            {
                Department = model.Department.ToString(),
                DateOfCreation = DateTime.UtcNow,
                EmailAddress = User.FindFirstValue(ClaimTypes.Email),
                Description = model.Description,
                Title = model.Title,
                UserId = User.FindFirstValue(ClaimTypes.UserData),
                Priority = model.Priority.ToString(),
                State = "In Behandeling"
            };

            _complaintsService.Create(complaint);

            TempData["message"] = new AlertMessage { CssClass = "alert-success", Id = string.Empty, Title = "Klacht ontvangen", Message = "Uw klacht is succesvol ontvangen!"};

            return Complainments();
        }
    }
}
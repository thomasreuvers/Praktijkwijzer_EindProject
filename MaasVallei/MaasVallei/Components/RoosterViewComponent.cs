using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaasVallei.Models;
using MaasVallei.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MaasVallei.Components
{
    public class RoosterViewComponent : ViewComponent
    {
        private readonly UserService _userService;

        public RoosterViewComponent(UserService userService)
        {
            _userService = userService;
        }


        /// <summary>
        /// When called return a View component with a new RoosterModel object
        /// </summary>
        /// <returns></returns>
        public IViewComponentResult Invoke()
        {
            var users = _userService.Get().Where(x => x.Role == "TECHNISCH" || x.Role == "SCHOONMAAK" || x.Role == "ADMINISTRATIEF");

            var model = new RoosterModel
            {
                AllUsers = new List<SelectListItem>()
            };

            foreach (var user in users)
            {
                model.AllUsers.Add(new SelectListItem
                {
                    Text = $"{user.Username} | {user.Role}",
                    Value = user.Id
                });
            }

            return View(model);
        }
    }
}

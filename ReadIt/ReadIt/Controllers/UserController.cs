using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadIt.Models.DTOs;
using ReadIt.Models.Entities;

namespace ReadIt.Controllers
{
    public class UserController : Controller
    {
        private UserManager<User> userManager;

        public UserController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> Details()
        {
            User user = await userManager.GetUserAsync(User);

            UserDTO userDTO = new UserDTO();
            userDTO.Email = user.Email;
            userDTO.FirstName = user.FirstName;
            userDTO.LastName = user.LastName;

            return View(userDTO);
        }
    }
}

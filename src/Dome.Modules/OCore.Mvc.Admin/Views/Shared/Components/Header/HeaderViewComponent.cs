using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OCore.Identity;
using OCore.Mvc.Admin.Models;

namespace OCore.Mvc.Admin.Views.Shared.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger _logger;

        public HeaderViewComponent(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<HeaderViewComponent> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
 
            var loginUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var headerModel = new HeaderViewModel()
            {
                LoginInformations = new GetCurrentLoginInformationsOutput() {
                    User = new UserLoginInfoDto() {
                    Id = loginUser.Id,
                    FullName = loginUser.FullName,
                    UserName = loginUser.UserName,
                    EmailAddress = loginUser.Email }
                }
            };

            return View(headerModel);
        }
    }
}

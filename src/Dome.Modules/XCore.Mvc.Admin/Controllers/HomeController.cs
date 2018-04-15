using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using XCore.Environment.Extensions.Features;
using XCore.Identity;
using XCore.Identity.Extensions;
using XCore.Mvc.Admin.Models;
using XCore.Security.Permissions;
using XCore.Web.Common;

namespace XCore.Mvc.Admin.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IEnumerable<IPermissionProvider> permissionProviders;
        private readonly ITypeFeatureProvider typeFeatureProvider;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger _logger;

        public HomeController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IEnumerable<IPermissionProvider> _permissionProviders,
            ITypeFeatureProvider _typeFeatureProvider,
            SignInManager<User> signInManager,
            ILogger<HomeController> logger)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.permissionProviders = _permissionProviders;
            this.typeFeatureProvider = _typeFeatureProvider;
            _signInManager = signInManager;
            _logger = logger;
        }
        [TempData]
        public string StatusMessage { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Installed()
        {
            var vm = new InstalledViewModel();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Installed(InstalledViewModel vm)
        {
            var ajaxResponse = CreateAjaxResponse();
            if (ModelState.IsValid)
            {
                if (vm.InstalledKey == "tonggentongmeng")
                {
                    var admin = new Role() { Name = "administrator" };
                    var userAdmin = new User() { Email = "admin@tonggentongmeng.com", UserName = "admin@tonggentongmeng.com", FullName = "admin" };

                    await roleManager.CreateAsync(admin);

                    //List<RoleClaim> rolePermissions = new List<RoleClaim>();
                    var p = GetInstalledPermissions();
                    foreach (var item in p)
                    {
                        foreach (var item2 in item.Value)
                        {
                            await roleManager.AddClaimAsync(admin, new RoleClaim { ClaimType = Permission.ClaimType, ClaimValue = item.Key + "." + item2.Name }.ToClaim());
                        }
                    }

                    var result = await userManager.CreateAsync(userAdmin, "Chuangkaiguoji@123.");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(userAdmin, admin.Name);

                        //激活该帐号
                        userAdmin.IsActive = true;
                        await userManager.UpdateAsync(userAdmin);
                    }
                }
                else
                {
                    _logger.LogWarning("无效的安装密钥");
                    ajaxResponse.Errors.Add(new ErrorInfo(-1, "无效的安装密钥"));
                }
            }
            else
            {
                AddErrorsToAjaxResponse(ajaxResponse);
            }

            return Json(ajaxResponse);
        }

        #region Login
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            var model = new LoginViewModel();
            model.OtherLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync())
                //.Where(auth => model.CurrentLogins.All(ul => auth.Name != ul.LoginProvider))
                .ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Login([FromForm]LoginViewModel model, string returnUrl = null)
        {
            var ajaxResponse = CreateAjaxResponse();
            ajaxResponse.TargetUrl = returnUrl;

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation($"用户 {model.Email} 登录成功");

                    ajaxResponse.TargetUrl = GetAppHomeUrl();
                }
                else if (result.RequiresTwoFactor)
                {
                    //登录成功，但是还要进行二次验证
                    ajaxResponse.TargetUrl = Url.Action("LoginWith2fa", "Home", new { area = "Admin" });
                }
                else if (result.IsLockedOut)
                {
                    _logger.LogWarning("用户帐户被锁定");
                    ajaxResponse.Errors.Add(new ErrorInfo(-1, "用户帐户被锁定"));
                }
                else
                {
                    _logger.LogWarning("无效的登录尝试");
                    ajaxResponse.Errors.Add(new ErrorInfo(-2, "无效的登录尝试"));
                }
            }
            else
            {
                AddErrorsToAjaxResponse(ajaxResponse);
            }

            return Json(ajaxResponse);
        }

        [HttpGet]
        public async Task<IActionResult> LoginWith2fa(bool rememberMe, string returnUrl = null)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            return View();
        }
        #endregion

        #region Register
        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Register([FromForm]RegisterViewModel model, string returnUrl = null)
        {
            var ajaxResponse = CreateAjaxResponse();
            ajaxResponse.TargetUrl = returnUrl;

            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email, FullName = model.FullName, PhoneNumber = model.PhoneNumber };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation($"用户 {model.Email} 注册成功");
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    //await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    //return RedirectToLocal(returnUrl);

                    ajaxResponse.TargetUrl = GetAppHomeUrl();
                }
                else
                {
                    IdentityResultToAjaxResponse(ajaxResponse, result);
                }
            }
            else
            {
                AddErrorsToAjaxResponse(ajaxResponse);
            }
            await Task.Run(() => { });

            return Json(ajaxResponse);
        }
        #endregion

        //转到外部登录页面，并在外部登录成功后进行相应处理
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "home", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        //外部登录成功后回调处理页面
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToAction(nameof(Login));
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in with {Name} provider.", info.LoginProvider);
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(Lockout));
            }
            else if (result.IsNotAllowed) {
                return RedirectToAction(nameof(NotAllowed));
            } else
            {
                // 如果用户没有帐户，请跳转到创建帐户页面。
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["LoginProvider"] = info.LoginProvider;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View("ExternalLogin", new ExternalLoginRegisterViewModel { Email = email });
            }
        }

        //外部登录本地帐户注册处理
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginRegisterViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    throw new ApplicationException("Error loading external login information during confirmation.");
                }
                var user = new User { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber, FullName = model.FullName };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        //await _signInManager.SignInAsync(user, isPersistent: false);
                        //_logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                        //这里不能直接登录，_signInManager.SignInAsync()方法并没有做登录前检查
                        //例如，当系统开启了“手机验证”、“邮箱验证”、“注册用户审核”等功能后，直接发放登录令牌会导致用户越权访问。
                        return RedirectToLocal(Url.Action(nameof(NotAllowed)));
                    }
                }
                AddErrors(result);
            }
            //await Task.Run(()=> { });

            ViewData["ReturnUrl"] = returnUrl;
            return View(nameof(ExternalLogin), model);
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }


        //[HttpGet]
        //public async Task<IActionResult> ExternalLogins()
        //{
        //    var user = await userManager.GetUserAsync(User);
        //    if (user == null)
        //    {
        //        throw new ApplicationException($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
        //    }

        //    var model = new ExternalLoginsViewModel { CurrentLogins = await userManager.GetLoginsAsync(user) };
        //    model.OtherLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync())
        //        .Where(auth => model.CurrentLogins.All(ul => auth.Name != ul.LoginProvider))
        //        .ToList();
        //    model.ShowRemoveButton = await userManager.HasPasswordAsync(user) || model.CurrentLogins.Count > 1;
        //    model.StatusMessage = StatusMessage;

        //    return View(model);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> LinkLogin(string provider)
        //{
        //    // Clear the existing external cookie to ensure a clean login process
        //    await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        //    // Request a redirect to the external login provider to link a login for the current user
        //    var redirectUrl = Url.Action(nameof(LinkLoginCallback));
        //    var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, userManager.GetUserId(User));
        //    return new ChallengeResult(provider, properties);
        //}

        //[HttpGet]
        //public async Task<IActionResult> LinkLoginCallback()
        //{
        //    var user = await userManager.GetUserAsync(User);
        //    if (user == null)
        //    {
        //        throw new ApplicationException($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
        //    }

        //    var info = await _signInManager.GetExternalLoginInfoAsync(user.Id);
        //    if (info == null)
        //    {
        //        throw new ApplicationException($"Unexpected error occurred loading external login info for user with ID '{user.Id}'.");
        //    }

        //    var result = await userManager.AddLoginAsync(user, info);
        //    if (!result.Succeeded)
        //    {
        //        throw new ApplicationException($"Unexpected error occurred adding external login for user with ID '{user.Id}'.");
        //    }

        //    // Clear the existing external cookie to ensure a clean login process
        //    await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        //    StatusMessage = "The external login was added.";
        //    return RedirectToAction(nameof(ExternalLogins));
        //}

        [HttpGet]
        [AllowAnonymous]
        public IActionResult NotAllowed()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation($"用户 {userManager.GetUserId(User)} 退出登录");
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Home");
        }

        #region 一些验证
        [HttpGet]
        public async Task<JsonResult> CheckFullName(string fullname, string id = null/*需要忽略的对象Id，在修改的时候会用到*/)
        {
            var user = await userManager.FindByFullNameAsync(fullname);
            return Valid(id, user);
        }
        [HttpGet]
        public async Task<JsonResult> CheckEmail(string email, string id = null/*需要忽略的对象Id，在修改的时候会用到*/)
        {
            var user = await userManager.FindByEmailAsync(email);
            return Valid(id, user);
        }
        [HttpGet]
        public async Task<JsonResult> CheckPhoneNumber(string phoneNumber, string id = null/*需要忽略的对象Id，在修改的时候会用到*/)
        {
            var user = await userManager.FindByPhoneNumberAsync(phoneNumber);
            return Valid(id, user);
        }

        private JsonResult Valid(string id, User user)
        {
            if (user != null)
            {
                if (user.Id == id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            return Json(true);
        }
        #endregion

        #region Helpers
        private IDictionary<string, IEnumerable<Permission>> GetInstalledPermissions()
        {
            var installedPermissions = new Dictionary<string, IEnumerable<Permission>>();
            foreach (var permissionProvider in permissionProviders)
            {
                var feature = typeFeatureProvider.GetFeatureForDependency(permissionProvider.GetType());
                var featureName = feature.Id;
                var permissions = permissionProvider.GetPermissions();
                foreach (var permission in permissions)
                {
                    var category = permission.Category;

                    string title = String.IsNullOrWhiteSpace(category) ? featureName : category;

                    if (installedPermissions.ContainsKey(title))
                    {
                        installedPermissions[title] = installedPermissions[title].Concat(new[] { permission });
                    }
                    else
                    {
                        installedPermissions.Add(title, new[] { permission });
                    }
                }
            }

            return installedPermissions;
        }


        private void IdentityResultToAjaxResponse(AjaxResponse ajaxResponse, IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ajaxResponse.Errors.Add(new ErrorInfo() {/*Code = error.Code,*/Message = error.Description });
            }
        }
        //private void IdentityResultToAjaxResponse<TResult>(AjaxResponse<TResult> ajaxResponse, IdentityResult result)
        //{
        //    foreach (var error in result.Errors)
        //    {
        //        ajaxResponse.Errors.Add(new ErrorInfo() {/*Code = error.Code,*/Message = error.Description });
        //    }
        //}
        private void AddErrorsToAjaxResponse(AjaxResponse ajaxResponse)
        {
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    ajaxResponse.Errors.Add(new ErrorInfo() {/*Code = error.Code,*/Message = error.ErrorMessage });
                    break; //只取第一条
                }
            }
            // ModelState.SelectMany(c => c.Value.Errors.Select(d => d.ErrorMessage));
        }

        //private AjaxResponse<TResult> CreateAjaxResponse<TResult>(TResult result)
        //{
        //    return new AjaxResponse<TResult>(result);
        //}
        private AjaxResponse CreateAjaxResponse()
        {
            return new AjaxResponse();
        }

        private string GetAppHomeUrl()
        {
            return Url.Action("Index", "Home"/*, new { area = "Admin" }*/);
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        #endregion
    }
}

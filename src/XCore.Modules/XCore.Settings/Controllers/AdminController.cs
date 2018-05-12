using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using System.Threading.Tasks;
using XCore.DisplayManagement.ModelBinding;
using XCore.Settings.ViewModels;

namespace XCore.Settings.Controllers
{
    public class AdminController : Controller, IUpdateModel
    {
 
        private readonly ISiteService _siteService;
 
        private readonly IAuthorizationService _authorizationService;

        public AdminController(
            ISiteService siteService,
 
            IAuthorizationService authorizationService,
 
            IHtmlLocalizer<AdminController> h)
        {
 
            _siteService = siteService;
 
            _authorizationService = authorizationService;
            H = h;
        }

        IHtmlLocalizer H { get; set; }

        public async Task<IActionResult> Index(string groupId)
        {
            //if (!await _authorizationService.AuthorizeAsync(User, Permissions.ManageGroupSettings, (object) groupId))
            //{
            //    return Unauthorized();
            //}

            var site = await _siteService.GetSiteSettingsAsync();

            var viewModel = new AdminIndexViewModel();

            viewModel.GroupId = groupId;
            //viewModel.Shape = await _siteSettingsDisplayManager.BuildEditorAsync(site, this, false, groupId);

            return View(viewModel);
        }

        //[HttpPost]
        //[ActionName(nameof(Index))]
        //public async Task<IActionResult> IndexPost(string groupId)
        //{
        //    if (!await _authorizationService.AuthorizeAsync(User, Permissions.ManageGroupSettings, (object)groupId))
        //    {
        //        return Unauthorized();
        //    }

        //    var cachedSite = await _siteService.GetSiteSettingsAsync();

        //    // Clone the settings as the driver will update it and as it's a globally cached object
        //    // it would stay this way even on validation errors.

        //    var site = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(cachedSite, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }), cachedSite.GetType()) as ISite;

        //    var viewModel = new AdminIndexViewModel();

        //    viewModel.GroupId = groupId;
        //    viewModel.Shape = await _siteSettingsDisplayManager.UpdateEditorAsync(site, this, false, groupId);

        //    if (ModelState.IsValid)
        //    {
        //        await _siteService.UpdateSiteSettingsAsync(site);

        //        _notifier.Success(H["Site settings updated successfully."]);

        //        return RedirectToAction(nameof(Index), new { groupId });
        //    }

        //    return View(viewModel);
        //}
    }
}
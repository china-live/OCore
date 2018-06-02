using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OCore.Tree;
using OCore.Article;
using OCore.Mvc.Admin.Models;
using OCore.Web.Common;

namespace OCore.Mvc.Admin.Controllers
{
    [Authorize]
    public class VideoController : Controller
    {
        private readonly TencentVodManager<TencentVod> vodManager;
        private readonly ILogger _logger;


        public VideoController(
            TencentVodManager<TencentVod> vodManager,
            ILogger<HomeController> logger)
        {
            this.vodManager = vodManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var article = vodManager.TencentVods.Where(c => c.Id > 0).ToList();

            return View(article);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var vm = new CreateAndEditVideoViewModel();
            vm.CreateTime = DateTime.Now;

            if (id > 0)
            {
                var video = await vodManager.FindByIdAsync(id);
                if (video != null)
                {
                    vm.Id = video.Id;
                    vm.Title = video.Title;
                    vm.Description = video.Description;
                    vm.CoverImg = video.CoverImg;
                    vm.CategoryName = video.CategoryName;
                    vm.Status = video.Status;
                    vm.AppId = video.AppId;
                    vm.FileId = video.FileId;
                    vm.CreateTime = video.CreateTime;
                    vm.IsHot = video.IsHot;
                    vm.IsRed = video.IsRed;
                    vm.IsTop = video.IsTop;
                    vm.Click = video.Click;
                    vm.LikeCount = video.LikeCount;
                    vm.Sort = video.Sort;
                    vm.CreateTime = video.CreateTime;
                }
            }

            ViewData["Categorys"] = GetCategorys();

            return View(vm);
        }

        private List<SelectListItem> GetCategorys()
        {
            VideoCategorys categorys = new VideoCategorys();
            return categorys.GetNodes().Select(c => new SelectListItem() { Text = c.FullName, Value = c.FullName }).ToList();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Edit(CreateAndEditVideoViewModel vm)
        {
            var ajaxResponse = CreateAjaxResponse();
            ajaxResponse.TargetUrl = vm.Id.ToString();

            if (ModelState.IsValid)
            {
                if (vm.Id > 0)
                {
                    var video = await vodManager.FindByIdAsync(vm.Id);
                    if (video != null)
                    {
                        video.AppId = vm.AppId;
                        video.FileId = vm.FileId;
                        video.IsHot = vm.IsHot;
                        video.IsRed = vm.IsRed;
                        video.IsTop = vm.IsTop;
                        video.LikeCount = vm.LikeCount;
                        video.Click = vm.Click;
                        video.CreateTime = vm.CreateTime;
                        video.Sort = vm.Sort;
                        video.Title = vm.Title;
                        video.Description = vm.Description;
                        video.CoverImg = vm.CoverImg;
                        video.CategoryName = vm.CategoryName;
                        video.Status = vm.Status ?? 0;

                        var r = await vodManager.UpdateAsync(video);
                        if (!r.Succeeded)
                        {
                            _logger.LogWarning("修改视频失败");
                            ajaxResponse.Errors.Add(new ErrorInfo(-1, r.Errors.FirstOrDefault()?.Description ?? "修改失败"));
                        }
                    }
                    else
                    {
                        var tempId = await CreateVideo(vm, ajaxResponse);
                        if (tempId > 0)
                        {
                            ajaxResponse.TargetUrl = tempId.ToString();
                        }
                    }
                }
                else
                {
                    var tempId = await CreateVideo(vm, ajaxResponse);
                    if (tempId > 0)
                    {
                        ajaxResponse.TargetUrl = tempId.ToString();
                    }
                }
            }
            else
            {
                AddErrorsToAjaxResponse(ajaxResponse);
            }

            return Json(ajaxResponse);
        }

        private async Task<int> CreateVideo(CreateAndEditVideoViewModel vm, AjaxResponse ajaxResponse)
        {
            var article = new TencentVod()
            {
                Title = vm.Title,
                Description = vm.Description,
                CoverImg = vm.CoverImg,

                CategoryName = vm.CategoryName,
                Status = vm.Status ?? 0,

                Sort = vm.Sort,
                AppId = vm.AppId,
                FileId = vm.FileId,
                Click = vm.Click,
                IsHot = vm.IsHot,
                IsTop = vm.IsTop,
                IsRed = vm.IsRed,
                LikeCount = vm.LikeCount,

                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            };
            var r = await vodManager.CreateAsync(article);
            if (r.Succeeded)
            {
                return article.Id;
            }

            _logger.LogWarning("添加视频失败");
            ajaxResponse.Errors.Add(new ErrorInfo(-1, r.Errors.FirstOrDefault()?.Description ?? "添加失败"));
            return 0;
        }

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
        }

        private AjaxResponse CreateAjaxResponse()
        {
            return new AjaxResponse();
        }
    }
}

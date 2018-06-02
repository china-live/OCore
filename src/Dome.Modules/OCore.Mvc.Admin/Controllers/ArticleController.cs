using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using OCore.Tree;
using OCore.Article;
using OCore.Common;
using OCore.Mvc.Admin.Models;
using OCore.Web.Common;

namespace OCore.Mvc.Admin.Controllers
{
    [Authorize]
    public class ArticleController : Controller
    {
        private readonly ArticleManager<Article.Article> _articleManager;
        private readonly ILogger _logger;
        private readonly IHostingEnvironment env;
        //private readonly ArticleOptions newsOptions = Categorys.cwNews.FullName; // new ArticleOptions() { CategoryName = "春晚/新闻动态" };


        public ArticleController(
            ArticleManager<Article.Article> articleManager,
            ILogger<HomeController> logger, IHostingEnvironment env)
        {
            _articleManager = articleManager;
            _logger = logger;
            this.env = env;
        }

        public IActionResult Index()
        {
            var article = _articleManager.Articles.Where(c => c.Id > 0).ToList();

            return View(article);
        }

        public IActionResult Uploaded()
        {
            return View();
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> UploadFilesAjaxAsync()
        {
            long size = 0;
            var files = Request.Form.Files;
            var updateDirectory = Path.Combine(env.WebRootPath, "updates");
            string message = "";
            if (!Directory.Exists(updateDirectory))
            {
                Directory.CreateDirectory(updateDirectory);
            }

            foreach (var file in files)
            {
                var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');

                filename = Path.Combine(updateDirectory,filename);
                size += file.Length;
 
                await Task.Run(() =>
                {
                    using (FileStream fs = System.IO.File.Create(filename))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                });

                message = Url.Content(Path.Combine(filename.Replace(env.WebRootPath, "").Replace("\\","/")));
            }
            
            return Json(message);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var vm = new CreateAndEditArticleViewModel();

            if (id > 0)
            {
                var article = await _articleManager.FindByIdAsync(id);
                if (article != null)
                {
                    vm.Id = article.Id;
                    vm.Title = article.Title;
                    vm.Content = Base64.base64Encode(article.Content);
                    vm.Description = article.Description;
                    vm.Source = article.Source;
                    vm.CoverImg = article.CoverImg;
                    vm.CategoryName = article.CategoryName;
                    vm.Status = article.Status;
                }
            }

            ViewData["Categorys"] = GetCategorys();

            return View(vm);
        }

        private List<SelectListItem> GetCategorys()
        {
            ArticleCategorys categorys = new ArticleCategorys();
            return categorys.GetNodes().Select(c => new SelectListItem() { Text = c.FullName, Value = c.FullName }).ToList();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Edit(CreateAndEditArticleViewModel vm)
        {
            var ajaxResponse = CreateAjaxResponse();
            ajaxResponse.TargetUrl = vm.Id.ToString();

            if (ModelState.IsValid)
            {
                if (vm.Id > 0)
                {
                    var article = await _articleManager.FindByIdAsync(vm.Id);
                    if (article != null)
                    {
                        article.Title = vm.Title;
                        article.Description = vm.Description;
                        article.Content = Base64.base64Decode(vm.Content);
                        article.Source = vm.Source;
                        article.CoverImg = vm.CoverImg;
                        article.CategoryName = vm.CategoryName;
                        article.Status = vm.Status ?? 0;

                        var r = await _articleManager.UpdateAsync(article);
                        if (!r.Succeeded)
                        {
                            _logger.LogWarning("修改文章失败");
                            ajaxResponse.Errors.Add(new ErrorInfo(-1, r.Errors.FirstOrDefault()?.Description ?? "修改文章失败"));
                        }

                    }
                    else
                    {
                        var tempId = await CreateArticle(vm, ajaxResponse);
                        if (tempId > 0)
                        {
                            ajaxResponse.TargetUrl = tempId.ToString();
                        }
                    }
                }
                else
                {
                    var tempId = await CreateArticle(vm, ajaxResponse);
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

        private async Task<int> CreateArticle(CreateAndEditArticleViewModel vm, AjaxResponse ajaxResponse)
        {
            var article = new Article.Article()
            {
                Title = vm.Title,
                Content = Base64.base64Decode(vm.Content),
                Description = vm.Description,
                Source = vm.Source,
                CoverImg = vm.CoverImg,

                CategoryName = vm.CategoryName,
                Status = vm.Status ?? 0,

                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            };
            var r = await _articleManager.CreateAsync(article);
            if (r.Succeeded)
            {
                return article.Id;
            }

            _logger.LogWarning("添加文章失败");
            ajaxResponse.Errors.Add(new ErrorInfo(-1, r.Errors.FirstOrDefault()?.Description ?? "添加文章失败"));
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using OCore.Mvc.Admin.Models;

namespace OCore.Mvc.Admin.Views.Shared.Components
{
    public class SideBarViewComponent : ViewComponent
    {
        //private readonly IUserNavigationManager _userNavigationManager;
        //private readonly IAbpSession _abpSession;
        private readonly IViewRenderService viewRenderService;

        public SideBarViewComponent(
            /*IUserNavigationManager userNavigationManager*/
            IViewRenderService viewRenderService)
        {
            //_userNavigationManager = userNavigationManager;
            this.viewRenderService = viewRenderService;
        }

        public  async Task<IViewComponentResult> InvokeAsync(string currentPageName = null)
        {
            NavManage navObj = new NavManage();

            //var navString = CacheManage.GetNavCache();
            //if (navString == null)
            //{
            //    navString = ViewHelper.RenderPartialViewToString(this, "_navTemplate");
            //    CacheManage.SetNavCache(navString);
            //}
            var navString = await viewRenderService.RenderToStringAsync(this.ViewContext, "_NavTemplate", currentPageName);
            //var navString = ViewHelper.RenderPartialViewToString(this, "_navTemplate");

            navObj.Read(navString);
            List<Nav> navList = navObj.ToViewList();

            //ViewBag.CurrentPageName  = currentPageName;

            var sidebarModel = new SidebarViewModel
            {
                Menu = navList,
                CurrentPageName = currentPageName
            };

            return View("Default", sidebarModel);
        }
    }

    //public class CacheManage
    //{
    //    private const string nav_string_key = "seeeoojfoeoeifeoe";

    //    public static string GetNavCache()
    //    {
    //        return CacheHelper.GetCache(nav_string_key) as string;
    //    }

    //    public static void SetNavCache(string nav)
    //    {
    //        CacheHelper.SetCache(nav_string_key, nav);
    //    }
    //}

    public class NavManage
    {
        private XElement navTree = null;
        public string rootNode = "div";
        public string tagName = "nav";
        public string navId = "id";
        public string navName = "name";

        #region Read & Create
        public void Read(string xmlString)
        {
            var tree = XElement.Parse(xmlString);
            if (tree == null || tree.Name != this.rootNode)
            {
                throw new ArgumentException("指定的字符串不能解析成合法的菜单树");
            }
            navTree = tree;
        }
        public void Create()
        {
            navTree = new XElement(this.rootNode);
        }
        #endregion

        public void Add(string navId, string navName, IEnumerable<KeyValuePair<string, string>> attrList, string pid = null)
        {
            if (navTree == null || navTree.Name != this.rootNode)
            {
                throw new ArgumentException("请先使用Read方法读取或Create方法创建一个菜单树实例");
            }

            var nav = new XElement(this.tagName);
            nav.Add(new XAttribute(this.navId, navId));
            nav.Add(new XAttribute(this.navName, navName));
            foreach (var item in attrList)
            {
                nav.Add(new XAttribute(item.Key, item.Value));
            }

            if (pid != null)
            {
                var pNode = Find(pid);
                if (pNode == null)
                {
                    throw new ArgumentException("指定的父节点不存在");
                }
                pNode.Add(nav);
                return;
            }

            var tempNode = navTree.Element(this.rootNode);
            tempNode.Add(nav);
        }

        public List<Nav> ToViewList()
        {
            var query = from nav in navTree.DescendantsAndSelf(this.tagName)
                        select nav;

            Nav navView = null;
            List<Nav> list = new List<Nav>();
            foreach (var item in query)
            {
                navView = new Nav();
                int id = (int)item.Attribute(this.navId);
                var pid = GetParent(id.ToString());

                navView.id = id;
                navView.text = item.Attribute(this.navName).Value;
                navView.depth = GetDepth(navView.id.ToString());
                if (pid != null)
                {
                    navView.pId = Convert.ToInt32(pid);
                }
                navView.icon = GetAttr(navView.id.ToString(), "icon");
                navView.href = GetAttr(navView.id.ToString(), "href");
                navView.parents = GetParents(navView.id.ToString());
                navView.visible = GetAttr(navView.id.ToString(), "visible") != "false" ? true : false;

                list.Add(navView);
            }
            return list;
        }

        public string GetParent(string id)
        {
            var p = Find(id);
            if (p == null)
            {
                throw new ArgumentException("指定的节点不存在");
            }

            foreach (var item in p.Ancestors(this.tagName))
            {
                return item.Attribute(this.navId).Value;
            }

            return null;
        }
        public string GetParents(string id)
        {
            var p = Find(id);
            if (p == null)
            {
                throw new ArgumentException("指定的节点不存在");
            }

            List<string> list = new List<string>();
            foreach (var item in p.Ancestors(this.tagName))
            {
                list.Add(item.Attribute(this.navId).Value);
            }

            return string.Join(",", list);
        }

        public int GetDepth(string id)
        {
            var p = Find(id);
            if (p == null)
            {
                throw new ArgumentException("指定的节点不存在");
            }
            return p.AncestorsAndSelf(this.tagName).Count();
        }

        /// <summary>
        /// 根据Id找出节点（如果找到多个，只返回第一个，没找到返回null）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private XElement Find(string id)
        {
            var query = from nav in navTree.DescendantsAndSelf(this.tagName)
                        where (string)nav.Attribute(this.navId) == id
                        select nav;
            foreach (var item in query)
            {
                return item;
            }
            return null;
        }

        private string GetAttr(string id, string attrName)
        {
            var p = Find(id);
            if (p == null)
            {
                throw new ArgumentException("指定的节点不存在");
            }
            return p.Attribute(attrName)?.Value;
        }
    }

    public class Nav
    {
        /// <summary>
        /// 导航菜单Id，不能重复
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 父级节点Id
        /// </summary>
        public int? pId { get; set; }
        /// <summary>
        /// 显示的文字
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 图标名称
        /// </summary>
        public string icon { get; set; }

        public int depth { get; set; }

        public bool active { get; set; }
        public bool open { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool visible { get; set; }

        /// <summary>
        /// 单击时打开的目标页面地址
        /// </summary>
        public string href { get; set; }

        /// <summary>
        /// 父级节点Id列表(不包含当前节点Id)
        /// </summary>
        public string parents { get; set; }
    }
}

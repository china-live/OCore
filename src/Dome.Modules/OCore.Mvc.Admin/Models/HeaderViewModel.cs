using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OCore.Mvc.Admin.Views.Shared.Components;

namespace OCore.Mvc.Admin.Models
{
    public class HeaderViewModel
    {
        public GetCurrentLoginInformationsOutput LoginInformations { get; set; }

        public string GetLogoUrl(string appPath = null)
        {
            return appPath + "OCore.Mvc.Admin/img/logo.png";
        }

        public string GetShownLoginName()
        {
            var userName = "<span id=\"HeaderCurrentUserName\">" + LoginInformations.User.UserName + "</span>";

            return userName;
        }
    }

    public class GetCurrentLoginInformationsOutput {
        public UserLoginInfoDto User { get; set; }
    }

    public class UserLoginInfoDto {
            public string Id { get; set; }
            public string FullName { get; set; }

            public string UserName { get; set; }

            public string EmailAddress { get; set; }
 
    }

    public class SidebarViewModel
    {
        public List<Nav> Menu { get; set; }
        public List<Nav> TempMenu { get; set; }
        public string CurrentPageName { get; set; }
    }
}

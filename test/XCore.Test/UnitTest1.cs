using Moq;
using System;
using XCore.Settings.Services;
using Xunit;

namespace XCore.Test
{
    public class UnitTest1
    {
        private Settings.SiteSettings settings = new Settings.SiteSettings(){ Id = 1, SiteName = "test" };

        [Fact]
        public void Test1()
        {
            var fakeObject = new Mock<ISiteSettingStore>();
            fakeObject.Setup(x => x.CreateAndUpdateAsync(It.IsAny<Settings.SiteSettings>()));
            var res = fakeObject.Object.CreateAndUpdateAsync(settings);
            //Assert.Equal<bool>(true, res);
            var m = fakeObject.Object.GetSiteSettingsAsync();
            
        }
    }
}

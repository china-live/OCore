# 关于
长久以来,很多.NET开发者都非常喜欢Orchard这个项目,但是由于Orchard模块众多，加上本身也有一定的复杂度，需要对ASP.NET底层和ASP.NET MVC框架有相当的了解，所以关注的人虽多，但实际使用者甚少（当然，这只是在国内）。  

随着.NET Core的发布，OrchardCore复杂度大大降低了，并且OrchardCore相对Orchard来讲，模块化得更加彻底，连基础框架也实现了模块化，这样一来，选择性将会更加灵活。 

但是它毕竟是一个以CMS为主的项目，很多模块都是为CMS量身定做的，要想把它当作一个通用的Web开发框架来用，还需要作少许修改。
 
# 目标
打造一个轻量级、通用的、模块化的、多租户的Web应用框架,并且要很方便的和其它原生态的.Net Core类库集成。
>  关于多租户，OrchardCore中的多租户结构是采用的“单一部署-多个数据库”的结构，也就是说每一个租户都有一个独立的数据库，租户与租户之间是完全隔离的，也没有Host和Tenant之分。

# 源码结构介绍（文件目录）
> - src  
>   - Dome（示例项目主程序集）  
>   - Dome.Modules（作用和XCore.Modules一样，没有任何区别）  
>   - XCore（核心模块，大多数都是必须，具体见下面重要项目/模块介绍）  
>   - XCore.Build（该文件夹用来放置一些公用的MSBuild片段，用来统一管理项目属性，例如：版本号、引用的第三方NuGet包版本等等）  
>   - XCore.Modules（功能模块，非必须，可根据实际需要修改）  
>   - XCore.Themes（主题模块，非必须，可根据实际需要修改）  

# 重要模块/项目介绍
## 核心项目（必需）
* XCore.Environment.Extensions.Abstractions
* XCore.Environment.Extensions
* XCore.Environment.Shell.Abstractions
* XCore.Environment.Shell
* XCore.Modules.Abstractions
* XCore.Modules
## 与应用框架集成的适配项目（目前只移植了MVC，OrchadrCore还支持Nancy）
* XCore.Mvc.Abstractions
* XCore.Mvc.Core
## Targets项目（可以简单的理解为“快捷方式”，用来简化模块或应用开发时引入依赖项目和环境配置等工作）
* XCore.Module.Targets
> 引用该项目表示当前项目是一个XCore的功能模块。
* XCore.Theme.Targets
> 引用该项目表示当前项目是一个XCore主题。
* XCore.Application.Targets
> 引用该项目表示当前项目是一个ASP.NET Core应用程序，一般只做测试用，实际项目中不会直接引用它。
* XCore.Application.Mvc.Targets
> 引用该项目表示当前项目是一个ASP.NeT Core MVC应用，该项目建立在XCore.Application.Targets之上。

## 基础项目（核心项目依赖项目，也算必需）
* XCore.Parser.Yaml
* XCore.DeferredTasks.Abstractions

>以上项目是XCore(OrchardCore)的核心项目，在移植过程中会尽量保持代码不变（为了命名统一，项目名和命名空间全部更该改）以达到最大兼容的目的。
修改的地方（详情请查看项目README.md文件）：
1.修改 XCore.Environment.Shell.Abstractions/XCore.Environment.Shell 以适配EF Core 
2.修改 XCore.Mvc.Core 服务注册AddMvcModules返回IServiceCollection为MvcBuilder
3.XCore.Modules.ModularServiceCollectionExtensions.cs 中添加一个WithConfiguration方法

## 辅助、功能增强
* XCore.Common
* XCore.Linq
* XCore.Web.Common
* XCore.Logging.NLog
* XCore.DeferredTasks
* XCore.BackgroundTasks
* XCore.BackgroundTasks.Abstractions
* XCore.Environment.Cache
* XCore.Environment.Cache.Abstractions
* XCore.Entities
* XCore.EntityFrameworkCore
* XCore.Environment.Shell.EntityFrameworkCore

## 一些基本的业务模块
* XCore.DeferredTasks（闲置）
* XCore.BackgroundTasks.Abstractions（闲置）
* XCore.BackgroundTasks（闲置）
* XCore.Navigation（闲置）
* XCore.Security.Abstractions（闲置）
* XCore.Security（闲置）
* XCore.Mvc.Authorization（废弃）
* XCore.ResourceManagement.Abstractions（闲置）
* XCore.ResourceManagement（闲置）

> 标明（闲置）的模块表示从OrchardCore移植过来，但还未使用过，包括在该项目的示例Demo中也未经验证过。
 

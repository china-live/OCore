# 关于XCore
一直以来,很多.NET开发者都非常喜欢Orchard这个项目,但是由于Orchard模块众多，加上本身也有一定的复杂度，需要对ASP.NET底层和ASP.NET MVC框架有相当的了解，所以关注的人虽多，但实际使用者甚少。  

随着.NET Core的发布，OrchardCore复杂度大大降低了，并且OrchardCore相对Orchard来讲，模块化得更加彻底，连基础框架也实现了模块化，这样一来，选择性将会更加灵活。 

>  基于.NET Framework的Orchard的基础框架并没有完全实现模块化，核心框架项目（Orchard.Framework）中还是封装了很多属于业务逻辑的东西，而OrchardCore将这些也都独立成一个个单独的模块了。

但是它毕竟是一个以CMS为主的项目，很多模块都是为CMS量身定做的，要想把它当作一个通用的Web开发框架来用，还需要作少许修改。所以，本项目诞生了...  

# 项目定位及目标：基于**OrchardCore**的**模块化**的**Web开发框架**。

  1. OrchardCore
     >  XCore核心框架及代码绝大部分直接来源自OrchardCore核心框架，有少许修改，但会最大限度与OrchardCore保持一致。至于为什么选用OrchardCore？这是基于多方面原因的选择结果。一个重要的原因，它是由微软官方发起并主导的开源项目，技术方向上紧跟着微软ASP.NET Core路线。另一个重要原因，它是完全基于.NET Core的，轻装上阵没有厚重的历史包袱，不用在费心费力的去考虑兼容性，而且.NET Core本身模块化就做得非常好。当然还有很多其他原因这里就不一一描述了。
    
  2. 模块化
     >  模块化的开发框架很多，但是大多数的框架都有一个很笨重的核心模块。每次看到它的时候就很不爽，总有一种想把它集成的那些无用的东西剔出去冲动。这还是小问题，虽然不爽，还是可以接受，但一部分框架使用后会影响整个项目后面的开发流程，这个就很严重了。这就相当于你在主流的.NET开发路线外选择了一条偏僻小路前进，这是很危险的选择，因为这就意味着你失去了众多同伴和官方技术支持，在遇到问题的时候，你会陷入“叫天天不应，叫地地不灵”的尴尬境地。

  3. Web开发框架
     >  我们知道OrchardCore是基于ASP.NET Core的开源项目，XCore作为基于OrchardCore的开发框架，当然也是需要依赖ASP.NET Core的。我们还知道，ASP.NET Core是Web开发框架，所以道理你懂的，正好应了句俗语———龙生龙，凤生凤，老鼠的儿子会......。特别说明一点，MVC也只是模块而已（ASP.NET Core MVC），并不是必须选项，你完全可以使用其他框架像Nancy什么的（Nancy模块目前还没移植过来，MVC是唯一的选择）。

  4. 多租户
     >  这一点我在标题中没有提，因为到目前为止，还是被阉割状态，没有移植过来，后面会补上。

# 模块介绍
## 核心框架模块（必需）
* XCore.Environment.Extensions.Abstractions
* XCore.Environment.Extensions
* XCore.Environment.Shell.Abstractions
* XCore.Environment.Shell
* XCore.Modules.Abstractions
* XCore.Modules
## 核心应用模块（虽然不是必需，但也是多选一或多选多的选择）
* XCore.Mvc.Abstractions
* XCore.Mvc.Core
## 基础模块（被核心框架模块所依赖，也算必需）
* XCore.Parser.Yaml
* XCore.DeferredTasks.Abstractions（闲置）
## 辅助模块
* XCore.Common
* XCore.Linq
* XCore.Web.Common
## 一些基本的业务模块
* XCore.DeferredTasks（闲置）
* XCore.BackgroundTasks.Abstractions（闲置）
* XCore.BackgroundTasks（闲置）
* XCore.Entities
* XCore.EntityFrameworkCore
* XCore.Navigation（闲置）
* XCore.Security.Abstractions（闲置）
* XCore.Security（闲置）
* XCore.Mvc.Authorization（废弃）
* XCore.ResourceManagement.Abstractions（）
* XCore.ResourceManagement（闲置）

> 标明（闲置）的模块表示从OrchardCore移植过来，但还未使用过，包括在该项目的示例Demo中也未经验证过。
- - - -
下面这些模块虽然也叫模块，但是和上面列举的有一点小小的区别。






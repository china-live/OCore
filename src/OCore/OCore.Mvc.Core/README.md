# 2018/04/11

同步到OCore dev分支，添加了一些中文注释而已

修改：将ModularServiceCollectionExtensions.AddMvcModules返回IServiceCollection修改为MvcBuilder，更改后和mvc源码一致，方便扩展。
例如：
>services.AddMvcModules(_applicationServices).AddSessionStateTempDataProvider();

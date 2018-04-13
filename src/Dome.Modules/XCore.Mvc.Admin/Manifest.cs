using XCore.Modules.Manifest;

[assembly: Module(
    Name = "MvcAdmin",
    Author = "ldming",
    Website = "http://www.ldming.com",
    Version = "1.0.0",
    Description = "XCore MVC Admin",
    Category = "Demo",
    Dependencies = new[]
    {
        "XCore.Mvc",
        "XCore.Admin"
    }
)]

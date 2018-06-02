using OCore.Modules.Manifest;

[assembly: Module(
    Name = "MvcAdmin",
    Author = "ldming",
    Website = "http://www.ldming.com",
    Version = "1.0.0",
    Description = "OCore MVC Admin",
    Category = "Demo",
    Dependencies = new[]
    {
        "OCore.Mvc",
        "OCore.Admin"
    }
)]

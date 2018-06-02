using OCore.Modules.Manifest;

[assembly: Module(
    Name = "Migrator",
    Author = "ldming",
    Website = "http://www.ldming.com",
    Version = "1.0.0",
    Description = "OCore MVC Admin",
    Category = "Infrastructure",
    Dependencies = new[]
    {
        "OCore.EntityFrameworkCore"
    }
)]

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OCore.Migrator.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OCore_Articles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(nullable: true),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Source = table.Column<string>(maxLength: 256, nullable: true),
                    CoverImg = table.Column<string>(nullable: true),
                    SeoTitle = table.Column<string>(nullable: true),
                    SeoKeywords = table.Column<string>(nullable: true),
                    SeoDescription = table.Column<string>(nullable: true),
                    Tags = table.Column<string>(nullable: true),
                    Click = table.Column<int>(nullable: false),
                    LikeCount = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    IsTop = table.Column<bool>(nullable: false),
                    IsRed = table.Column<bool>(nullable: false),
                    IsHot = table.Column<bool>(nullable: false),
                    Sort = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCore_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OCore_Roles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCore_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OCore_TencentVods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileId = table.Column<string>(nullable: true),
                    AppId = table.Column<string>(nullable: true),
                    CategoryName = table.Column<string>(nullable: true),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CoverImg = table.Column<string>(nullable: true),
                    Click = table.Column<int>(nullable: false),
                    LikeCount = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    IsTop = table.Column<bool>(nullable: false),
                    IsRed = table.Column<bool>(nullable: false),
                    IsHot = table.Column<bool>(nullable: false),
                    Sort = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCore_TencentVods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OCore_Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCore_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExecutionId = table.Column<string>(nullable: true),
                    JsonValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShellDescriptor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SerialNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShellDescriptor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShellState",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShellState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BaseUrl = table.Column<string>(nullable: true),
                    Calendar = table.Column<string>(nullable: true),
                    Culture = table.Column<string>(nullable: true),
                    MaxPagedCount = table.Column<int>(nullable: false),
                    MaxPageSize = table.Column<int>(nullable: false),
                    PageSize = table.Column<int>(nullable: false),
                    ResourceDebugMode = table.Column<int>(nullable: false),
                    SiteName = table.Column<string>(nullable: true),
                    SiteSalt = table.Column<string>(nullable: true),
                    SuperUser = table.Column<string>(nullable: true),
                    TimeZone = table.Column<string>(nullable: true),
                    UseCdn = table.Column<bool>(nullable: false),
                    HomeRoute_Area = table.Column<string>(nullable: true),
                    HomeRoute_Controller = table.Column<string>(nullable: true),
                    HomeRoute_Action = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OCore_RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCore_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OCore_RoleClaims_OCore_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "OCore_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OCore_UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCore_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OCore_UserClaims_OCore_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "OCore_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OCore_UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCore_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_OCore_UserLogins_OCore_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "OCore_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OCore_UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCore_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_OCore_UserRoles_OCore_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "OCore_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OCore_UserRoles_OCore_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "OCore_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OCore_UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCore_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_OCore_UserTokens_OCore_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "OCore_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShellFeature",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ShellDescriptorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShellFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShellFeature_ShellDescriptor_ShellDescriptorId",
                        column: x => x.ShellDescriptorId,
                        principalTable: "ShellDescriptor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShellParameter",
                columns: table => new
                {
                    Component = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: false),
                    ShellDescriptorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShellParameter", x => new { x.Name, x.Value, x.Component });
                    table.ForeignKey(
                        name: "FK_ShellParameter_ShellDescriptor_ShellDescriptorId",
                        column: x => x.ShellDescriptorId,
                        principalTable: "ShellDescriptor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShellFeatureState",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    InstallState = table.Column<int>(nullable: false),
                    EnableState = table.Column<int>(nullable: false),
                    ShellStateId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShellFeatureState", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShellFeatureState_ShellState_ShellStateId",
                        column: x => x.ShellStateId,
                        principalTable: "ShellState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OCore_RoleClaims_RoleId",
                table: "OCore_RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "OCore_Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OCore_UserClaims_UserId",
                table: "OCore_UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OCore_UserLogins_UserId",
                table: "OCore_UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OCore_UserRoles_RoleId",
                table: "OCore_UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "OCore_Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "OCore_Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ShellFeature_ShellDescriptorId",
                table: "ShellFeature",
                column: "ShellDescriptorId");

            migrationBuilder.CreateIndex(
                name: "IX_ShellFeatureState_ShellStateId",
                table: "ShellFeatureState",
                column: "ShellStateId");

            migrationBuilder.CreateIndex(
                name: "IX_ShellParameter_ShellDescriptorId",
                table: "ShellParameter",
                column: "ShellDescriptorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OCore_Articles");

            migrationBuilder.DropTable(
                name: "OCore_RoleClaims");

            migrationBuilder.DropTable(
                name: "OCore_TencentVods");

            migrationBuilder.DropTable(
                name: "OCore_UserClaims");

            migrationBuilder.DropTable(
                name: "OCore_UserLogins");

            migrationBuilder.DropTable(
                name: "OCore_UserRoles");

            migrationBuilder.DropTable(
                name: "OCore_UserTokens");

            migrationBuilder.DropTable(
                name: "Recipe");

            migrationBuilder.DropTable(
                name: "ShellFeature");

            migrationBuilder.DropTable(
                name: "ShellFeatureState");

            migrationBuilder.DropTable(
                name: "ShellParameter");

            migrationBuilder.DropTable(
                name: "SiteSettings");

            migrationBuilder.DropTable(
                name: "OCore_Roles");

            migrationBuilder.DropTable(
                name: "OCore_Users");

            migrationBuilder.DropTable(
                name: "ShellState");

            migrationBuilder.DropTable(
                name: "ShellDescriptor");
        }
    }
}

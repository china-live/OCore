using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace XCore.Migrator.Migrations
{
    public partial class addTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    HomeRoute_Action = table.Column<string>(nullable: true),
                    HomeRoute_Area = table.Column<string>(nullable: true),
                    HomeRoute_Controller = table.Column<string>(nullable: true),
                    MaxPageSize = table.Column<int>(nullable: false),
                    MaxPagedCount = table.Column<int>(nullable: false),
                    PageSize = table.Column<int>(nullable: false),
                    ResourceDebugMode = table.Column<int>(nullable: false),
                    SiteName = table.Column<string>(nullable: true),
                    SiteSalt = table.Column<string>(nullable: true),
                    SuperUser = table.Column<string>(nullable: true),
                    TimeZone = table.Column<string>(nullable: true),
                    UseCdn = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "XCore_Articles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(nullable: true),
                    Click = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    CoverImg = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsHot = table.Column<bool>(nullable: false),
                    IsRed = table.Column<bool>(nullable: false),
                    IsTop = table.Column<bool>(nullable: false),
                    LikeCount = table.Column<int>(nullable: false),
                    SeoDescription = table.Column<string>(nullable: true),
                    SeoKeywords = table.Column<string>(nullable: true),
                    SeoTitle = table.Column<string>(nullable: true),
                    Sort = table.Column<int>(nullable: false),
                    Source = table.Column<string>(maxLength: 256, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Tags = table.Column<string>(nullable: true),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XCore_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "XCore_Roles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XCore_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "XCore_TencentVods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppId = table.Column<string>(nullable: true),
                    CategoryName = table.Column<string>(nullable: true),
                    Click = table.Column<int>(nullable: false),
                    CoverImg = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    FileId = table.Column<string>(nullable: true),
                    IsHot = table.Column<bool>(nullable: false),
                    IsRed = table.Column<bool>(nullable: false),
                    IsTop = table.Column<bool>(nullable: false),
                    LikeCount = table.Column<int>(nullable: false),
                    Sort = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XCore_TencentVods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "XCore_Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    FullName = table.Column<string>(maxLength: 256, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XCore_Users", x => x.Id);
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Component = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ShellDescriptorId = table.Column<int>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShellParameter", x => x.Id);
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
                    EnableState = table.Column<int>(nullable: false),
                    InstallState = table.Column<int>(nullable: false),
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

            migrationBuilder.CreateTable(
                name: "XCore_RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XCore_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_XCore_RoleClaims_XCore_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "XCore_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "XCore_UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XCore_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_XCore_UserClaims_XCore_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "XCore_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "XCore_UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XCore_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_XCore_UserLogins_XCore_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "XCore_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "XCore_UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XCore_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_XCore_UserRoles_XCore_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "XCore_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_XCore_UserRoles_XCore_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "XCore_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "XCore_UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XCore_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_XCore_UserTokens_XCore_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "XCore_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_XCore_RoleClaims_RoleId",
                table: "XCore_RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "XCore_Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_XCore_UserClaims_UserId",
                table: "XCore_UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_XCore_UserLogins_UserId",
                table: "XCore_UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_XCore_UserRoles_RoleId",
                table: "XCore_UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "XCore_Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "XCore_Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "XCore_Articles");

            migrationBuilder.DropTable(
                name: "XCore_RoleClaims");

            migrationBuilder.DropTable(
                name: "XCore_TencentVods");

            migrationBuilder.DropTable(
                name: "XCore_UserClaims");

            migrationBuilder.DropTable(
                name: "XCore_UserLogins");

            migrationBuilder.DropTable(
                name: "XCore_UserRoles");

            migrationBuilder.DropTable(
                name: "XCore_UserTokens");

            migrationBuilder.DropTable(
                name: "ShellState");

            migrationBuilder.DropTable(
                name: "ShellDescriptor");

            migrationBuilder.DropTable(
                name: "XCore_Roles");

            migrationBuilder.DropTable(
                name: "XCore_Users");
        }
    }
}

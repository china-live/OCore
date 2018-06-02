using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OCore.Migrator.Migrations
{
    public partial class addtext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "abc");

            migrationBuilder.CreateTable(
                name: "OCore_Recipe",
                schema: "abc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExecutionId = table.Column<string>(nullable: true),
                    JsonValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCore_Recipe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OCore_ShellDescriptor",
                schema: "abc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SerialNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCore_ShellDescriptor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OCore_ShellState",
                schema: "abc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCore_ShellState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OCore_SiteSettings",
                schema: "abc",
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
                    table.PrimaryKey("PK_OCore_SiteSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OCore_OCore_Articles",
                schema: "abc",
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
                    table.PrimaryKey("PK_OCore_OCore_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OCore_OCore_Roles",
                schema: "abc",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCore_OCore_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OCore_OCore_TencentVods",
                schema: "abc",
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
                    table.PrimaryKey("PK_OCore_OCore_TencentVods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OCore_OCore_Users",
                schema: "abc",
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
                    table.PrimaryKey("PK_OCore_OCore_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OCore_ShellFeature",
                schema: "abc",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ShellDescriptorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCore_ShellFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OCore_ShellFeature_OCore_ShellDescriptor_ShellDescriptorId",
                        column: x => x.ShellDescriptorId,
                        principalSchema: "abc",
                        principalTable: "OCore_ShellDescriptor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OCore_ShellParameter",
                schema: "abc",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: false),
                    Component = table.Column<string>(nullable: false),
                    ShellDescriptorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCore_ShellParameter", x => new { x.Name, x.Value, x.Component });
                    table.ForeignKey(
                        name: "FK_OCore_ShellParameter_OCore_ShellDescriptor_ShellDescriptorId",
                        column: x => x.ShellDescriptorId,
                        principalSchema: "abc",
                        principalTable: "OCore_ShellDescriptor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OCore_ShellFeatureState",
                schema: "abc",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    EnableState = table.Column<int>(nullable: false),
                    InstallState = table.Column<int>(nullable: false),
                    ShellStateId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCore_ShellFeatureState", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OCore_ShellFeatureState_OCore_ShellState_ShellStateId",
                        column: x => x.ShellStateId,
                        principalSchema: "abc",
                        principalTable: "OCore_ShellState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OCore_OCore_RoleClaims",
                schema: "abc",
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
                    table.PrimaryKey("PK_OCore_OCore_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OCore_OCore_RoleClaims_OCore_OCore_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "abc",
                        principalTable: "OCore_OCore_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OCore_OCore_UserClaims",
                schema: "abc",
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
                    table.PrimaryKey("PK_OCore_OCore_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OCore_OCore_UserClaims_OCore_OCore_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "abc",
                        principalTable: "OCore_OCore_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OCore_OCore_UserLogins",
                schema: "abc",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCore_OCore_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_OCore_OCore_UserLogins_OCore_OCore_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "abc",
                        principalTable: "OCore_OCore_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OCore_OCore_UserRoles",
                schema: "abc",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCore_OCore_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_OCore_OCore_UserRoles_OCore_OCore_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "abc",
                        principalTable: "OCore_OCore_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OCore_OCore_UserRoles_OCore_OCore_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "abc",
                        principalTable: "OCore_OCore_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OCore_OCore_UserTokens",
                schema: "abc",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCore_OCore_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_OCore_OCore_UserTokens_OCore_OCore_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "abc",
                        principalTable: "OCore_OCore_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OCore_ShellFeature_ShellDescriptorId",
                schema: "abc",
                table: "OCore_ShellFeature",
                column: "ShellDescriptorId");

            migrationBuilder.CreateIndex(
                name: "IX_OCore_ShellFeatureState_ShellStateId",
                schema: "abc",
                table: "OCore_ShellFeatureState",
                column: "ShellStateId");

            migrationBuilder.CreateIndex(
                name: "IX_OCore_ShellParameter_ShellDescriptorId",
                schema: "abc",
                table: "OCore_ShellParameter",
                column: "ShellDescriptorId");

            migrationBuilder.CreateIndex(
                name: "IX_OCore_OCore_RoleClaims_RoleId",
                schema: "abc",
                table: "OCore_OCore_RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "abc",
                table: "OCore_OCore_Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OCore_OCore_UserClaims_UserId",
                schema: "abc",
                table: "OCore_OCore_UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OCore_OCore_UserLogins_UserId",
                schema: "abc",
                table: "OCore_OCore_UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OCore_OCore_UserRoles_RoleId",
                schema: "abc",
                table: "OCore_OCore_UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "abc",
                table: "OCore_OCore_Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "abc",
                table: "OCore_OCore_Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OCore_Recipe",
                schema: "abc");

            migrationBuilder.DropTable(
                name: "OCore_ShellFeature",
                schema: "abc");

            migrationBuilder.DropTable(
                name: "OCore_ShellFeatureState",
                schema: "abc");

            migrationBuilder.DropTable(
                name: "OCore_ShellParameter",
                schema: "abc");

            migrationBuilder.DropTable(
                name: "OCore_SiteSettings",
                schema: "abc");

            migrationBuilder.DropTable(
                name: "OCore_OCore_Articles",
                schema: "abc");

            migrationBuilder.DropTable(
                name: "OCore_OCore_RoleClaims",
                schema: "abc");

            migrationBuilder.DropTable(
                name: "OCore_OCore_TencentVods",
                schema: "abc");

            migrationBuilder.DropTable(
                name: "OCore_OCore_UserClaims",
                schema: "abc");

            migrationBuilder.DropTable(
                name: "OCore_OCore_UserLogins",
                schema: "abc");

            migrationBuilder.DropTable(
                name: "OCore_OCore_UserRoles",
                schema: "abc");

            migrationBuilder.DropTable(
                name: "OCore_OCore_UserTokens",
                schema: "abc");

            migrationBuilder.DropTable(
                name: "OCore_ShellState",
                schema: "abc");

            migrationBuilder.DropTable(
                name: "OCore_ShellDescriptor",
                schema: "abc");

            migrationBuilder.DropTable(
                name: "OCore_OCore_Roles",
                schema: "abc");

            migrationBuilder.DropTable(
                name: "OCore_OCore_Users",
                schema: "abc");
        }
    }
}

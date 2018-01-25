using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace XCore.Migrator.Migrations
{
    public partial class addShell : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "XCore_ShellDescriptor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SerialNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XCore_ShellDescriptor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "XCore_ShellState",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XCore_ShellState", x => x.Id);
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
                        name: "FK_ShellFeature_XCore_ShellDescriptor_ShellDescriptorId",
                        column: x => x.ShellDescriptorId,
                        principalTable: "XCore_ShellDescriptor",
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
                        name: "FK_ShellParameter_XCore_ShellDescriptor_ShellDescriptorId",
                        column: x => x.ShellDescriptorId,
                        principalTable: "XCore_ShellDescriptor",
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
                        name: "FK_ShellFeatureState_XCore_ShellState_ShellStateId",
                        column: x => x.ShellStateId,
                        principalTable: "XCore_ShellState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShellFeature");

            migrationBuilder.DropTable(
                name: "ShellFeatureState");

            migrationBuilder.DropTable(
                name: "ShellParameter");

            migrationBuilder.DropTable(
                name: "XCore_ShellState");

            migrationBuilder.DropTable(
                name: "XCore_ShellDescriptor");
        }
    }
}

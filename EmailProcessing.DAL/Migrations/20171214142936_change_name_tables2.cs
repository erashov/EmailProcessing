using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EmailProcessing.DAL.Migrations
{
    public partial class change_name_tables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParamSettings_PramTypes_PramTypeId",
                table: "ParamSettings");

            migrationBuilder.DropTable(
                name: "PramTypes");

            migrationBuilder.CreateTable(
                name: "ParamTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParamTypes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ParamSettings_ParamTypes_PramTypeId",
                table: "ParamSettings",
                column: "PramTypeId",
                principalTable: "ParamTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParamSettings_ParamTypes_PramTypeId",
                table: "ParamSettings");

            migrationBuilder.DropTable(
                name: "ParamTypes");

            migrationBuilder.CreateTable(
                name: "PramTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PramTypes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ParamSettings_PramTypes_PramTypeId",
                table: "ParamSettings",
                column: "PramTypeId",
                principalTable: "PramTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

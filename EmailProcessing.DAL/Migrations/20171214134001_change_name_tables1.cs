using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EmailProcessing.DAL.Migrations
{
    public partial class change_name_tables1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParamSettings_TypeParams_PramTypeId",
                table: "ParamSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_Serrings_RepeRequests_RequestTypeId",
                table: "Serrings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeParams",
                table: "TypeParams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RepeRequests",
                table: "RepeRequests");

            migrationBuilder.RenameTable(
                name: "TypeParams",
                newName: "PramTypes");

            migrationBuilder.RenameTable(
                name: "RepeRequests",
                newName: "RequestTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PramTypes",
                table: "PramTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestTypes",
                table: "RequestTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParamSettings_PramTypes_PramTypeId",
                table: "ParamSettings",
                column: "PramTypeId",
                principalTable: "PramTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Serrings_RequestTypes_RequestTypeId",
                table: "Serrings",
                column: "RequestTypeId",
                principalTable: "RequestTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParamSettings_PramTypes_PramTypeId",
                table: "ParamSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_Serrings_RequestTypes_RequestTypeId",
                table: "Serrings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestTypes",
                table: "RequestTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PramTypes",
                table: "PramTypes");

            migrationBuilder.RenameTable(
                name: "RequestTypes",
                newName: "RepeRequests");

            migrationBuilder.RenameTable(
                name: "PramTypes",
                newName: "TypeParams");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RepeRequests",
                table: "RepeRequests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeParams",
                table: "TypeParams",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParamSettings_TypeParams_PramTypeId",
                table: "ParamSettings",
                column: "PramTypeId",
                principalTable: "TypeParams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Serrings_RepeRequests_RequestTypeId",
                table: "Serrings",
                column: "RequestTypeId",
                principalTable: "RepeRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

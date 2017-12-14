using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EmailProcessing.DAL.Migrations
{
    public partial class change_name_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParamSettings_TypeParams_TypePramId",
                table: "ParamSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_Serrings_RepeRequests_TypeRequestId",
                table: "Serrings");

            migrationBuilder.RenameColumn(
                name: "TypeRequestId",
                table: "Serrings",
                newName: "RequestTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Serrings_TypeRequestId",
                table: "Serrings",
                newName: "IX_Serrings_RequestTypeId");

            migrationBuilder.RenameColumn(
                name: "TypePramId",
                table: "ParamSettings",
                newName: "PramTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ParamSettings_TypePramId",
                table: "ParamSettings",
                newName: "IX_ParamSettings_PramTypeId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParamSettings_TypeParams_PramTypeId",
                table: "ParamSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_Serrings_RepeRequests_RequestTypeId",
                table: "Serrings");

            migrationBuilder.RenameColumn(
                name: "RequestTypeId",
                table: "Serrings",
                newName: "TypeRequestId");

            migrationBuilder.RenameIndex(
                name: "IX_Serrings_RequestTypeId",
                table: "Serrings",
                newName: "IX_Serrings_TypeRequestId");

            migrationBuilder.RenameColumn(
                name: "PramTypeId",
                table: "ParamSettings",
                newName: "TypePramId");

            migrationBuilder.RenameIndex(
                name: "IX_ParamSettings_PramTypeId",
                table: "ParamSettings",
                newName: "IX_ParamSettings_TypePramId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParamSettings_TypeParams_TypePramId",
                table: "ParamSettings",
                column: "TypePramId",
                principalTable: "TypeParams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Serrings_RepeRequests_TypeRequestId",
                table: "Serrings",
                column: "TypeRequestId",
                principalTable: "RepeRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

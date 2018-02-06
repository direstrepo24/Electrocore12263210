using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Electrocore.Migrations
{
    public partial class FourCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Elemento_Ciudad_Id",
                table: "Elemento",
                column: "Ciudad_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Elemento_Ciudad_Ciudad_Id",
                table: "Elemento",
                column: "Ciudad_Id",
                principalTable: "Ciudad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Elemento_Ciudad_Ciudad_Id",
                table: "Elemento");

            migrationBuilder.DropIndex(
                name: "IX_Elemento_Ciudad_Id",
                table: "Elemento");
        }
    }
}

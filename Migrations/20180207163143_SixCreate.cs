using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Electrocore.Migrations
{
    public partial class SixCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Is_Enabled_Data",
                table: "ElementoCable",
                type: "bool",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Is_Enabled_Data",
                table: "Elemento",
                type: "bool",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Is_Enabled_Data",
                table: "ElementoCable");

            migrationBuilder.DropColumn(
                name: "Is_Enabled_Data",
                table: "Elemento");
        }
    }
}

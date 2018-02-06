using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Electrocore.Migrations
{
    public partial class TreeCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userId",
                table: "Dispositivo");

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha_Sincronizacion",
                table: "Elemento",
                type: "timestamp",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hora_Sincronizacion",
                table: "Elemento",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fecha_Sincronizacion",
                table: "Elemento");

            migrationBuilder.DropColumn(
                name: "Hora_Sincronizacion",
                table: "Elemento");

            migrationBuilder.AddColumn<long>(
                name: "userId",
                table: "Dispositivo",
                nullable: true);
        }
    }
}

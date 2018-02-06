using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Electrocore.Migrations
{
    public partial class SecondCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imei_Device",
                table: "Elemento",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token_Elemento",
                table: "Elemento",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "userId",
                table: "Dispositivo",
                type: "int8",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<string>(
                name: "Android_Id",
                table: "Dispositivo",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Android_Version",
                table: "Dispositivo",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Device_Name",
                table: "Dispositivo",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Direccion_Ip",
                table: "Dispositivo",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Local_Ip_Address",
                table: "Dispositivo",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MacAddr",
                table: "Dispositivo",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone_Type_Device",
                table: "Dispositivo",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Software_Version",
                table: "Dispositivo",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imei_Device",
                table: "Elemento");

            migrationBuilder.DropColumn(
                name: "Token_Elemento",
                table: "Elemento");

            migrationBuilder.DropColumn(
                name: "Android_Id",
                table: "Dispositivo");

            migrationBuilder.DropColumn(
                name: "Android_Version",
                table: "Dispositivo");

            migrationBuilder.DropColumn(
                name: "Device_Name",
                table: "Dispositivo");

            migrationBuilder.DropColumn(
                name: "Direccion_Ip",
                table: "Dispositivo");

            migrationBuilder.DropColumn(
                name: "Local_Ip_Address",
                table: "Dispositivo");

            migrationBuilder.DropColumn(
                name: "MacAddr",
                table: "Dispositivo");

            migrationBuilder.DropColumn(
                name: "Phone_Type_Device",
                table: "Dispositivo");

            migrationBuilder.DropColumn(
                name: "Software_Version",
                table: "Dispositivo");

            migrationBuilder.AlterColumn<long>(
                name: "userId",
                table: "Dispositivo",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "int8",
                oldNullable: true);
        }
    }
}

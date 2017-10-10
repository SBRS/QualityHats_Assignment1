using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QualityHats.Migrations
{
    public partial class ModelsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SupplierName",
                table: "Supplier",
                maxLength: 100,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Supplier",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                table: "Supplier",
                maxLength: 20,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "HomeNumber",
                table: "Supplier",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Supplier",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "HatName",
                table: "Hat",
                maxLength: 50,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Hat",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Category",
                maxLength: 50,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "WorkNumber",
                table: "AspNetUsers",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                table: "AspNetUsers",
                maxLength: 20,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "HomeNumber",
                table: "AspNetUsers",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SupplierName",
                table: "Supplier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Supplier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                table: "Supplier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HomeNumber",
                table: "Supplier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Supplier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HatName",
                table: "Hat",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Hat",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Category",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WorkNumber",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HomeNumber",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}

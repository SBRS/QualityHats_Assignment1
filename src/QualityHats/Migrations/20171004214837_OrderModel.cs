using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QualityHats.Migrations
{
    public partial class OrderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Subtotal",
                table: "Order",
                nullable: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "GrandTotal",
                table: "Order",
                nullable: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "GST",
                table: "Order",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Subtotal",
                table: "Order",
                type: "money",
                nullable: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "GrandTotal",
                table: "Order",
                type: "money",
                nullable: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "GST",
                table: "Order",
                type: "money",
                nullable: false);
        }
    }
}

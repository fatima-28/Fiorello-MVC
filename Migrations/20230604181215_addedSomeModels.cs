﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class addedSomeModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Slides",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "ProductImages",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Slides");

            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "ProductImages");
        }
    }
}

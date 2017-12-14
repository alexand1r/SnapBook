using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Snapbook.Data.Migrations
{
    public partial class NotificationUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Notifications",
                newName: "SenderUrl");

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Notifications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sender",
                table: "Notifications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Sender",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "SenderUrl",
                table: "Notifications",
                newName: "SenderId");
        }
    }
}

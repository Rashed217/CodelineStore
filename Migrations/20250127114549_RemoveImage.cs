﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodelineStore.Migrations
{
    /// <inheritdoc />
    public partial class RemoveImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

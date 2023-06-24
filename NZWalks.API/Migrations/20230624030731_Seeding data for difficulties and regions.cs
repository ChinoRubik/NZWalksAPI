using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class Seedingdatafordifficultiesandregions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("67a04df2-e37a-4e80-8f3b-d52ba62f65e8"), "GDL", "Guadalajara", "https://th.bing.com/th/id/R.3ee144293cd7e27e7d24d62d6ac6da95?rik=ojfVhRArgDl8PA&pid=ImgRaw&r=0" },
                    { new Guid("8929b4bf-5be3-4002-8ad6-b9f46f782f16"), "CDMX", "Ciudad de México", "https://th.bing.com/th/id/OIP.YlInlV-4E257n6usFJv1GgHaFJ?pid=ImgDet&rs=1" }
                });

            migrationBuilder.InsertData(
                table: "difficulties",
                columns: new[] { "Id", "name" },
                values: new object[,]
                {
                    { new Guid("3897b275-7a3f-4a84-a620-105b9b0eb89a"), "Easy" },
                    { new Guid("de63304d-8500-4570-8333-abb077e5a23f"), "Hard" },
                    { new Guid("e4567686-1b4d-483d-a374-9e99306c8e7b"), "Medium" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("67a04df2-e37a-4e80-8f3b-d52ba62f65e8"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("8929b4bf-5be3-4002-8ad6-b9f46f782f16"));

            migrationBuilder.DeleteData(
                table: "difficulties",
                keyColumn: "Id",
                keyValue: new Guid("3897b275-7a3f-4a84-a620-105b9b0eb89a"));

            migrationBuilder.DeleteData(
                table: "difficulties",
                keyColumn: "Id",
                keyValue: new Guid("de63304d-8500-4570-8333-abb077e5a23f"));

            migrationBuilder.DeleteData(
                table: "difficulties",
                keyColumn: "Id",
                keyValue: new Guid("e4567686-1b4d-483d-a374-9e99306c8e7b"));
        }
    }
}

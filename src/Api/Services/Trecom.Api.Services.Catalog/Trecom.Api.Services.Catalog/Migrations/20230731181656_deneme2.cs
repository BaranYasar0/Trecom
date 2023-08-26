using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trecom.Api.Services.Catalog.Migrations
{
    public partial class deneme2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BaseCategories",
                keyColumn: "Id",
                keyValue: new Guid("e4d275b7-3ef1-41b3-8074-7e21df92dc30"));

            migrationBuilder.InsertData(
                table: "BaseCategories",
                columns: new[] { "Id", "CreateDate", "Description", "IsActive", "Name", "PictureUrl" },
                values: new object[] { new Guid("3c5983c0-2959-400b-9d37-087e1ecdde5f"), new DateTime(2023, 7, 31, 21, 16, 56, 625, DateTimeKind.Local).AddTicks(827), null, true, "Base Deneme", null });

            migrationBuilder.InsertData(
                table: "TypeCategories",
                columns: new[] { "Id", "BaseCategoryId", "CreateDate", "Description", "IsActive", "Name", "PictureUrl" },
                values: new object[] { new Guid("3f0af26e-1828-4f89-b800-18ae76d393f9"), new Guid("3c5983c0-2959-400b-9d37-087e1ecdde5f"), new DateTime(2023, 7, 31, 21, 16, 56, 625, DateTimeKind.Local).AddTicks(838), null, true, "Type Deneme", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TypeCategories",
                keyColumn: "Id",
                keyValue: new Guid("3f0af26e-1828-4f89-b800-18ae76d393f9"));

            migrationBuilder.DeleteData(
                table: "BaseCategories",
                keyColumn: "Id",
                keyValue: new Guid("3c5983c0-2959-400b-9d37-087e1ecdde5f"));

            migrationBuilder.InsertData(
                table: "BaseCategories",
                columns: new[] { "Id", "CreateDate", "Description", "IsActive", "Name", "PictureUrl" },
                values: new object[] { new Guid("e4d275b7-3ef1-41b3-8074-7e21df92dc30"), new DateTime(2023, 7, 31, 21, 11, 10, 653, DateTimeKind.Local).AddTicks(2128), null, true, "Base Deneme", null });
        }
    }
}

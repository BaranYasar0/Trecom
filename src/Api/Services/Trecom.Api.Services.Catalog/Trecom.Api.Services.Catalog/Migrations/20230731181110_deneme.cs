using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trecom.Api.Services.Catalog.Migrations
{
    public partial class deneme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BaseCategories",
                keyColumn: "Id",
                keyValue: new Guid("095db9b1-4e8d-4c88-9c7e-492213fb7e67"));

            migrationBuilder.DeleteData(
                table: "BaseCategories",
                keyColumn: "Id",
                keyValue: new Guid("1bf94232-9dd6-4fe1-9c7d-7b99c4e35e81"));

            migrationBuilder.DeleteData(
                table: "BaseCategories",
                keyColumn: "Id",
                keyValue: new Guid("27d3359d-39cc-48ab-a131-fd53009dc91d"));

            migrationBuilder.DeleteData(
                table: "BaseCategories",
                keyColumn: "Id",
                keyValue: new Guid("437681c8-3f9d-4825-af8a-82dfa337883e"));

            migrationBuilder.DeleteData(
                table: "BaseCategories",
                keyColumn: "Id",
                keyValue: new Guid("4afe2c20-f9e0-4f77-9869-4f8cbbe7c250"));

            migrationBuilder.DeleteData(
                table: "BaseCategories",
                keyColumn: "Id",
                keyValue: new Guid("52698c6c-c5cc-4a39-9000-28ce5fa267ef"));

            migrationBuilder.DeleteData(
                table: "BaseCategories",
                keyColumn: "Id",
                keyValue: new Guid("57c65f10-53b5-45f1-90e7-aa91079aed4b"));

            migrationBuilder.DeleteData(
                table: "BaseCategories",
                keyColumn: "Id",
                keyValue: new Guid("68dddf99-f84c-4b89-9304-c9e8f35d4ac6"));

            migrationBuilder.DeleteData(
                table: "BaseCategories",
                keyColumn: "Id",
                keyValue: new Guid("7c6e3ac7-6e4d-498e-b2d9-5cdfdf6e90b5"));

            migrationBuilder.DeleteData(
                table: "BaseCategories",
                keyColumn: "Id",
                keyValue: new Guid("8a2599dc-7c66-4302-a612-18b2c825ccee"));

            migrationBuilder.DeleteData(
                table: "BaseCategories",
                keyColumn: "Id",
                keyValue: new Guid("914ee5fb-2807-424a-ba70-5848b72d95fb"));

            migrationBuilder.DeleteData(
                table: "BaseCategories",
                keyColumn: "Id",
                keyValue: new Guid("cbde1b95-c5b1-4891-83cb-754dfc4d2809"));

            migrationBuilder.DeleteData(
                table: "BaseCategories",
                keyColumn: "Id",
                keyValue: new Guid("cc9e5a92-233e-432d-8e86-dfbbf505e85e"));

            migrationBuilder.DeleteData(
                table: "BaseCategories",
                keyColumn: "Id",
                keyValue: new Guid("cdc8a989-c128-4a00-8f4d-c47fa5d2ed5d"));

            migrationBuilder.DeleteData(
                table: "BaseCategories",
                keyColumn: "Id",
                keyValue: new Guid("d71545e7-d41f-48dc-8bbb-c6ab9c7126e5"));

            migrationBuilder.DeleteData(
                table: "BaseCategories",
                keyColumn: "Id",
                keyValue: new Guid("dd60d192-2712-4e9e-9ac8-3a75362b6aff"));

            migrationBuilder.DeleteData(
                table: "BaseCategories",
                keyColumn: "Id",
                keyValue: new Guid("ef124dcd-8ced-4ad7-8a4c-18c5753e57ca"));

            migrationBuilder.DeleteData(
                table: "BaseCategories",
                keyColumn: "Id",
                keyValue: new Guid("f8188859-604b-47e0-93df-93106532cbf9"));

            migrationBuilder.InsertData(
                table: "BaseCategories",
                columns: new[] { "Id", "CreateDate", "Description", "IsActive", "Name", "PictureUrl" },
                values: new object[] { new Guid("e4d275b7-3ef1-41b3-8074-7e21df92dc30"), new DateTime(2023, 7, 31, 21, 11, 10, 653, DateTimeKind.Local).AddTicks(2128), null, true, "Base Deneme", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BaseCategories",
                keyColumn: "Id",
                keyValue: new Guid("e4d275b7-3ef1-41b3-8074-7e21df92dc30"));

            migrationBuilder.InsertData(
                table: "BaseCategories",
                columns: new[] { "Id", "CreateDate", "Description", "IsActive", "Name", "PictureUrl" },
                values: new object[,]
                {
                    { new Guid("095db9b1-4e8d-4c88-9c7e-492213fb7e67"), new DateTime(2023, 7, 31, 18, 1, 44, 632, DateTimeKind.Local).AddTicks(4690), null, true, "Electronics", null },
                    { new Guid("1bf94232-9dd6-4fe1-9c7d-7b99c4e35e81"), new DateTime(2023, 7, 31, 18, 1, 44, 632, DateTimeKind.Local).AddTicks(4693), null, true, "Hardware", null },
                    { new Guid("27d3359d-39cc-48ab-a131-fd53009dc91d"), new DateTime(2023, 7, 31, 18, 1, 44, 632, DateTimeKind.Local).AddTicks(4691), null, true, "Food, Beverages & Tobacco", null },
                    { new Guid("437681c8-3f9d-4825-af8a-82dfa337883e"), new DateTime(2023, 7, 31, 18, 1, 44, 632, DateTimeKind.Local).AddTicks(4679), null, true, "Cameras & Optics", null },
                    { new Guid("4afe2c20-f9e0-4f77-9869-4f8cbbe7c250"), new DateTime(2023, 7, 31, 18, 1, 44, 632, DateTimeKind.Local).AddTicks(4703), null, true, "Vehicles & Parts", null },
                    { new Guid("52698c6c-c5cc-4a39-9000-28ce5fa267ef"), new DateTime(2023, 7, 31, 18, 1, 44, 632, DateTimeKind.Local).AddTicks(4700), null, true, "Software", null },
                    { new Guid("57c65f10-53b5-45f1-90e7-aa91079aed4b"), new DateTime(2023, 7, 31, 18, 1, 44, 632, DateTimeKind.Local).AddTicks(4696), null, true, "Mature", null },
                    { new Guid("68dddf99-f84c-4b89-9304-c9e8f35d4ac6"), new DateTime(2023, 7, 31, 18, 1, 44, 632, DateTimeKind.Local).AddTicks(4663), null, true, "Animals & Pet Supplies", null },
                    { new Guid("7c6e3ac7-6e4d-498e-b2d9-5cdfdf6e90b5"), new DateTime(2023, 7, 31, 18, 1, 44, 632, DateTimeKind.Local).AddTicks(4676), null, true, "Arts & Entertainment", null },
                    { new Guid("8a2599dc-7c66-4302-a612-18b2c825ccee"), new DateTime(2023, 7, 31, 18, 1, 44, 632, DateTimeKind.Local).AddTicks(4692), null, true, "Furniture", null },
                    { new Guid("914ee5fb-2807-424a-ba70-5848b72d95fb"), new DateTime(2023, 7, 31, 18, 1, 44, 632, DateTimeKind.Local).AddTicks(4702), null, true, "Toys & Games", null },
                    { new Guid("cbde1b95-c5b1-4891-83cb-754dfc4d2809"), new DateTime(2023, 7, 31, 18, 1, 44, 632, DateTimeKind.Local).AddTicks(4675), null, true, "Apparel & Accessories", null },
                    { new Guid("cc9e5a92-233e-432d-8e86-dfbbf505e85e"), new DateTime(2023, 7, 31, 18, 1, 44, 632, DateTimeKind.Local).AddTicks(4677), null, true, "Baby & Toddler", null },
                    { new Guid("cdc8a989-c128-4a00-8f4d-c47fa5d2ed5d"), new DateTime(2023, 7, 31, 18, 1, 44, 632, DateTimeKind.Local).AddTicks(4695), null, true, "Home & Garden", null },
                    { new Guid("d71545e7-d41f-48dc-8bbb-c6ab9c7126e5"), new DateTime(2023, 7, 31, 18, 1, 44, 632, DateTimeKind.Local).AddTicks(4701), null, true, "Sporting Goods", null },
                    { new Guid("dd60d192-2712-4e9e-9ac8-3a75362b6aff"), new DateTime(2023, 7, 31, 18, 1, 44, 632, DateTimeKind.Local).AddTicks(4697), null, true, "Office Supplies", null },
                    { new Guid("ef124dcd-8ced-4ad7-8a4c-18c5753e57ca"), new DateTime(2023, 7, 31, 18, 1, 44, 632, DateTimeKind.Local).AddTicks(4678), null, true, "Business & Industrial", null },
                    { new Guid("f8188859-604b-47e0-93df-93106532cbf9"), new DateTime(2023, 7, 31, 18, 1, 44, 632, DateTimeKind.Local).AddTicks(4694), null, true, "Health & Beauty", null }
                });
        }
    }
}

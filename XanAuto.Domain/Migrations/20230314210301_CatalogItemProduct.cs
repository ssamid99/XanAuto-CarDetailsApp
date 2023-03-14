using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XanAuto.Domain.Migrations
{
    public partial class CatalogItemProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductCatalogItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    MeasureId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCatalogItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCatalogItem_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductCatalogItem_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductCatalogItem_Measures_MeasureId",
                        column: x => x.MeasureId,
                        principalTable: "Measures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductCatalogItem_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductCatalogItem_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductCatalogItem_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductCatalogItem_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCatalogItem_CreatedByUserId",
                table: "ProductCatalogItem",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCatalogItem_CurrencyId",
                table: "ProductCatalogItem",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCatalogItem_GroupId",
                table: "ProductCatalogItem",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCatalogItem_MeasureId",
                table: "ProductCatalogItem",
                column: "MeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCatalogItem_ModelId",
                table: "ProductCatalogItem",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCatalogItem_ProductId",
                table: "ProductCatalogItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCatalogItem_SupplierId",
                table: "ProductCatalogItem",
                column: "SupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCatalogItem");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace restaurant.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "foods",
                columns: table => new
                {
                    food_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    food_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    food_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    img = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_foods", x => x.food_ID);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    username_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.username_ID);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    order_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customar_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<int>(type: "int", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    messege = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    payment = table.Column<bool>(type: "bit", nullable: true),
                    username_ID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.order_ID);
                    table.ForeignKey(
                        name: "FK_orders_users_username_ID",
                        column: x => x.username_ID,
                        principalTable: "users",
                        principalColumn: "username_ID");
                });

            migrationBuilder.CreateTable(
                name: "payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Master_card = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    csv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    username_ID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_payment_users_username_ID",
                        column: x => x.username_ID,
                        principalTable: "users",
                        principalColumn: "username_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tabel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KindOfFood = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    username_ID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tabel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tabel_users_username_ID",
                        column: x => x.username_ID,
                        principalTable: "users",
                        principalColumn: "username_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_Foods",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_ID = table.Column<int>(type: "int", nullable: false),
                    food_ID = table.Column<int>(type: "int", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_Foods", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_Foods_foods_food_ID",
                        column: x => x.food_ID,
                        principalTable: "foods",
                        principalColumn: "food_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_Foods_orders_order_ID",
                        column: x => x.order_ID,
                        principalTable: "orders",
                        principalColumn: "order_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_Foods_food_ID",
                table: "order_Foods",
                column: "food_ID");

            migrationBuilder.CreateIndex(
                name: "IX_order_Foods_order_ID",
                table: "order_Foods",
                column: "order_ID");

            migrationBuilder.CreateIndex(
                name: "IX_orders_username_ID",
                table: "orders",
                column: "username_ID");

            migrationBuilder.CreateIndex(
                name: "IX_payment_username_ID",
                table: "payment",
                column: "username_ID");

            migrationBuilder.CreateIndex(
                name: "IX_tabel_username_ID",
                table: "tabel",
                column: "username_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_Foods");

            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropTable(
                name: "tabel");

            migrationBuilder.DropTable(
                name: "foods");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}

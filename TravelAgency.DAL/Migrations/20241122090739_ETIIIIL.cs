using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelAgency.DAL.Migrations
{
    public partial class ETIIIIL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "picture_services",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_service = table.Column<Guid>(type: "uuid", nullable: false),
                    path_img = table.Column<string>(type: "text", nullable: true),
                    Picture_servicesDbId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_picture_services", x => x.id);
                    table.ForeignKey(
                        name: "FK_picture_services_picture_services_Picture_servicesDbId",
                        column: x => x.Picture_servicesDbId,
                        principalTable: "picture_services",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    login = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    role = table.Column<int>(type: "integer", nullable: false),
                    path_img = table.Column<string>(type: "text", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_country = table.Column<Guid>(type: "uuid", nullable: false),
                    city = table.Column<string>(type: "text", nullable: true),
                    name_service = table.Column<string>(type: "text", nullable: true),
                    cleaning_apartment_price = table.Column<decimal>(type: "numeric", nullable: false),
                    cleaning_home_price = table.Column<decimal>(type: "numeric", nullable: false),
                    cleaning_area_price = table.Column<decimal>(type: "numeric", nullable: false),
                    cleaning_office_price = table.Column<decimal>(type: "numeric", nullable: false),
                    cleaning_construction_price = table.Column<decimal>(type: "numeric", nullable: false),
                    cleaning_garden_price = table.Column<decimal>(type: "numeric", nullable: false),
                    path_img = table.Column<string>(type: "text", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    pictureServicesId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_services", x => x.id);
                    table.ForeignKey(
                        name: "FK_services_picture_services_pictureServicesId",
                        column: x => x.pictureServicesId,
                        principalTable: "picture_services",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "requests",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_user = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    path_img = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UsersDbId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requests", x => x.id);
                    table.ForeignKey(
                        name: "FK_requests_users_UsersDbId",
                        column: x => x.UsersDbId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    path_img = table.Column<string>(type: "text", nullable: true),
                    count_services = table.Column<int>(type: "integer", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    ServicesId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.id);
                    table.ForeignKey(
                        name: "FK_countries_services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "services",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_user = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    id_service = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersDbId = table.Column<Guid>(type: "uuid", nullable: true),
                    ordersDbsId = table.Column<Guid>(type: "uuid", nullable: true),
                    ServicesDbId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_orders_orders_ordersDbsId",
                        column: x => x.ordersDbsId,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_orders_services_ServicesDbId",
                        column: x => x.ServicesDbId,
                        principalTable: "services",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_orders_users_UsersDbId",
                        column: x => x.UsersDbId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_countries_ServicesId",
                table: "countries",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_ordersDbsId",
                table: "orders",
                column: "ordersDbsId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_ServicesDbId",
                table: "orders",
                column: "ServicesDbId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_UsersDbId",
                table: "orders",
                column: "UsersDbId");

            migrationBuilder.CreateIndex(
                name: "IX_picture_services_Picture_servicesDbId",
                table: "picture_services",
                column: "Picture_servicesDbId");

            migrationBuilder.CreateIndex(
                name: "IX_requests_UsersDbId",
                table: "requests",
                column: "UsersDbId");

            migrationBuilder.CreateIndex(
                name: "IX_services_pictureServicesId",
                table: "services",
                column: "pictureServicesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "countries");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "requests");

            migrationBuilder.DropTable(
                name: "services");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "picture_services");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuariosApi.Migrations
{
    public partial class AdicionandoCustomIdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997,
                column: "ConcurrencyStamp",
                value: "94f0fa3a-f356-49e4-952b-8a40de2bac5b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "31ca5398-99e4-4b59-95ce-981b49c80a88");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 199999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "59fe5f8b-105d-454d-85a7-3554da969994", "AQAAAAEAACcQAAAAECTw+nrIWaLQZa+NeQcVIGcT7hH6cKnfV+SgHX345XajqX20VkeI1rhYEzD8Zn1yfw==", "c96b1449-a0ce-487e-885c-f43bb4e27126" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997,
                column: "ConcurrencyStamp",
                value: "b3d707f7-752d-46ee-8986-f2b4577da167");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "a9d9fe4a-12bd-41b3-aeac-de1869cfc502");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 199999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "010adc30-d23c-4465-b232-4cbde0dd83bb", "AQAAAAEAACcQAAAAEJVt9l4wIxbtqZdzjie2l/vWYP3Ld5HPcT6CUKjF3lF9LAp3xa99My21JbqGUKlTQg==", "01aecaa9-deea-4566-8d86-f2c989f6c523" });
        }
    }
}

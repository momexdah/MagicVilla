using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "id", "Tarifa", "amenidad", "detalle", "fecha_actualizacion", "fecha_creacion", "imagen_url", "metros_cuadrados", "nombre", "ocupantes" },
                values: new object[,]
                {
                    { 1, 200.0, "", "Detalle...", new DateTime(2023, 9, 18, 23, 2, 14, 822, DateTimeKind.Local).AddTicks(4253), new DateTime(2023, 9, 18, 23, 2, 14, 822, DateTimeKind.Local).AddTicks(4243), "", 50, "Villa Real", 4 },
                    { 2, 150.0, "", "Detalle...", new DateTime(2023, 9, 18, 23, 2, 14, 822, DateTimeKind.Local).AddTicks(4255), new DateTime(2023, 9, 18, 23, 2, 14, 822, DateTimeKind.Local).AddTicks(4255), "", 40, "Villa Premium", 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "id",
                keyValue: 2);
        }
    }
}

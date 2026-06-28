using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SemanticSentry.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Method = table.Column<string>(type: "TEXT", nullable: false),
                    TargetPath = table.Column<string>(type: "TEXT", nullable: false),
                    ClientIp = table.Column<string>(type: "TEXT", nullable: false),
                    IdentityRisk = table.Column<double>(type: "REAL", nullable: false),
                    AnomalyRisk = table.Column<double>(type: "REAL", nullable: false),
                    ContentRisk = table.Column<double>(type: "REAL", nullable: false),
                    FinalSuspicionScore = table.Column<double>(type: "REAL", nullable: false),
                    IsBlocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    MitigationReason = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestLogs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestLogs");
        }
    }
}

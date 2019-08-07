using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiSite.Migrations {
    public partial class V2 : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                "LogonHistory",
                table => new {
                    Id = table.Column<int>()
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Token = table.Column<string>(),
                    StaffId = table.Column<int>(),
                    StaffName = table.Column<string>(),
                    IpAddr = table.Column<string>(),
                    Date = table.Column<DateTime>(),
                    Guid = table.Column<string>(),
                    State = table.Column<string>()
                },
                constraints: table => { table.PrimaryKey("PK_LogonHistory", x => x.Id); });

            migrationBuilder.CreateTable(
                "ProjectInfo",
                table => new {
                    Id = table.Column<int>()
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Department = table.Column<string>(),
                    Description = table.Column<string>(),
                    Handler = table.Column<string>(),
                    Name = table.Column<string>(),
                    OperateDateTime = table.Column<DateTime>(),
                    Operator = table.Column<string>(),
                    State = table.Column<string>()
                },
                constraints: table => { table.PrimaryKey("PK_ProjectInfo", x => x.Id); });

            migrationBuilder.CreateTable(
                "ProjectAttachment",
                table => new {
                    Id = table.Column<int>()
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(),
                    Url = table.Column<string>(),
                    ProjectInfoId = table.Column<int>(nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_ProjectAttachment", x => x.Id);
                    table.ForeignKey(
                        "FK_ProjectAttachment_ProjectInfo_ProjectInfoId",
                        x => x.ProjectInfoId,
                        "ProjectInfo",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_ProjectAttachment_ProjectInfoId",
                "ProjectAttachment",
                "ProjectInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                "LogonHistory");

            migrationBuilder.DropTable(
                "ProjectAttachment");

            migrationBuilder.DropTable(
                "ProjectInfo");
        }
    }
}
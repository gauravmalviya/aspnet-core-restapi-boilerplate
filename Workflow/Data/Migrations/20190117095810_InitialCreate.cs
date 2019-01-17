using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CWG.API.Workflow.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.EnsureSchema(
                name: "cl");

            migrationBuilder.CreateTable(
                name: "AuthRole",
                schema: "auth",
                columns: table => new
                {
                    IsActive = table.Column<bool>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthRole", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "AuthUser",
                schema: "auth",
                columns: table => new
                {
                    IsActive = table.Column<bool>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<byte[]>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordResetCode = table.Column<string>(nullable: true),
                    IsFirstTimeLogin = table.Column<bool>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    PhoneConfirmed = table.Column<bool>(nullable: true),
                    PhoneConfirmedCode = table.Column<string>(nullable: true),
                    IsTwoFactoredEnabled = table.Column<bool>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    EmailConfirmedCode = table.Column<string>(nullable: true),
                    PictureUrl = table.Column<string>(nullable: true),
                    LastLoginTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthUser", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AuthUserRole",
                schema: "auth",
                columns: table => new
                {
                    IsActive = table.Column<bool>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    UserRoleId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthUserRole", x => x.UserRoleId);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                schema: "cl",
                columns: table => new
                {
                    IsActive = table.Column<bool>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<int>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    ClientId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    StateName = table.Column<string>(maxLength: 100, nullable: true),
                    Country = table.Column<string>(maxLength: 100, nullable: true),
                    Pincode = table.Column<string>(maxLength: 10, nullable: true),
                    OfficeNo = table.Column<string>(maxLength: 15, nullable: true),
                    Mobile = table.Column<string>(maxLength: 15, nullable: false),
                    LogoUrl = table.Column<string>(maxLength: 500, nullable: true),
                    Effective_Date = table.Column<DateTime>(nullable: true),
                    Termination_Date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ClientId);
                });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "AuthRole",
                columns: new[] { "RoleId", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "IsActive", "IsDeleted", "ModifiedBy", "ModifiedOn", "Name" },
                values: new object[] { 1, null, null, null, null, null, null, null, null, "Super Admin" });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "AuthUser",
                columns: new[] { "UserId", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Email", "EmailConfirmed", "EmailConfirmedCode", "FirstName", "IsActive", "IsDeleted", "IsFirstTimeLogin", "IsTwoFactoredEnabled", "LastLoginTime", "LastName", "ModifiedBy", "ModifiedOn", "PasswordHash", "PasswordResetCode", "Phone", "PhoneConfirmed", "PhoneConfirmedCode", "PictureUrl", "SecurityStamp", "UserName" },
                values: new object[] { 1, null, new DateTime(2019, 1, 17, 9, 58, 10, 16, DateTimeKind.Utc), null, null, "info@codewithgaurav.com", true, null, "System", true, false, true, false, null, "Admin", null, null, new byte[] { 140, 219, 82, 99, 102, 69, 70, 123, 97, 59, 47, 94, 7, 71, 236, 153, 109, 94, 224, 104, 181, 141, 20, 140, 163, 182, 149, 29, 184, 123, 136, 238, 7, 183, 230, 45, 55, 175, 165, 76, 68, 233, 194, 107, 18, 66, 71, 69, 152, 232, 176, 16, 119, 233, 30, 82, 25, 72, 191, 178, 159, 132, 98, 154 }, null, "999999999", true, null, null, new byte[] { 2, 171, 143, 104, 93, 31, 55, 21, 183, 218, 132, 88, 214, 90, 89, 255, 31, 132, 143, 99, 34, 97, 48, 177, 226, 3, 69, 139, 196, 166, 147, 71, 167, 69, 186, 100, 195, 34, 228, 104, 105, 29, 213, 40, 152, 90, 173, 224, 222, 213, 235, 187, 240, 1, 201, 37, 46, 249, 16, 201, 27, 154, 185, 255, 248, 33, 46, 42, 171, 238, 41, 227, 200, 163, 129, 5, 124, 249, 18, 1, 173, 190, 191, 211, 60, 97, 67, 29, 197, 30, 19, 87, 213, 209, 156, 72, 90, 149, 30, 118, 156, 231, 159, 232, 16, 88, 41, 189, 120, 207, 96, 205, 239, 249, 176, 238, 31, 247, 185, 247, 226, 234, 249, 180, 107, 74, 185, 133 }, "sysadmin" });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "AuthUserRole",
                columns: new[] { "UserRoleId", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "IsActive", "IsDeleted", "ModifiedBy", "ModifiedOn", "RoleId", "UserId" },
                values: new object[] { 1, null, null, null, null, true, false, null, null, 1, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthRole",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "AuthUser",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "AuthUserRole",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "Client",
                schema: "cl");
        }
    }
}

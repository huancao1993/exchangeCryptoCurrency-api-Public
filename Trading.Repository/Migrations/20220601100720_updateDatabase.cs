using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Trading.Authen.Repository.Migrations
{
    public partial class updateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleGroups",
                columns: table => new
                {
                    IdRoleGroup = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 100, nullable: true),
                    CreateAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Status = table.Column<byte>(nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleGroups", x => x.IdRoleGroup);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRole = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    RoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRole);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 100, nullable: true),
                    CreateAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Code = table.Column<Guid>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    Status = table.Column<byte>(maxLength: 200, nullable: false),
                    IdAvatar = table.Column<Guid>(nullable: true),
                    Phone = table.Column<string>(maxLength: 100, nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleActions",
                columns: table => new
                {
                    IdRoleAction = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleActionName = table.Column<string>(maxLength: 100, nullable: true),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    IdRole = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleActions", x => x.IdRoleAction);
                    table.ForeignKey(
                        name: "FK_RoleActions_Roles_IdRole",
                        column: x => x.IdRole,
                        principalTable: "Roles",
                        principalColumn: "IdRole",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Screens",
                columns: table => new
                {
                    IdScree = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    IdRole = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screens", x => x.IdScree);
                    table.ForeignKey(
                        name: "FK_Screens_Roles_IdRole",
                        column: x => x.IdRole,
                        principalTable: "Roles",
                        principalColumn: "IdRole",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Screens_Screens_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Screens",
                        principalColumn: "IdScree",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserHasRoleGroups",
                columns: table => new
                {
                    IdUserHasRoleGroup = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 100, nullable: true),
                    CreateAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IdUser = table.Column<int>(nullable: false),
                    IdRoleGroup = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserHasRoleGroups", x => x.IdUserHasRoleGroup);
                    table.ForeignKey(
                        name: "FK_UserHasRoleGroups_RoleGroups_IdRoleGroup",
                        column: x => x.IdRoleGroup,
                        principalTable: "RoleGroups",
                        principalColumn: "IdRoleGroup",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserHasRoleGroups_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    IdPermission = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 100, nullable: true),
                    CreateAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IdUser = table.Column<int>(nullable: false),
                    IdRoleAction = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.IdPermission);
                    table.ForeignKey(
                        name: "FK_Permissions_RoleActions_IdRoleAction",
                        column: x => x.IdRoleAction,
                        principalTable: "RoleActions",
                        principalColumn: "IdRoleAction",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permissions_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleGroupActions",
                columns: table => new
                {
                    IdRoleGroupAction = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    UpdateBy = table.Column<string>(maxLength: 100, nullable: true),
                    CreateAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IdRoleGroup = table.Column<int>(nullable: false),
                    IdRoleAction = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleGroupActions", x => x.IdRoleGroupAction);
                    table.ForeignKey(
                        name: "FK_RoleGroupActions_RoleActions_IdRoleAction",
                        column: x => x.IdRoleAction,
                        principalTable: "RoleActions",
                        principalColumn: "IdRoleAction",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoleGroupActions_RoleGroups_IdRoleGroup",
                        column: x => x.IdRoleGroup,
                        principalTable: "RoleGroups",
                        principalColumn: "IdRoleGroup",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_IdRoleAction",
                table: "Permissions",
                column: "IdRoleAction");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_IdUser",
                table: "Permissions",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_RoleActions_IdRole",
                table: "RoleActions",
                column: "IdRole");

            migrationBuilder.CreateIndex(
                name: "IX_RoleGroupActions_IdRoleAction",
                table: "RoleGroupActions",
                column: "IdRoleAction");

            migrationBuilder.CreateIndex(
                name: "IX_RoleGroupActions_IdRoleGroup",
                table: "RoleGroupActions",
                column: "IdRoleGroup");

            migrationBuilder.CreateIndex(
                name: "IX_Screens_IdRole",
                table: "Screens",
                column: "IdRole");

            migrationBuilder.CreateIndex(
                name: "IX_Screens_ParentId",
                table: "Screens",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHasRoleGroups_IdRoleGroup",
                table: "UserHasRoleGroups",
                column: "IdRoleGroup");

            migrationBuilder.CreateIndex(
                name: "IX_UserHasRoleGroups_IdUser",
                table: "UserHasRoleGroups",
                column: "IdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "RoleGroupActions");

            migrationBuilder.DropTable(
                name: "Screens");

            migrationBuilder.DropTable(
                name: "UserHasRoleGroups");

            migrationBuilder.DropTable(
                name: "RoleActions");

            migrationBuilder.DropTable(
                name: "RoleGroups");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

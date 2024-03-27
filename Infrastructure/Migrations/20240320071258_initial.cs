using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StatusDepartment = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Problem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsDisplay = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Problem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ColorCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleTypeName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RoomStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleTypeId = table.Column<int>(type: "int", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_RoleTypes_RoleTypeId",
                        column: x => x.RoleTypeId,
                        principalTable: "RoleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarPhoto = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Birthday = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VerifyCode = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    VerifyRefreshExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RefreshTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Enable = table.Column<bool>(type: "bit", nullable: false),
                    IsBanned = table.Column<bool>(type: "bit", nullable: false),
                    StatusAccount = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounts_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestStatusId = table.Column<int>(type: "int", nullable: false),
                    ProblemId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    SeveralLevel = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Enable = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_Problem_ProblemId",
                        column: x => x.ProblemId,
                        principalTable: "Problem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Requests_RequestStatus_RequestStatusId",
                        column: x => x.RequestStatusId,
                        principalTable: "RequestStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Requests_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NotificationHandleRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 100, nullable: false),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationHandleRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationHandleRequest_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId");
                    table.ForeignKey(
                        name: "FK_NotificationHandleRequest_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationQueue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotificationTypeId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountSenderId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotificationTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsViewed = table.Column<bool>(type: "bit", nullable: false),
                    NotificationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ViewedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationQueue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationQueue_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationQueue_Accounts_AccountSenderId",
                        column: x => x.AccountSenderId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId");
                    table.ForeignKey(
                        name: "FK_NotificationQueue_NotificationType_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalTable: "NotificationType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NotificationQueue_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NotificationRemark",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false),
                    Unwatchs = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationRemark", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationRemark_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId");
                    table.ForeignKey(
                        name: "FK_NotificationRemark_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessByAssignees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessByAssignees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessByAssignees_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId");
                    table.ForeignKey(
                        name: "FK_ProcessByAssignees_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Remarks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Enable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Remarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Remarks_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId");
                    table.ForeignKey(
                        name: "FK_Remarks_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "NotificationType",
                columns: new[] { "Id", "TypeName" },
                values: new object[,]
                {
                    { 1, "Create request" },
                    { 2, "Assigned request" },
                    { 3, "Update request" },
                    { 4, "Chat" }
                });

            migrationBuilder.InsertData(
                table: "Problem",
                columns: new[] { "Id", "IsDisplay", "Title" },
                values: new object[,]
                {
                    { 1, true, "Fire and Safety Hazards" },
                    { 2, true, "Poor Ventilation" },
                    { 3, true, "Bullying and Harassment" },
                    { 4, true, "Inadequate Facilities" },
                    { 5, true, "Health and Sanitation Issues" },
                    { 6, true, "Transportation Challenges" }
                });

            migrationBuilder.InsertData(
                table: "RequestStatus",
                columns: new[] { "Id", "ColorCode", "StatusName" },
                values: new object[,]
                {
                    { 1, "#3300FF", "Open" },
                    { 2, "#FFFF00", "Assigned" },
                    { 3, "#FF6600", "Work in progress" },
                    { 4, "#FF0033", "Need more info" },
                    { 5, "#FF0000", "Rejected" },
                    { 6, "#33FF33", "Completed" },
                    { 7, "#FF0000", "Closed" }
                });

            migrationBuilder.InsertData(
                table: "RoleTypes",
                columns: new[] { "Id", "RoleTypeName" },
                values: new object[,]
                {
                    { 1, "End-Users" },
                    { 2, "Facility-Heads" },
                    { 3, "Assignees" },
                    { 4, "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName", "RoleTypeId" },
                values: new object[,]
                {
                    { 1, "Student", 1 },
                    { 2, "Teacher", 1 },
                    { 3, "Request Handler", 2 },
                    { 4, "Assignees", 3 },
                    { 5, "Admin", 4 }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountId", "Address", "AvatarPhoto", "Birthday", "CreatedAt", "Email", "Enable", "FullName", "Gender", "IsBanned", "Password", "PhoneNumber", "RefreshToken", "RefreshTokenExpiry", "RoleId", "StatusAccount", "UpdatedAt", "VerifyCode", "VerifyRefreshExpiry" },
                values: new object[,]
                {
                    { "AD729729", "Alaska", null, "1975/04/30", new DateTime(2024, 3, 20, 14, 12, 58, 91, DateTimeKind.Local).AddTicks(4152), "nguyentruongphi15032003@gmail.com", true, "Phi Đzai", "Orther", false, "$2a$12$GPbRVLdOyRw7H1yw/.fv/uStTWDcvprTAergcVbhc7zQ3/zFAqOtW", "0937888707", null, null, 5, "Active", null, null, null },
                    { "AS729729", "Bình Định", null, "1954/06/07", new DateTime(2024, 3, 20, 14, 12, 58, 91, DateTimeKind.Local).AddTicks(4148), "assignees@gmail.com", true, "Johnny Đãng", "Orther", false, "$2a$12$GPbRVLdOyRw7H1yw/.fv/uStTWDcvprTAergcVbhc7zQ3/zFAqOtW", "0909009003", null, null, 4, "Active", null, null, null },
                    { "FH729729", "Alaska", null, "1975/04/30", new DateTime(2024, 3, 20, 14, 12, 58, 91, DateTimeKind.Local).AddTicks(4150), "facility@gmail.com", true, "Ngọc Nhi", "Orther", false, "$2a$12$GPbRVLdOyRw7H1yw/.fv/uStTWDcvprTAergcVbhc7zQ3/zFAqOtW", "0909009004", null, null, 3, "Active", null, null, null },
                    { "ST729729", "Bình Chánh", null, "1975/04/30", new DateTime(2024, 3, 20, 14, 12, 58, 91, DateTimeKind.Local).AddTicks(4127), "student@gmail.com", true, "Duy Hiển", "Male", false, "$2a$12$GPbRVLdOyRw7H1yw/.fv/uStTWDcvprTAergcVbhc7zQ3/zFAqOtW", "0909009001", null, null, 1, "Active", null, null, null },
                    { "TC729729", "Bình Dương", null, "1945/09/02", new DateTime(2024, 3, 20, 14, 12, 58, 91, DateTimeKind.Local).AddTicks(4145), "teacher@gmail.com", true, "Duy Hiển", "Female", false, "$2a$12$GPbRVLdOyRw7H1yw/.fv/uStTWDcvprTAergcVbhc7zQ3/zFAqOtW", "0909009002", null, null, 2, "Verifying", null, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RoleId",
                table: "Accounts",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationHandleRequest_AccountId",
                table: "NotificationHandleRequest",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationHandleRequest_RequestId",
                table: "NotificationHandleRequest",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationQueue_AccountId",
                table: "NotificationQueue",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationQueue_AccountSenderId",
                table: "NotificationQueue",
                column: "AccountSenderId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationQueue_NotificationTypeId",
                table: "NotificationQueue",
                column: "NotificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationQueue_RequestId",
                table: "NotificationQueue",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationRemark_AccountId",
                table: "NotificationRemark",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationRemark_RequestId",
                table: "NotificationRemark",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessByAssignees_AccountId",
                table: "ProcessByAssignees",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessByAssignees_RequestId",
                table: "ProcessByAssignees",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Remarks_AccountId",
                table: "Remarks",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Remarks_RequestId",
                table: "Remarks",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_AccountId",
                table: "Requests",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ProblemId",
                table: "Requests",
                column: "ProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestStatusId",
                table: "Requests",
                column: "RequestStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RoomId",
                table: "Requests",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleTypeId",
                table: "Roles",
                column: "RoleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_DepartmentId",
                table: "Rooms",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationHandleRequest");

            migrationBuilder.DropTable(
                name: "NotificationQueue");

            migrationBuilder.DropTable(
                name: "NotificationRemark");

            migrationBuilder.DropTable(
                name: "ProcessByAssignees");

            migrationBuilder.DropTable(
                name: "Remarks");

            migrationBuilder.DropTable(
                name: "NotificationType");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Problem");

            migrationBuilder.DropTable(
                name: "RequestStatus");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "RoleTypes");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SampleUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "email_sending_type",
                columns: table => new
                {
                    uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 2, 22, 18, 9, 427, DateTimeKind.Local).AddTicks(7033)),
                    modified_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_time = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.uuid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mail_outbox",
                columns: table => new
                {
                    uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    from = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    subject = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    body = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    is_body_html = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: false, comment: "boolean value to describe if email contain html content"),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 2, 22, 18, 9, 428, DateTimeKind.Local).AddTicks(7550)),
                    modified_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_time = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.uuid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mail_outbox_attachment",
                columns: table => new
                {
                    uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    mail_outbox_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    filename = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    attachment_path = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    order = table.Column<int>(type: "int", nullable: false),
                    attachment_sha1 = table.Column<string>(type: "varchar(4096)", maxLength: 4096, nullable: false),
                    mime_media_type = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    mime_media_subtype = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    is_embeded_in_html = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: false, comment: "boolean value to describe if attachment is part of html mail"),
                    mime_cid = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 2, 22, 18, 9, 430, DateTimeKind.Local).AddTicks(6629)),
                    modified_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_time = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.uuid);
                    table.ForeignKey(
                        name: "FK__MAILOUTBOXATTACHMENT_MAILOUTBOXFK_TO_MAILOUTBOX",
                        column: x => x.mail_outbox_id,
                        principalTable: "mail_outbox",
                        principalColumn: "uuid");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mail_outbox_recipient",
                columns: table => new
                {
                    uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    mail_outbox_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    email_type_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 2, 22, 18, 9, 429, DateTimeKind.Local).AddTicks(2889)),
                    modified_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_time = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.uuid);
                    table.ForeignKey(
                        name: "FK__MAILOUTBOXRECIPIENT_EMAILSENDINGTYPEFK_TO_EMAILSENDINGTYPE",
                        column: x => x.email_type_id,
                        principalTable: "email_sending_type",
                        principalColumn: "uuid");
                    table.ForeignKey(
                        name: "FK__MAILOUTBOXRECIPIENT_MAILOUTBOXFK_TO_MAILOUTBOX",
                        column: x => x.mail_outbox_id,
                        principalTable: "mail_outbox",
                        principalColumn: "uuid");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "auth",
                columns: table => new
                {
                    uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    user_uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    remote_ip = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    remote_port = table.Column<int>(type: "int", maxLength: 5, nullable: false),
                    local_ip = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    local_port = table.Column<int>(type: "int", maxLength: 5, nullable: false),
                    token = table.Column<string>(type: "varchar(4096)", maxLength: 4096, nullable: false),
                    token_expires_in = table.Column<DateTime>(type: "datetime", nullable: false),
                    browser_user_agent = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    refresh_token_expires_in = table.Column<DateTime>(type: "datetime", nullable: false),
                    refresh_token = table.Column<string>(type: "varchar(4096)", maxLength: 4096, nullable: false),
                    logout_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 2, 22, 18, 9, 387, DateTimeKind.Local).AddTicks(316)),
                    modified_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_time = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.uuid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "chat",
                columns: table => new
                {
                    uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    Picture_Data = table.Column<byte[]>(type: "longblob", nullable: true),
                    picture_path = table.Column<string>(type: "longtext", nullable: true),
                    picture_path_file_extension = table.Column<string>(type: "longtext", nullable: true),
                    name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 2, 22, 18, 9, 387, DateTimeKind.Local).AddTicks(3416)),
                    modified_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by_uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true),
                    modified_by_uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true),
                    deleted_by_uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.uuid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "chat_invite_request",
                columns: table => new
                {
                    chat_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    target_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    requester_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    request_message = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 2, 22, 18, 9, 422, DateTimeKind.Local).AddTicks(9274)),
                    modified_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_invite_request", x => new { x.chat_id, x.target_id });
                    table.ForeignKey(
                        name: "FK__CHATINVITEREQUEST_CHATFK_TO_CHAT",
                        column: x => x.chat_id,
                        principalTable: "chat",
                        principalColumn: "uuid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "chat_relation_to_user",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    chat_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    is_admin = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: false, comment: "boolean value to describe if chatmember is chat-admin"),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 2, 22, 18, 9, 400, DateTimeKind.Local).AddTicks(8109)),
                    modified_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true),
                    created_by_uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true),
                    modified_by_uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true),
                    deleted_by_uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chat_relation_to_user", x => new { x.user_id, x.chat_id });
                    table.ForeignKey(
                        name: "FK__CHATRELATIONTOUSER_CHATFK_TO_CHAT",
                        column: x => x.chat_id,
                        principalTable: "chat",
                        principalColumn: "uuid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "friendship_request",
                columns: table => new
                {
                    requester_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    target_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    request_message = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 2, 22, 18, 9, 352, DateTimeKind.Local).AddTicks(337)),
                    modified_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_friendship_request", x => new { x.requester_id, x.target_id });
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "message",
                columns: table => new
                {
                    uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    chat_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    user_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    text = table.Column<string>(type: "TEXT", nullable: true),
                    binary_content_base64 = table.Column<byte[]>(type: "longblob", nullable: true),
                    binary_content_path = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true),
                    binary_content_base64_mime_type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 2, 22, 18, 9, 409, DateTimeKind.Local).AddTicks(8368)),
                    modified_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by_uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true),
                    modified_by_uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true),
                    deleted_by_uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.uuid);
                    table.ForeignKey(
                        name: "FK__MESSAGE_CHATFK_TO_CHAT",
                        column: x => x.chat_id,
                        principalTable: "chat",
                        principalColumn: "uuid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "message_outbox",
                columns: table => new
                {
                    message_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    user_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 2, 22, 18, 9, 420, DateTimeKind.Local).AddTicks(6051)),
                    modified_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_message_outbox", x => new { x.message_id, x.user_id });
                    table.ForeignKey(
                        name: "FK__MESSAGEOUTBOX_MESSAGEFK_TO_CHAT",
                        column: x => x.message_id,
                        principalTable: "message",
                        principalColumn: "uuid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    name = table.Column<string>(type: "longtext", nullable: false),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 2, 22, 18, 9, 355, DateTimeKind.Local).AddTicks(5613)),
                    modified_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by_uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true),
                    modified_by_uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true),
                    deleted_by_uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.uuid);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    user_type_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, defaultValue: new Guid("c92db314-765b-46dd-bf40-ef7d5a5abd7b")),
                    name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    password = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    first_name = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    last_name = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "datetime", nullable: false),
                    activation_token = table.Column<string>(type: "varchar(4096)", maxLength: 4096, nullable: true),
                    activation_code = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true),
                    activation_datetime = table.Column<DateTime>(type: "datetime", nullable: true),
                    password_reset_code = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true),
                    password_reset_token = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true),
                    password_reset_token_expires_in = table.Column<DateTime>(type: "datetime", nullable: true),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Picture_Data = table.Column<byte[]>(type: "longblob", nullable: true),
                    profile_picture_path = table.Column<string>(type: "longtext", nullable: true),
                    profile_picture_fileext = table.Column<string>(type: "longtext", nullable: true),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 2, 22, 18, 9, 375, DateTimeKind.Local).AddTicks(3447)),
                    modified_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by_uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true),
                    modified_by_uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true),
                    deleted_by_uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.uuid);
                    table.ForeignKey(
                        name: "FK__USER_CREATEDBYUSERFK_TO_USER",
                        column: x => x.created_by_uuid,
                        principalTable: "user",
                        principalColumn: "uuid");
                    table.ForeignKey(
                        name: "FK__USER_DELETEDBYUSERFK_TO_USER",
                        column: x => x.deleted_by_uuid,
                        principalTable: "user",
                        principalColumn: "uuid");
                    table.ForeignKey(
                        name: "FK__USER_LASTMODIFIEDBYUSERFK_TO_USER",
                        column: x => x.modified_by_uuid,
                        principalTable: "user",
                        principalColumn: "uuid");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_has_relation_to_friend",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    friend_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 2, 22, 18, 9, 341, DateTimeKind.Local).AddTicks(7384)),
                    modified_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true),
                    created_by_uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true),
                    modified_by_uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true),
                    deleted_by_uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_has_relation_to_friend", x => new { x.user_id, x.friend_id });
                    table.ForeignKey(
                        name: "FK__USERHASRELATIONTOFRIEND_CREATEDBYUSERFK_TO_USER",
                        column: x => x.created_by_uuid,
                        principalTable: "user",
                        principalColumn: "uuid");
                    table.ForeignKey(
                        name: "FK__USERHASRELATIONTOFRIEND_DELETEDBYUSERFK_TO_USER",
                        column: x => x.deleted_by_uuid,
                        principalTable: "user",
                        principalColumn: "uuid");
                    table.ForeignKey(
                        name: "FK__USERHASRELATIONTOFRIEND_LASTMODIFIEDBYUSERFK_TO_USER",
                        column: x => x.modified_by_uuid,
                        principalTable: "user",
                        principalColumn: "uuid");
                    table.ForeignKey(
                        name: "FK__USERHASRELATIONTOFRIEND_USERFK_TO_USER",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "uuid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__USERHASRELATIONTOFRIEND_USERFRIENDFK_TO_USER",
                        column: x => x.friend_id,
                        principalTable: "user",
                        principalColumn: "uuid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_has_relation_to_role",
                columns: table => new
                {
                    uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    user_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    role_id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, defaultValue: new Guid("8937df9c-fbef-11ed-8f81-7085c294413b")),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 2, 22, 18, 9, 361, DateTimeKind.Local).AddTicks(4657)),
                    modified_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by_uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true),
                    modified_by_uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true),
                    deleted_by_uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.uuid);
                    table.ForeignKey(
                        name: "FK__USERHASRELATIONTOROLE_CREATEDBYUSERFK_TO_USER",
                        column: x => x.created_by_uuid,
                        principalTable: "user",
                        principalColumn: "uuid");
                    table.ForeignKey(
                        name: "FK__USERHASRELATIONTOROLE_DELETEDBYUSERFK_TO_USER",
                        column: x => x.deleted_by_uuid,
                        principalTable: "user",
                        principalColumn: "uuid");
                    table.ForeignKey(
                        name: "FK__USERHASRELATIONTOROLE_LASTMODIFIEDBYUSERFK_TO_USER",
                        column: x => x.modified_by_uuid,
                        principalTable: "user",
                        principalColumn: "uuid");
                    table.ForeignKey(
                        name: "FK__USERHASRELATIONTOROLE_ROLEFK_TO_ROLE",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "uuid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__USERHASRELATIONTOROLE_USERFK_TO_USER",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "uuid",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_type",
                columns: table => new
                {
                    uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false),
                    name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 2, 22, 18, 9, 334, DateTimeKind.Local).AddTicks(6344)),
                    modified_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by_uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true),
                    modified_by_uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true),
                    deleted_by_uuid = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.uuid);
                    table.ForeignKey(
                        name: "FK__USERTYPE_CREATEDBYUSERFK_TO_USER",
                        column: x => x.created_by_uuid,
                        principalTable: "user",
                        principalColumn: "uuid");
                    table.ForeignKey(
                        name: "FK__USERTYPE_DELETEDBYUSERFK_TO_USER",
                        column: x => x.deleted_by_uuid,
                        principalTable: "user",
                        principalColumn: "uuid");
                    table.ForeignKey(
                        name: "FK__USERTYPE_LASTMODIFIEDBYUSERFK_TO_USER",
                        column: x => x.modified_by_uuid,
                        principalTable: "user",
                        principalColumn: "uuid");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "email_sending_type",
                columns: new[] { "uuid", "created_time", "deleted_time", "modified_time", "name" },
                values: new object[,]
                {
                    { new Guid("c4f357ff-1c85-4e3c-b6b2-21ef4afba71f"), new DateTime(2024, 1, 2, 22, 18, 9, 431, DateTimeKind.Local).AddTicks(6310), null, null, "to" },
                    { new Guid("c52db414-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 2, 22, 18, 9, 431, DateTimeKind.Local).AddTicks(6328), null, null, "bcc" },
                    { new Guid("c62db414-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 2, 22, 18, 9, 431, DateTimeKind.Local).AddTicks(6324), null, null, "cc" }
                });

            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "uuid", "created_by_uuid", "created_time", "deleted_by_uuid", "deleted_time", "modified_by_uuid", "modified_time", "name" },
                values: new object[,]
                {
                    { new Guid("776848df-084f-11ee-b2c8-7085c294413b"), null, new DateTime(2024, 1, 2, 22, 18, 9, 361, DateTimeKind.Local).AddTicks(1264), null, null, null, null, "Admin" },
                    { new Guid("8937df9c-fbef-11ed-8f81-7085c294413b"), null, new DateTime(2024, 1, 2, 22, 18, 9, 361, DateTimeKind.Local).AddTicks(1289), null, null, null, null, "User" },
                    { new Guid("d411a531-fbea-11ed-8f81-7085c294413b"), null, new DateTime(2024, 1, 2, 22, 18, 9, 361, DateTimeKind.Local).AddTicks(1292), null, null, null, null, "Root" }
                });

            migrationBuilder.InsertData(
                table: "user_type",
                columns: new[] { "uuid", "created_by_uuid", "created_time", "deleted_by_uuid", "deleted_time", "modified_by_uuid", "modified_time", "name" },
                values: new object[,]
                {
                    { new Guid("c3f257ff-1c85-4e3c-b6b2-21ef4afba71f"), null, new DateTime(2024, 1, 2, 22, 18, 9, 431, DateTimeKind.Local).AddTicks(5709), null, null, null, null, "Root" },
                    { new Guid("c3f267ff-1c85-4e3c-b6b2-21ef4afba71f"), null, new DateTime(2024, 1, 2, 22, 18, 9, 431, DateTimeKind.Local).AddTicks(5702), null, null, null, null, "Admin" },
                    { new Guid("c92db314-765b-46dd-bf40-ef7d5a5abd7b"), null, new DateTime(2024, 1, 2, 22, 18, 9, 431, DateTimeKind.Local).AddTicks(5656), null, null, null, null, "User" }
                });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "uuid", "activation_code", "activation_datetime", "activation_token", "created_by_uuid", "date_of_birth", "deleted_by_uuid", "deleted_time", "email", "first_name", "modified_by_uuid", "modified_time", "last_name", "password", "password_reset_code", "password_reset_token_expires_in", "password_reset_token", "phone", "name", "user_type_id" },
                values: new object[] { new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), null, new DateTime(2024, 1, 2, 22, 18, 9, 431, DateTimeKind.Local).AddTicks(5923), null, null, new DateTime(2024, 1, 2, 22, 18, 9, 431, DateTimeKind.Local).AddTicks(5916), null, null, "root@localhost.local", "Root", null, null, "Root", "root@localhost.local", null, null, null, null, "Root", new Guid("c3f257ff-1c85-4e3c-b6b2-21ef4afba71f") });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "uuid", "activation_code", "activation_datetime", "activation_token", "created_by_uuid", "created_time", "date_of_birth", "deleted_by_uuid", "deleted_time", "email", "first_name", "modified_by_uuid", "modified_time", "last_name", "password", "password_reset_code", "password_reset_token_expires_in", "password_reset_token", "phone", "name", "user_type_id" },
                values: new object[,]
                {
                    { new Guid("c3f257ff-1c85-4e3c-b6b2-21ef2afba51f"), null, new DateTime(2024, 1, 2, 22, 18, 9, 433, DateTimeKind.Local).AddTicks(1366), null, new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 2, 22, 18, 9, 433, DateTimeKind.Local).AddTicks(1380), new DateTime(1873, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "skywalker.luke.sample@web.net", "Luke", null, null, "Skywalker", "NR*87utx", null, null, null, "4917512300", "luke-skywalker", new Guid("c92db314-765b-46dd-bf40-ef7d5a5abd7b") },
                    { new Guid("c3f257ff-1c85-4e3c-b6b2-21ef3afba61f"), null, new DateTime(2024, 1, 2, 22, 18, 9, 433, DateTimeKind.Local).AddTicks(2259), null, new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 2, 22, 18, 9, 433, DateTimeKind.Local).AddTicks(2271), new DateTime(1858, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "thehut.jabba.sample@web.net", "Jabba", null, null, "TheHut", "BO$67plf", null, null, null, "4917512300", "jabba-the-hut", new Guid("c92db314-765b-46dd-bf40-ef7d5a5abd7b") },
                    { new Guid("c3f257ff-1c85-4e3c-b6b2-21ef5afba61f"), null, new DateTime(2024, 1, 2, 22, 18, 9, 433, DateTimeKind.Local).AddTicks(2987), null, new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 2, 22, 18, 9, 433, DateTimeKind.Local).AddTicks(3002), new DateTime(1845, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "johnson.carl.sample@web.net", "Carl", null, null, "Johnson", "CE#80txn", null, null, null, "4917512300", "carl-johnson", new Guid("c92db314-765b-46dd-bf40-ef7d5a5abd7b") },
                    { new Guid("c3f257ff-1c85-4e3c-b6b2-21ef7afba41f"), null, new DateTime(2024, 1, 2, 22, 18, 9, 433, DateTimeKind.Local).AddTicks(627), null, new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 2, 22, 18, 9, 433, DateTimeKind.Local).AddTicks(641), new DateTime(1852, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "organa.laia.sample@web.net", "Laia", null, null, "Organa", "XB*39avb", null, null, null, "4917512300", "princess-laia", new Guid("c92db314-765b-46dd-bf40-ef7d5a5abd7b") },
                    { new Guid("c3f257ff-1c85-4e3c-b6b2-21ef8afba31f"), null, new DateTime(2024, 1, 2, 22, 18, 9, 432, DateTimeKind.Local).AddTicks(9831), null, new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 2, 22, 18, 9, 432, DateTimeKind.Local).AddTicks(9843), new DateTime(1861, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "maul.darth.sample@web.net", "Darth", null, null, "Maul", "CM&44tge", null, null, null, "4917512300", "darth-maul", new Guid("c92db314-765b-46dd-bf40-ef7d5a5abd7b") },
                    { new Guid("c3f257ff-1c85-4e3c-b6b2-21ef9afba21f"), null, new DateTime(2024, 1, 2, 22, 18, 9, 432, DateTimeKind.Local).AddTicks(8994), null, new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 2, 22, 18, 9, 432, DateTimeKind.Local).AddTicks(9006), new DateTime(1861, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "vader.darth.sample@web.net", "Darth", null, null, "Vader", "MG#81svd", null, null, null, "4917512300", "darth-vader", new Guid("c92db314-765b-46dd-bf40-ef7d5a5abd7b") }
                });

            migrationBuilder.InsertData(
                table: "user_has_relation_to_role",
                columns: new[] { "uuid", "created_by_uuid", "created_time", "deleted_by_uuid", "deleted_time", "modified_by_uuid", "modified_time", "role_id", "user_id" },
                values: new object[,]
                {
                    { new Guid("37753954-ad8b-466f-bc3e-b378f10e80f1"), new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 2, 22, 18, 9, 431, DateTimeKind.Local).AddTicks(6184), null, null, null, null, new Guid("8937df9c-fbef-11ed-8f81-7085c294413b"), new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b") },
                    { new Guid("93cb8c0a-4baf-449c-ae05-7512711b661a"), new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 2, 22, 18, 9, 431, DateTimeKind.Local).AddTicks(6150), null, null, null, null, new Guid("776848df-084f-11ee-b2c8-7085c294413b"), new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b") },
                    { new Guid("f7483142-a94f-496c-a395-6e99e7fde8cf"), new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 2, 22, 18, 9, 431, DateTimeKind.Local).AddTicks(6091), null, null, null, null, new Guid("d411a531-fbea-11ed-8f81-7085c294413b"), new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b") },
                    { new Guid("00716485-5dbd-4e8f-ba71-721dfef245be"), new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 2, 22, 18, 9, 432, DateTimeKind.Local).AddTicks(9074), null, null, null, null, new Guid("8937df9c-fbef-11ed-8f81-7085c294413b"), new Guid("c3f257ff-1c85-4e3c-b6b2-21ef9afba21f") },
                    { new Guid("03bd0a66-1e14-4055-a264-cc24bf85952a"), new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 2, 22, 18, 9, 433, DateTimeKind.Local).AddTicks(1456), null, null, null, null, new Guid("8937df9c-fbef-11ed-8f81-7085c294413b"), new Guid("c3f257ff-1c85-4e3c-b6b2-21ef2afba51f") },
                    { new Guid("3ec68557-16d1-4fb6-aac7-09ae233853f7"), new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 2, 22, 18, 9, 433, DateTimeKind.Local).AddTicks(719), null, null, null, null, new Guid("8937df9c-fbef-11ed-8f81-7085c294413b"), new Guid("c3f257ff-1c85-4e3c-b6b2-21ef7afba41f") },
                    { new Guid("68f7d16e-8eb1-431f-ad7f-3ce60b85cf90"), new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 2, 22, 18, 9, 433, DateTimeKind.Local).AddTicks(3072), null, null, null, null, new Guid("8937df9c-fbef-11ed-8f81-7085c294413b"), new Guid("c3f257ff-1c85-4e3c-b6b2-21ef5afba61f") },
                    { new Guid("95f69e57-6b98-41ee-8d23-c16ffab6739c"), new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 2, 22, 18, 9, 432, DateTimeKind.Local).AddTicks(9906), null, null, null, null, new Guid("8937df9c-fbef-11ed-8f81-7085c294413b"), new Guid("c3f257ff-1c85-4e3c-b6b2-21ef8afba31f") },
                    { new Guid("f06a708b-3e5c-46a3-94f5-ef3f6dafce03"), new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 2, 22, 18, 9, 433, DateTimeKind.Local).AddTicks(2336), null, null, null, null, new Guid("8937df9c-fbef-11ed-8f81-7085c294413b"), new Guid("c3f257ff-1c85-4e3c-b6b2-21ef3afba61f") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_auth_user_uuid",
                table: "auth",
                column: "user_uuid");

            migrationBuilder.CreateIndex(
                name: "IX_chat_created_by_uuid",
                table: "chat",
                column: "created_by_uuid");

            migrationBuilder.CreateIndex(
                name: "IX_chat_deleted_by_uuid",
                table: "chat",
                column: "deleted_by_uuid");

            migrationBuilder.CreateIndex(
                name: "IX_chat_modified_by_uuid",
                table: "chat",
                column: "modified_by_uuid");

            migrationBuilder.CreateIndex(
                name: "IDX_FK__CHATINVITEREQUEST_CHAT_TO_CHAT",
                table: "chat_invite_request",
                column: "chat_id");

            migrationBuilder.CreateIndex(
                name: "IDX_FK__CHATINVITEREQUEST_REQUESTERUSER_TO_USER",
                table: "chat_invite_request",
                column: "requester_id");

            migrationBuilder.CreateIndex(
                name: "IDX_FK__CHATINVITEREQUEST_TARGETUSER_TO_USER",
                table: "chat_invite_request",
                column: "target_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_relation_to_user_chat_id",
                table: "chat_relation_to_user",
                column: "chat_id");

            migrationBuilder.CreateIndex(
                name: "IX_chat_relation_to_user_created_by_uuid",
                table: "chat_relation_to_user",
                column: "created_by_uuid");

            migrationBuilder.CreateIndex(
                name: "IX_chat_relation_to_user_deleted_by_uuid",
                table: "chat_relation_to_user",
                column: "deleted_by_uuid");

            migrationBuilder.CreateIndex(
                name: "IX_chat_relation_to_user_modified_by_uuid",
                table: "chat_relation_to_user",
                column: "modified_by_uuid");

            migrationBuilder.CreateIndex(
                name: "IDX_FK__FRIENDSHIPREQUEST_REQUESTERUSER_TO_USER",
                table: "friendship_request",
                column: "requester_id");

            migrationBuilder.CreateIndex(
                name: "IDX_FK__FRIENDSHIPREQUEST_TARGETUSER_TO_USER",
                table: "friendship_request",
                column: "target_id");

            migrationBuilder.CreateIndex(
                name: "IX_mail_outbox_attachment_mail_outbox_id",
                table: "mail_outbox_attachment",
                column: "mail_outbox_id");

            migrationBuilder.CreateIndex(
                name: "IX_mail_outbox_recipient_email_type_id",
                table: "mail_outbox_recipient",
                column: "email_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_mail_outbox_recipient_mail_outbox_id",
                table: "mail_outbox_recipient",
                column: "mail_outbox_id");

            migrationBuilder.CreateIndex(
                name: "IX_message_chat_id",
                table: "message",
                column: "chat_id");

            migrationBuilder.CreateIndex(
                name: "IX_message_created_by_uuid",
                table: "message",
                column: "created_by_uuid");

            migrationBuilder.CreateIndex(
                name: "IX_message_deleted_by_uuid",
                table: "message",
                column: "deleted_by_uuid");

            migrationBuilder.CreateIndex(
                name: "IX_message_modified_by_uuid",
                table: "message",
                column: "modified_by_uuid");

            migrationBuilder.CreateIndex(
                name: "IX_message_user_id",
                table: "message",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_message_outbox_user_id",
                table: "message_outbox",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_created_by_uuid",
                table: "role",
                column: "created_by_uuid");

            migrationBuilder.CreateIndex(
                name: "IX_role_deleted_by_uuid",
                table: "role",
                column: "deleted_by_uuid");

            migrationBuilder.CreateIndex(
                name: "IX_role_modified_by_uuid",
                table: "role",
                column: "modified_by_uuid");

            migrationBuilder.CreateIndex(
                name: "IX_user_created_by_uuid",
                table: "user",
                column: "created_by_uuid");

            migrationBuilder.CreateIndex(
                name: "IX_user_deleted_by_uuid",
                table: "user",
                column: "deleted_by_uuid");

            migrationBuilder.CreateIndex(
                name: "IX_user_modified_by_uuid",
                table: "user",
                column: "modified_by_uuid");

            migrationBuilder.CreateIndex(
                name: "IX_user_user_type_id",
                table: "user",
                column: "user_type_id");

            migrationBuilder.CreateIndex(
                name: "IDX_FK__USERHASRELATIONTOFRIEND_USERFK_TO_USER",
                table: "user_has_relation_to_friend",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IDX_FK__USERHASRELATIONTOFRIEND_USERFRIENDFK_TO_USER",
                table: "user_has_relation_to_friend",
                column: "friend_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_has_relation_to_friend_created_by_uuid",
                table: "user_has_relation_to_friend",
                column: "created_by_uuid");

            migrationBuilder.CreateIndex(
                name: "IX_user_has_relation_to_friend_deleted_by_uuid",
                table: "user_has_relation_to_friend",
                column: "deleted_by_uuid");

            migrationBuilder.CreateIndex(
                name: "IX_user_has_relation_to_friend_modified_by_uuid",
                table: "user_has_relation_to_friend",
                column: "modified_by_uuid");

            migrationBuilder.CreateIndex(
                name: "IDX_FK__USERHASRELATIONTOROLE_ROLEFK_TO_ROLE",
                table: "user_has_relation_to_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IDX_FK__USERHASRELATIONTOROLE_USERFK_TO_USER",
                table: "user_has_relation_to_role",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_has_relation_to_role_created_by_uuid",
                table: "user_has_relation_to_role",
                column: "created_by_uuid");

            migrationBuilder.CreateIndex(
                name: "IX_user_has_relation_to_role_deleted_by_uuid",
                table: "user_has_relation_to_role",
                column: "deleted_by_uuid");

            migrationBuilder.CreateIndex(
                name: "IX_user_has_relation_to_role_modified_by_uuid",
                table: "user_has_relation_to_role",
                column: "modified_by_uuid");

            migrationBuilder.CreateIndex(
                name: "IX_user_type_created_by_uuid",
                table: "user_type",
                column: "created_by_uuid");

            migrationBuilder.CreateIndex(
                name: "IX_user_type_deleted_by_uuid",
                table: "user_type",
                column: "deleted_by_uuid");

            migrationBuilder.CreateIndex(
                name: "IX_user_type_modified_by_uuid",
                table: "user_type",
                column: "modified_by_uuid");

            migrationBuilder.AddForeignKey(
                name: "FK__AUTH_USERID_TO_USER",
                table: "auth",
                column: "user_uuid",
                principalTable: "user",
                principalColumn: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK__CHAT_CREATEDBYUSERFK_TO_USER",
                table: "chat",
                column: "created_by_uuid",
                principalTable: "user",
                principalColumn: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK__CHAT_DELETEDBYUSERFK_TO_USER",
                table: "chat",
                column: "deleted_by_uuid",
                principalTable: "user",
                principalColumn: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK__CHAT_LASTMODIFIEDBYUSERFK_TO_USER",
                table: "chat",
                column: "modified_by_uuid",
                principalTable: "user",
                principalColumn: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK__CHATINVITEREQUEST_REQUESTERUSERFK_TO_USER",
                table: "chat_invite_request",
                column: "requester_id",
                principalTable: "user",
                principalColumn: "uuid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__CHATINVITEREQUEST_TARGETUSERFK_TO_USER",
                table: "chat_invite_request",
                column: "target_id",
                principalTable: "user",
                principalColumn: "uuid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__CHATRELATIONTOUSER_CREATEDBYUSERFK_TO_USER",
                table: "chat_relation_to_user",
                column: "created_by_uuid",
                principalTable: "user",
                principalColumn: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK__CHATRELATIONTOUSER_DELETEDBYUSERFK_TO_USER",
                table: "chat_relation_to_user",
                column: "deleted_by_uuid",
                principalTable: "user",
                principalColumn: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK__CHATRELATIONTOUSER_LASTMODIFIEDBYUSERFK_TO_USER",
                table: "chat_relation_to_user",
                column: "modified_by_uuid",
                principalTable: "user",
                principalColumn: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK__CHATRELATIONTOUSER_USERFK_TO_USER",
                table: "chat_relation_to_user",
                column: "user_id",
                principalTable: "user",
                principalColumn: "uuid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__FRIENDSHIPREQUEST_REQUESTUSERFK_TO_USER",
                table: "friendship_request",
                column: "requester_id",
                principalTable: "user",
                principalColumn: "uuid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__FRIENDSHIPREQUEST_TARGETUSERFK_TO_USER",
                table: "friendship_request",
                column: "target_id",
                principalTable: "user",
                principalColumn: "uuid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__MESSAGE_CREATEDBYUSERFK_TO_USER",
                table: "message",
                column: "created_by_uuid",
                principalTable: "user",
                principalColumn: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK__MESSAGE_DELETEDBYUSERFK_TO_USER",
                table: "message",
                column: "deleted_by_uuid",
                principalTable: "user",
                principalColumn: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK__MESSAGE_LASTMODIFIEDBYUSERFK_TO_USER",
                table: "message",
                column: "modified_by_uuid",
                principalTable: "user",
                principalColumn: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK__MESSAGE_USERFK_TO_USER",
                table: "message",
                column: "user_id",
                principalTable: "user",
                principalColumn: "uuid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__MESSAGEOUTBOX_USERFK_TO_USER",
                table: "message_outbox",
                column: "user_id",
                principalTable: "user",
                principalColumn: "uuid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__ROLE_CREATEDBYUSERFK_TO_USER",
                table: "role",
                column: "created_by_uuid",
                principalTable: "user",
                principalColumn: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK__ROLE_DELETEDBYUSERFK_TO_USER",
                table: "role",
                column: "deleted_by_uuid",
                principalTable: "user",
                principalColumn: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK__ROLE_LASTMODIFIEDBYUSERFK_TO_USER",
                table: "role",
                column: "modified_by_uuid",
                principalTable: "user",
                principalColumn: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK__USER_USERTYPEFK_TO_USERTYPE",
                table: "user",
                column: "user_type_id",
                principalTable: "user_type",
                principalColumn: "uuid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__USERTYPE_CREATEDBYUSERFK_TO_USER",
                table: "user_type");

            migrationBuilder.DropForeignKey(
                name: "FK__USERTYPE_DELETEDBYUSERFK_TO_USER",
                table: "user_type");

            migrationBuilder.DropForeignKey(
                name: "FK__USERTYPE_LASTMODIFIEDBYUSERFK_TO_USER",
                table: "user_type");

            migrationBuilder.DropTable(
                name: "auth");

            migrationBuilder.DropTable(
                name: "chat_invite_request");

            migrationBuilder.DropTable(
                name: "chat_relation_to_user");

            migrationBuilder.DropTable(
                name: "friendship_request");

            migrationBuilder.DropTable(
                name: "mail_outbox_attachment");

            migrationBuilder.DropTable(
                name: "mail_outbox_recipient");

            migrationBuilder.DropTable(
                name: "message_outbox");

            migrationBuilder.DropTable(
                name: "user_has_relation_to_friend");

            migrationBuilder.DropTable(
                name: "user_has_relation_to_role");

            migrationBuilder.DropTable(
                name: "email_sending_type");

            migrationBuilder.DropTable(
                name: "mail_outbox");

            migrationBuilder.DropTable(
                name: "message");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "chat");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "user_type");
        }
    }
}

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
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 3, 0, 32, 51, 869, DateTimeKind.Local).AddTicks(4995)),
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
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 3, 0, 32, 51, 870, DateTimeKind.Local).AddTicks(5422)),
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
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 3, 0, 32, 51, 872, DateTimeKind.Local).AddTicks(4237)),
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
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 3, 0, 32, 51, 871, DateTimeKind.Local).AddTicks(733)),
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
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 3, 0, 32, 51, 828, DateTimeKind.Local).AddTicks(852)),
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
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 3, 0, 32, 51, 828, DateTimeKind.Local).AddTicks(3978)),
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
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 3, 0, 32, 51, 864, DateTimeKind.Local).AddTicks(7613)),
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
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 3, 0, 32, 51, 841, DateTimeKind.Local).AddTicks(8873)),
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
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 3, 0, 32, 51, 793, DateTimeKind.Local).AddTicks(2709)),
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
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 3, 0, 32, 51, 851, DateTimeKind.Local).AddTicks(455)),
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
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 3, 0, 32, 51, 862, DateTimeKind.Local).AddTicks(3142)),
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
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 3, 0, 32, 51, 796, DateTimeKind.Local).AddTicks(7773)),
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
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 3, 0, 32, 51, 816, DateTimeKind.Local).AddTicks(4598)),
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
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 3, 0, 32, 51, 782, DateTimeKind.Local).AddTicks(9414)),
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
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 3, 0, 32, 51, 802, DateTimeKind.Local).AddTicks(6498)),
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
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2024, 1, 3, 0, 32, 51, 775, DateTimeKind.Local).AddTicks(8985)),
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
                    { new Guid("c4f357ff-1c85-4e3c-b6b2-21ef4afba71f"), new DateTime(2024, 1, 3, 0, 32, 51, 873, DateTimeKind.Local).AddTicks(3886), null, null, "to" },
                    { new Guid("c52db414-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 3, 0, 32, 51, 873, DateTimeKind.Local).AddTicks(3900), null, null, "bcc" },
                    { new Guid("c62db414-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 3, 0, 32, 51, 873, DateTimeKind.Local).AddTicks(3896), null, null, "cc" }
                });

            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "uuid", "created_by_uuid", "created_time", "deleted_by_uuid", "deleted_time", "modified_by_uuid", "modified_time", "name" },
                values: new object[,]
                {
                    { new Guid("776848df-084f-11ee-b2c8-7085c294413b"), null, new DateTime(2024, 1, 3, 0, 32, 51, 802, DateTimeKind.Local).AddTicks(2793), null, null, null, null, "Admin" },
                    { new Guid("8937df9c-fbef-11ed-8f81-7085c294413b"), null, new DateTime(2024, 1, 3, 0, 32, 51, 802, DateTimeKind.Local).AddTicks(2817), null, null, null, null, "User" },
                    { new Guid("d411a531-fbea-11ed-8f81-7085c294413b"), null, new DateTime(2024, 1, 3, 0, 32, 51, 802, DateTimeKind.Local).AddTicks(2820), null, null, null, null, "Root" }
                });

            migrationBuilder.InsertData(
                table: "user_type",
                columns: new[] { "uuid", "created_by_uuid", "created_time", "deleted_by_uuid", "deleted_time", "modified_by_uuid", "modified_time", "name" },
                values: new object[,]
                {
                    { new Guid("c3f257ff-1c85-4e3c-b6b2-21ef4afba71f"), null, new DateTime(2024, 1, 3, 0, 32, 51, 873, DateTimeKind.Local).AddTicks(3326), null, null, null, null, "Root" },
                    { new Guid("c3f267ff-1c85-4e3c-b6b2-21ef4afba71f"), null, new DateTime(2024, 1, 3, 0, 32, 51, 873, DateTimeKind.Local).AddTicks(3319), null, null, null, null, "Admin" },
                    { new Guid("c92db314-765b-46dd-bf40-ef7d5a5abd7b"), null, new DateTime(2024, 1, 3, 0, 32, 51, 873, DateTimeKind.Local).AddTicks(3271), null, null, null, null, "User" }
                });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "uuid", "activation_code", "activation_datetime", "activation_token", "created_by_uuid", "date_of_birth", "deleted_by_uuid", "deleted_time", "email", "first_name", "modified_by_uuid", "modified_time", "last_name", "password", "password_reset_code", "password_reset_token_expires_in", "password_reset_token", "phone", "name", "user_type_id" },
                values: new object[] { new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), null, new DateTime(2024, 1, 3, 0, 32, 51, 873, DateTimeKind.Local).AddTicks(3546), null, null, new DateTime(2024, 1, 3, 0, 32, 51, 873, DateTimeKind.Local).AddTicks(3539), null, null, "root@localhost.local", "Root", null, null, "Root", "root@localhost.local", null, null, null, null, "Root", new Guid("c3f257ff-1c85-4e3c-b6b2-21ef4afba71f") });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "uuid", "activation_code", "activation_datetime", "activation_token", "created_by_uuid", "created_time", "date_of_birth", "deleted_by_uuid", "deleted_time", "email", "first_name", "modified_by_uuid", "modified_time", "last_name", "password", "password_reset_code", "password_reset_token_expires_in", "password_reset_token", "phone", "name", "user_type_id" },
                values: new object[,]
                {
                    { new Guid("c3f257ff-1c85-4e3c-b6b2-21ef2afba51f"), null, new DateTime(2024, 1, 3, 0, 32, 51, 874, DateTimeKind.Local).AddTicks(8685), null, new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 3, 0, 32, 51, 874, DateTimeKind.Local).AddTicks(8695), new DateTime(1851, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "skywalker.luke.sample@web.net", "Luke", null, null, "Skywalker", "WT@14qia", null, null, null, "+49175123454", "luke-skywalker", new Guid("c92db314-765b-46dd-bf40-ef7d5a5abd7b") },
                    { new Guid("c3f257ff-1c85-4e3c-b6b2-21ef3afba61f"), null, new DateTime(2024, 1, 3, 0, 32, 51, 874, DateTimeKind.Local).AddTicks(9408), null, new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 3, 0, 32, 51, 874, DateTimeKind.Local).AddTicks(9424), new DateTime(1860, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "thehut.jabba.sample@web.net", "Jabba", null, null, "TheHut", "EX^45jcf", null, null, null, "+49175123455", "jabba-the-hut", new Guid("c92db314-765b-46dd-bf40-ef7d5a5abd7b") },
                    { new Guid("c3f257ff-1c85-4e3c-b6b2-21ef5afba61f"), null, new DateTime(2024, 1, 3, 0, 32, 51, 875, DateTimeKind.Local).AddTicks(233), null, new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 3, 0, 32, 51, 875, DateTimeKind.Local).AddTicks(247), new DateTime(1876, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "johnson.carl.sample@web.net", "Carl", null, null, "Johnson", "HE$91xhq", null, null, null, "+49175123456", "carl-johnson", new Guid("c92db314-765b-46dd-bf40-ef7d5a5abd7b") },
                    { new Guid("c3f257ff-1c85-4e3c-b6b2-21ef7afba41f"), null, new DateTime(2024, 1, 3, 0, 32, 51, 874, DateTimeKind.Local).AddTicks(7861), null, new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 3, 0, 32, 51, 874, DateTimeKind.Local).AddTicks(7874), new DateTime(1864, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "organa.laia.sample@web.net", "Laia", null, null, "Organa", "HS$38bzt", null, null, null, "+49175123453", "princess-laia", new Guid("c92db314-765b-46dd-bf40-ef7d5a5abd7b") },
                    { new Guid("c3f257ff-1c85-4e3c-b6b2-21ef8afba31f"), null, new DateTime(2024, 1, 3, 0, 32, 51, 874, DateTimeKind.Local).AddTicks(7097), null, new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 3, 0, 32, 51, 874, DateTimeKind.Local).AddTicks(7112), new DateTime(1847, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "maul.darth.sample@web.net", "Darth", null, null, "Maul", "OD&36xiq", null, null, null, "+49175123452", "darth-maul", new Guid("c92db314-765b-46dd-bf40-ef7d5a5abd7b") },
                    { new Guid("c3f257ff-1c85-4e3c-b6b2-21ef9afba21f"), null, new DateTime(2024, 1, 3, 0, 32, 51, 874, DateTimeKind.Local).AddTicks(6263), null, new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 3, 0, 32, 51, 874, DateTimeKind.Local).AddTicks(6277), new DateTime(1877, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "vader.darth.sample@web.net", "Darth", null, null, "Vader", "XB#20jbq", null, null, null, "+49175123451", "darth-vader", new Guid("c92db314-765b-46dd-bf40-ef7d5a5abd7b") }
                });

            migrationBuilder.InsertData(
                table: "user_has_relation_to_role",
                columns: new[] { "uuid", "created_by_uuid", "created_time", "deleted_by_uuid", "deleted_time", "modified_by_uuid", "modified_time", "role_id", "user_id" },
                values: new object[,]
                {
                    { new Guid("00f04640-afb0-448a-a1b3-46a22fefcc4d"), new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 3, 0, 32, 51, 873, DateTimeKind.Local).AddTicks(3702), null, null, null, null, new Guid("d411a531-fbea-11ed-8f81-7085c294413b"), new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b") },
                    { new Guid("09468eb0-96b5-4c05-8cb0-64552952f1a3"), new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 3, 0, 32, 51, 873, DateTimeKind.Local).AddTicks(3738), null, null, null, null, new Guid("776848df-084f-11ee-b2c8-7085c294413b"), new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b") },
                    { new Guid("87eefb3e-3fd8-45e5-94cb-88152bd2611f"), new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 3, 0, 32, 51, 873, DateTimeKind.Local).AddTicks(3762), null, null, null, null, new Guid("8937df9c-fbef-11ed-8f81-7085c294413b"), new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b") },
                    { new Guid("1fc161a1-758b-484f-9d40-b0d3fec4e5f7"), new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 3, 0, 32, 51, 874, DateTimeKind.Local).AddTicks(8750), null, null, null, null, new Guid("8937df9c-fbef-11ed-8f81-7085c294413b"), new Guid("c3f257ff-1c85-4e3c-b6b2-21ef2afba51f") },
                    { new Guid("7fc1a0ad-cbc7-4cea-9cfb-7d3b74828f14"), new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 3, 0, 32, 51, 875, DateTimeKind.Local).AddTicks(323), null, null, null, null, new Guid("8937df9c-fbef-11ed-8f81-7085c294413b"), new Guid("c3f257ff-1c85-4e3c-b6b2-21ef5afba61f") },
                    { new Guid("9aeda538-4603-410c-a744-a64d37071d1f"), new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 3, 0, 32, 51, 874, DateTimeKind.Local).AddTicks(7184), null, null, null, null, new Guid("8937df9c-fbef-11ed-8f81-7085c294413b"), new Guid("c3f257ff-1c85-4e3c-b6b2-21ef8afba31f") },
                    { new Guid("9c1acd43-0b8d-4d6d-af72-4563d3fa2ffb"), new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 3, 0, 32, 51, 874, DateTimeKind.Local).AddTicks(7956), null, null, null, null, new Guid("8937df9c-fbef-11ed-8f81-7085c294413b"), new Guid("c3f257ff-1c85-4e3c-b6b2-21ef7afba41f") },
                    { new Guid("d8d2350f-2a51-4243-a65b-ed267139caca"), new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 3, 0, 32, 51, 874, DateTimeKind.Local).AddTicks(6354), null, null, null, null, new Guid("8937df9c-fbef-11ed-8f81-7085c294413b"), new Guid("c3f257ff-1c85-4e3c-b6b2-21ef9afba21f") },
                    { new Guid("dc89065b-dbb7-4625-ac8d-59170789ffea"), new Guid("c92db313-765b-46dd-bf40-ef7d5a5abd7b"), new DateTime(2024, 1, 3, 0, 32, 51, 874, DateTimeKind.Local).AddTicks(9503), null, null, null, null, new Guid("8937df9c-fbef-11ed-8f81-7085c294413b"), new Guid("c3f257ff-1c85-4e3c-b6b2-21ef3afba61f") }
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

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Infrastructure.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "app_academy_info",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    academy_num = table.Column<string>(type: "text", nullable: false),
                    academy_name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_academy_info", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_building_info",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    building_num = table.Column<string>(type: "text", nullable: false),
                    building_name = table.Column<string>(type: "text", nullable: false),
                    floor = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_building_info", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_class_info",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    specialized_num = table.Column<string>(type: "text", nullable: false),
                    class_name = table.Column<string>(type: "text", nullable: false),
                    class_num = table.Column<string>(type: "text", nullable: false),
                    counselor_id = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_class_info", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_course_info",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    course_name = table.Column<string>(type: "text", nullable: false),
                    textbook_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_public_courses = table.Column<bool>(type: "boolean", nullable: false),
                    specialized_num = table.Column<string>(type: "text", nullable: true),
                    teacher_id = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_course_info", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_curriculum",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    curriculum_data = table.Column<string>(type: "text", nullable: true),
                    specialized_name = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_curriculum", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_dormitory",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    building_num = table.Column<string>(type: "text", nullable: false),
                    dormitory_num = table.Column<int>(type: "integer", nullable: false),
                    student_id = table.Column<string>(type: "text", nullable: false),
                    is_dorm_admin = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_dormitory", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_identity_user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_name = table.Column<string>(type: "text", nullable: false),
                    refresh_token = table.Column<string>(type: "text", nullable: false),
                    refresh_token_expiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_identity_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_intention_info",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    class_id = table.Column<Guid>(type: "uuid", nullable: false),
                    student_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_intention_info", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_questionnaire",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    questionnaire_theme = table.Column<string>(type: "text", nullable: false),
                    questionnaire_title = table.Column<string>(type: "text", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_questionnaire", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_questionnaire_options",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    questionnaire_id = table.Column<Guid>(type: "uuid", nullable: false),
                    questionnaire_question_id = table.Column<Guid>(type: "uuid", nullable: false),
                    option_name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_questionnaire_options", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_questionnaire_record",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    questionnaire_id = table.Column<Guid>(type: "uuid", nullable: false),
                    questionnaire_option_type = table.Column<string>(type: "text", nullable: false),
                    questionnaire_question = table.Column<string>(type: "text", nullable: false),
                    option_describe = table.Column<string>(type: "text", nullable: true),
                    questionnaire_flag = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_questionnaire_record", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_questionnaire_res",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    questionnaire_id = table.Column<Guid>(type: "uuid", nullable: false),
                    questionnaire_question_id = table.Column<Guid>(type: "uuid", nullable: false),
                    option_id = table.Column<Guid>(type: "uuid", nullable: false),
                    option_value = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_questionnaire_res", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_role",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_score_info",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    student_id = table.Column<Guid>(type: "uuid", nullable: false),
                    subject_id = table.Column<Guid>(type: "uuid", nullable: false),
                    score = table.Column<string>(type: "text", nullable: true),
                    semester_id = table.Column<Guid>(type: "uuid", nullable: false),
                    class_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_score_info", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_specialized_info",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    academy_num = table.Column<string>(type: "text", nullable: false),
                    specialized_name = table.Column<string>(type: "text", nullable: false),
                    specialized_num = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_specialized_info", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_student_info",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    student_id = table.Column<string>(type: "text", nullable: true),
                    student_name = table.Column<string>(type: "text", nullable: false),
                    sex = table.Column<string>(type: "text", nullable: false),
                    id_number = table.Column<string>(type: "text", nullable: false),
                    class_id = table.Column<Guid>(type: "uuid", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    we_chat = table.Column<string>(type: "text", nullable: true),
                    qq_number = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    father_name = table.Column<string>(type: "text", nullable: true),
                    mother_name = table.Column<string>(type: "text", nullable: true),
                    contact_details = table.Column<string>(type: "text", nullable: true),
                    enrollment_time = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_student_info", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_teacher_info",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    work_number = table.Column<string>(type: "text", nullable: true),
                    teacher_name = table.Column<string>(type: "text", nullable: false),
                    sex = table.Column<string>(type: "text", nullable: false),
                    id_number = table.Column<string>(type: "text", nullable: false),
                    academy_num = table.Column<string>(type: "text", nullable: true),
                    we_chat = table.Column<string>(type: "text", nullable: true),
                    qq_number = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_teacher_info", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_term_info",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TermName = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActived = table.Column<bool>(type: "boolean", nullable: false),
                    Remarks = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_term_info", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "app_textbook_info",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    text_bookname = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<string>(type: "text", nullable: true),
                    press = table.Column<string>(type: "text", nullable: false),
                    contact_number = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_textbook_info", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_uploadfile_info",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    origin_filename = table.Column<string>(type: "text", nullable: false),
                    current_filename = table.Column<string>(type: "text", nullable: false),
                    relative_path = table.Column<string>(type: "text", nullable: false),
                    file_type = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_uploadfile_info", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_name = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_avatar_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true),
                    OpenId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "audit_log",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    method_name = table.Column<string>(type: "text", nullable: true),
                    browser_info = table.Column<string>(type: "text", nullable: true),
                    client_name = table.Column<string>(type: "text", nullable: true),
                    client_ip_address = table.Column<string>(type: "text", nullable: true),
                    execution_duration = table.Column<int>(type: "integer", nullable: false),
                    execution_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    return_value = table.Column<string>(type: "text", nullable: true),
                    exception = table.Column<string>(type: "text", nullable: true),
                    service_name = table.Column<string>(type: "text", nullable: true),
                    user_info = table.Column<string>(type: "text", nullable: true),
                    custom_data = table.Column<string>(type: "text", nullable: true),
                    parameters = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_audit_log", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "exception_log",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    short_message = table.Column<string>(type: "text", nullable: false),
                    full_message = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exception_log", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app_academy_info");

            migrationBuilder.DropTable(
                name: "app_building_info");

            migrationBuilder.DropTable(
                name: "app_class_info");

            migrationBuilder.DropTable(
                name: "app_course_info");

            migrationBuilder.DropTable(
                name: "app_curriculum");

            migrationBuilder.DropTable(
                name: "app_dormitory");

            migrationBuilder.DropTable(
                name: "app_identity_user");

            migrationBuilder.DropTable(
                name: "app_intention_info");

            migrationBuilder.DropTable(
                name: "app_questionnaire");

            migrationBuilder.DropTable(
                name: "app_questionnaire_options");

            migrationBuilder.DropTable(
                name: "app_questionnaire_record");

            migrationBuilder.DropTable(
                name: "app_questionnaire_res");

            migrationBuilder.DropTable(
                name: "app_role");

            migrationBuilder.DropTable(
                name: "app_score_info");

            migrationBuilder.DropTable(
                name: "app_specialized_info");

            migrationBuilder.DropTable(
                name: "app_student_info");

            migrationBuilder.DropTable(
                name: "app_teacher_info");

            migrationBuilder.DropTable(
                name: "app_term_info");

            migrationBuilder.DropTable(
                name: "app_textbook_info");

            migrationBuilder.DropTable(
                name: "app_uploadfile_info");

            migrationBuilder.DropTable(
                name: "app_user");

            migrationBuilder.DropTable(
                name: "audit_log");

            migrationBuilder.DropTable(
                name: "exception_log");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    department_id = table.Column<int>(type: "integer", nullable: false, comment: "Уникальный идентификатор кафедры (AI)")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    department_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "Название кафедры")
                },
                constraints: table =>
                {
                    table.PrimaryKey("departments_pkey", x => x.department_id);
                },
                comment: "Таблица кафедр колледжа");

            migrationBuilder.CreateTable(
                name: "groups",
                columns: table => new
                {
                    group_id = table.Column<int>(type: "integer", nullable: false, comment: "Уникальный идентификатор группы (AI)")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    group_name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, comment: "Название группы (например, ИСП-421)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("groups_pkey", x => x.group_id);
                },
                comment: "Таблица учебных групп колледжа");

            migrationBuilder.CreateTable(
                name: "teachers",
                columns: table => new
                {
                    teacher_id = table.Column<int>(type: "integer", nullable: false, comment: "Уникальный идентификатор преподавателя (AI)")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    department_id = table.Column<int>(type: "integer", nullable: false, comment: "Внешний ключ к таблице кафедр")
                },
                constraints: table =>
                {
                    table.PrimaryKey("teachers_pkey", x => x.teacher_id);
                    table.ForeignKey(
                        name: "fk_teachers_department",
                        column: x => x.department_id,
                        principalTable: "departments",
                        principalColumn: "department_id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Таблица преподавателей");

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    student_id = table.Column<int>(type: "integer", nullable: false, comment: "Уникальный идентификатор студента (AI)")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    group_id = table.Column<int>(type: "integer", nullable: false, comment: "Внешний ключ к таблице учебных групп")
                },
                constraints: table =>
                {
                    table.PrimaryKey("students_pkey", x => x.student_id);
                    table.ForeignKey(
                        name: "fk_students_group",
                        column: x => x.group_id,
                        principalTable: "groups",
                        principalColumn: "group_id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Таблица студентов");

            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    course_id = table.Column<int>(type: "integer", nullable: false, comment: "Уникальный идентификатор курса (AI)")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    course_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    teacher_id = table.Column<int>(type: "integer", nullable: false, comment: "Внешний ключ к таблице преподавателей (кто ведет курс)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("courses_pkey", x => x.course_id);
                    table.ForeignKey(
                        name: "fk_courses_teacher",
                        column: x => x.teacher_id,
                        principalTable: "teachers",
                        principalColumn: "teacher_id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Таблица учебных дисциплин (курсов)");

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    project_id = table.Column<int>(type: "integer", nullable: false, comment: "Уникальный идентификатор проекта (AI)")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    project_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    course_id = table.Column<int>(type: "integer", nullable: false, comment: "Внешний ключ к таблице курсов (к какой дисциплине относится проект)"),
                    max_score = table.Column<int>(type: "integer", nullable: false, comment: "Максимально возможный балл за проект"),
                    deadline = table.Column<DateOnly>(type: "date", nullable: false, comment: "Крайний срок сдачи проекта")
                },
                constraints: table =>
                {
                    table.PrimaryKey("projects_pkey", x => x.project_id);
                    table.ForeignKey(
                        name: "fk_projects_course",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "course_id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Таблица проектов, назначаемых по дисциплинам");

            migrationBuilder.CreateTable(
                name: "projectteams",
                columns: table => new
                {
                    team_id = table.Column<int>(type: "integer", nullable: false, comment: "Уникальный идентификатор команды (AI)")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    team_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    project_id = table.Column<int>(type: "integer", nullable: false, comment: "Внешний ключ к таблице проектов (над каким проектом работает команда)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("projectteams_pkey", x => x.team_id);
                    table.ForeignKey(
                        name: "fk_teams_project",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Таблица команд, создаваемых для работы над проектами");

            migrationBuilder.CreateTable(
                name: "tasks",
                columns: table => new
                {
                    task_id = table.Column<int>(type: "integer", nullable: false, comment: "Уникальный идентификатор этапа (AI)")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    task_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    project_id = table.Column<int>(type: "integer", nullable: false, comment: "Внешний ключ к таблице проектов (в рамках какого проекта этап)"),
                    task_deadline = table.Column<DateOnly>(type: "date", nullable: false),
                    task_max_score = table.Column<int>(type: "integer", nullable: false, comment: "Максимальный балл за данный этап")
                },
                constraints: table =>
                {
                    table.PrimaryKey("tasks_pkey", x => x.task_id);
                    table.ForeignKey(
                        name: "fk_tasks_project",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Таблица этапов (задач) в рамках проекта");

            migrationBuilder.CreateTable(
                name: "studentteams",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    student_id = table.Column<int>(type: "integer", nullable: false, comment: "Внешний ключ к таблице студентов"),
                    team_id = table.Column<int>(type: "integer", nullable: false, comment: "Внешний ключ к таблице команд")
                },
                constraints: table =>
                {
                    table.PrimaryKey("studentteams_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_studentteams_student",
                        column: x => x.student_id,
                        principalTable: "students",
                        principalColumn: "student_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_studentteams_team",
                        column: x => x.team_id,
                        principalTable: "projectteams",
                        principalColumn: "team_id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Таблица для связи многие-ко-многим между Студентами и Командами");

            migrationBuilder.CreateTable(
                name: "submissions",
                columns: table => new
                {
                    submission_id = table.Column<int>(type: "integer", nullable: false, comment: "Уникальный идентификатор отправки (AI)")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    team_id = table.Column<int>(type: "integer", nullable: false, comment: "Внешний ключ к таблице команд (какая команда сдала)"),
                    task_id = table.Column<int>(type: "integer", nullable: false, comment: "Внешний ключ к таблице этапов (какой этап сдан)"),
                    submission_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    file_path = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "'Сдано'::character varying", comment: "Текущий статус проверки работы")
                },
                constraints: table =>
                {
                    table.PrimaryKey("submissions_pkey", x => x.submission_id);
                    table.ForeignKey(
                        name: "fk_submissions_task",
                        column: x => x.task_id,
                        principalTable: "tasks",
                        principalColumn: "task_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_submissions_team",
                        column: x => x.team_id,
                        principalTable: "projectteams",
                        principalColumn: "team_id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Таблица сданных студенческих работ (по этапам)");

            migrationBuilder.CreateTable(
                name: "grades",
                columns: table => new
                {
                    grade_id = table.Column<int>(type: "integer", nullable: false, comment: "Уникальный идентификатор оценки (AI)")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    submission_id = table.Column<int>(type: "integer", nullable: false, comment: "Внешний ключ к таблице сданных работ (за какую работу оценка)"),
                    teacher_id = table.Column<int>(type: "integer", nullable: false, comment: "Внешний ключ к таблице преподавателей (кто оценил)"),
                    score = table.Column<int>(type: "integer", nullable: false, comment: "Количество набранных баллов"),
                    feedback = table.Column<string>(type: "text", nullable: true),
                    grading_date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "CURRENT_DATE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("grades_pkey", x => x.grade_id);
                    table.ForeignKey(
                        name: "fk_grades_submission",
                        column: x => x.submission_id,
                        principalTable: "submissions",
                        principalColumn: "submission_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_grades_teacher",
                        column: x => x.teacher_id,
                        principalTable: "teachers",
                        principalColumn: "teacher_id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Таблица оценок, выставленных преподавателями за сданные работы");

            migrationBuilder.CreateIndex(
                name: "idx_courses_teacher_id",
                table: "courses",
                column: "teacher_id");

            migrationBuilder.CreateIndex(
                name: "departments_department_name_key",
                table: "departments",
                column: "department_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "grades_submission_id_key",
                table: "grades",
                column: "submission_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_grades_submission_id",
                table: "grades",
                column: "submission_id");

            migrationBuilder.CreateIndex(
                name: "idx_grades_teacher_id",
                table: "grades",
                column: "teacher_id");

            migrationBuilder.CreateIndex(
                name: "groups_group_name_key",
                table: "groups",
                column: "group_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_projects_course_id",
                table: "projects",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "idx_projectteams_project_id",
                table: "projectteams",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "idx_students_group_id",
                table: "students",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "idx_students_last_name",
                table: "students",
                column: "last_name");

            migrationBuilder.CreateIndex(
                name: "idx_studentteams_student_id",
                table: "studentteams",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "idx_studentteams_team_id",
                table: "studentteams",
                column: "team_id");

            migrationBuilder.CreateIndex(
                name: "unique_student_in_team",
                table: "studentteams",
                columns: new[] { "student_id", "team_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_submissions_task_id",
                table: "submissions",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "idx_submissions_team_id",
                table: "submissions",
                column: "team_id");

            migrationBuilder.CreateIndex(
                name: "idx_tasks_project_id",
                table: "tasks",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "idx_teachers_department_id",
                table: "teachers",
                column: "department_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "grades");

            migrationBuilder.DropTable(
                name: "studentteams");

            migrationBuilder.DropTable(
                name: "submissions");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "tasks");

            migrationBuilder.DropTable(
                name: "projectteams");

            migrationBuilder.DropTable(
                name: "groups");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "courses");

            migrationBuilder.DropTable(
                name: "teachers");

            migrationBuilder.DropTable(
                name: "departments");
        }
    }
}

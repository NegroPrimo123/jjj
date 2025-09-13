using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Models;

public partial class Task2DbContext : DbContext
{
    public Task2DbContext()
    {
    }

    public Task2DbContext(DbContextOptions<Task2DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Projectteam> Projectteams { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Studentteam> Studentteams { get; set; }

    public virtual DbSet<Submission> Submissions { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("courses_pkey");

            entity.ToTable("courses", tb => tb.HasComment("Таблица учебных дисциплин (курсов)"));

            entity.HasIndex(e => e.TeacherId, "idx_courses_teacher_id");

            entity.Property(e => e.CourseId)
                .HasComment("Уникальный идентификатор курса (AI)")
                .HasColumnName("course_id");
            entity.Property(e => e.CourseName)
                .HasMaxLength(100)
                .HasColumnName("course_name");
            entity.Property(e => e.TeacherId)
                .HasComment("Внешний ключ к таблице преподавателей (кто ведет курс)")
                .HasColumnName("teacher_id");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Courses)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_courses_teacher");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("departments_pkey");

            entity.ToTable("departments", tb => tb.HasComment("Таблица кафедр колледжа"));

            entity.HasIndex(e => e.DepartmentName, "departments_department_name_key").IsUnique();

            entity.Property(e => e.DepartmentId)
                .HasComment("Уникальный идентификатор кафедры (AI)")
                .HasColumnName("department_id");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(100)
                .HasComment("Название кафедры")
                .HasColumnName("department_name");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.GradeId).HasName("grades_pkey");

            entity.ToTable("grades", tb => tb.HasComment("Таблица оценок, выставленных преподавателями за сданные работы"));

            entity.HasIndex(e => e.SubmissionId, "grades_submission_id_key").IsUnique();

            entity.HasIndex(e => e.SubmissionId, "idx_grades_submission_id");

            entity.HasIndex(e => e.TeacherId, "idx_grades_teacher_id");

            entity.Property(e => e.GradeId)
                .HasComment("Уникальный идентификатор оценки (AI)")
                .HasColumnName("grade_id");
            entity.Property(e => e.Feedback).HasColumnName("feedback");
            entity.Property(e => e.GradingDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("grading_date");
            entity.Property(e => e.Score)
                .HasComment("Количество набранных баллов")
                .HasColumnName("score");
            entity.Property(e => e.SubmissionId)
                .HasComment("Внешний ключ к таблице сданных работ (за какую работу оценка)")
                .HasColumnName("submission_id");
            entity.Property(e => e.TeacherId)
                .HasComment("Внешний ключ к таблице преподавателей (кто оценил)")
                .HasColumnName("teacher_id");

            entity.HasOne(d => d.Submission).WithOne(p => p.Grade)
                .HasForeignKey<Grade>(d => d.SubmissionId)
                .HasConstraintName("fk_grades_submission");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Grades)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_grades_teacher");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("groups_pkey");

            entity.ToTable("groups", tb => tb.HasComment("Таблица учебных групп колледжа"));

            entity.HasIndex(e => e.GroupName, "groups_group_name_key").IsUnique();

            entity.Property(e => e.GroupId)
                .HasComment("Уникальный идентификатор группы (AI)")
                .HasColumnName("group_id");
            entity.Property(e => e.GroupName)
                .HasMaxLength(20)
                .HasComment("Название группы (например, ИСП-421)")
                .HasColumnName("group_name");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("projects_pkey");

            entity.ToTable("projects", tb => tb.HasComment("Таблица проектов, назначаемых по дисциплинам"));

            entity.HasIndex(e => e.CourseId, "idx_projects_course_id");

            entity.Property(e => e.ProjectId)
                .HasComment("Уникальный идентификатор проекта (AI)")
                .HasColumnName("project_id");
            entity.Property(e => e.CourseId)
                .HasComment("Внешний ключ к таблице курсов (к какой дисциплине относится проект)")
                .HasColumnName("course_id");
            entity.Property(e => e.Deadline)
                .HasComment("Крайний срок сдачи проекта")
                .HasColumnName("deadline");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.MaxScore)
                .HasComment("Максимально возможный балл за проект")
                .HasColumnName("max_score");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(100)
                .HasColumnName("project_name");

            entity.HasOne(d => d.Course).WithMany(p => p.Projects)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("fk_projects_course");
        });

        modelBuilder.Entity<Projectteam>(entity =>
        {
            entity.HasKey(e => e.TeamId).HasName("projectteams_pkey");

            entity.ToTable("projectteams", tb => tb.HasComment("Таблица команд, создаваемых для работы над проектами"));

            entity.HasIndex(e => e.ProjectId, "idx_projectteams_project_id");

            entity.Property(e => e.TeamId)
                .HasComment("Уникальный идентификатор команды (AI)")
                .HasColumnName("team_id");
            entity.Property(e => e.ProjectId)
                .HasComment("Внешний ключ к таблице проектов (над каким проектом работает команда)")
                .HasColumnName("project_id");
            entity.Property(e => e.TeamName)
                .HasMaxLength(50)
                .HasColumnName("team_name");

            entity.HasOne(d => d.Project).WithMany(p => p.Projectteams)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("fk_teams_project");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("students_pkey");

            entity.ToTable("students", tb => tb.HasComment("Таблица студентов"));

            entity.HasIndex(e => e.GroupId, "idx_students_group_id");

            entity.HasIndex(e => e.LastName, "idx_students_last_name");

            entity.Property(e => e.StudentId)
                .HasComment("Уникальный идентификатор студента (AI)")
                .HasColumnName("student_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.GroupId)
                .HasComment("Внешний ключ к таблице учебных групп")
                .HasColumnName("group_id");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");

            entity.HasOne(d => d.Group).WithMany(p => p.Students)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_students_group");
        });

        modelBuilder.Entity<Studentteam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("studentteams_pkey");

            entity.ToTable("studentteams", tb => tb.HasComment("Таблица для связи многие-ко-многим между Студентами и Командами"));

            entity.HasIndex(e => e.StudentId, "idx_studentteams_student_id");

            entity.HasIndex(e => e.TeamId, "idx_studentteams_team_id");

            entity.HasIndex(e => new { e.StudentId, e.TeamId }, "unique_student_in_team").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.StudentId)
                .HasComment("Внешний ключ к таблице студентов")
                .HasColumnName("student_id");
            entity.Property(e => e.TeamId)
                .HasComment("Внешний ключ к таблице команд")
                .HasColumnName("team_id");

            entity.HasOne(d => d.Student).WithMany(p => p.Studentteams)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("fk_studentteams_student");

            entity.HasOne(d => d.Team).WithMany(p => p.Studentteams)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("fk_studentteams_team");
        });

        modelBuilder.Entity<Submission>(entity =>
        {
            entity.HasKey(e => e.SubmissionId).HasName("submissions_pkey");

            entity.ToTable("submissions", tb => tb.HasComment("Таблица сданных студенческих работ (по этапам)"));

            entity.HasIndex(e => e.TaskId, "idx_submissions_task_id");

            entity.HasIndex(e => e.TeamId, "idx_submissions_team_id");

            entity.Property(e => e.SubmissionId)
                .HasComment("Уникальный идентификатор отправки (AI)")
                .HasColumnName("submission_id");
            entity.Property(e => e.FilePath)
                .HasMaxLength(255)
                .HasColumnName("file_path");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Сдано'::character varying")
                .HasComment("Текущий статус проверки работы")
                .HasColumnName("status");
            entity.Property(e => e.SubmissionDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("submission_date");
            entity.Property(e => e.TaskId)
                .HasComment("Внешний ключ к таблице этапов (какой этап сдан)")
                .HasColumnName("task_id");
            entity.Property(e => e.TeamId)
                .HasComment("Внешний ключ к таблице команд (какая команда сдала)")
                .HasColumnName("team_id");

            entity.HasOne(d => d.Task).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("fk_submissions_task");

            entity.HasOne(d => d.Team).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("fk_submissions_team");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("tasks_pkey");

            entity.ToTable("tasks", tb => tb.HasComment("Таблица этапов (задач) в рамках проекта"));

            entity.HasIndex(e => e.ProjectId, "idx_tasks_project_id");

            entity.Property(e => e.TaskId)
                .HasComment("Уникальный идентификатор этапа (AI)")
                .HasColumnName("task_id");
            entity.Property(e => e.ProjectId)
                .HasComment("Внешний ключ к таблице проектов (в рамках какого проекта этап)")
                .HasColumnName("project_id");
            entity.Property(e => e.TaskDeadline).HasColumnName("task_deadline");
            entity.Property(e => e.TaskMaxScore)
                .HasComment("Максимальный балл за данный этап")
                .HasColumnName("task_max_score");
            entity.Property(e => e.TaskName)
                .HasMaxLength(100)
                .HasColumnName("task_name");

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("fk_tasks_project");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId).HasName("teachers_pkey");

            entity.ToTable("teachers", tb => tb.HasComment("Таблица преподавателей"));

            entity.HasIndex(e => e.DepartmentId, "idx_teachers_department_id");

            entity.Property(e => e.TeacherId)
                .HasComment("Уникальный идентификатор преподавателя (AI)")
                .HasColumnName("teacher_id");
            entity.Property(e => e.DepartmentId)
                .HasComment("Внешний ключ к таблице кафедр")
                .HasColumnName("department_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");

            entity.HasOne(d => d.Department).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_teachers_department");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

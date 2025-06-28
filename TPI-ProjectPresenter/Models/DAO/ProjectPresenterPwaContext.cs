using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TPI_ProjectPresenter.Models.DAO;

public partial class ProjectPresenterPwaContext : DbContext
{
    public ProjectPresenterPwaContext()
    {
    }

    public ProjectPresenterPwaContext(DbContextOptions<ProjectPresenterPwaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ComparisonItemInfo> ComparisonItemInfos { get; set; }

    public virtual DbSet<ContentBudgetList> ContentBudgetLists { get; set; }

    public virtual DbSet<ContentCarrousel> ContentCarrousels { get; set; }

    public virtual DbSet<ContentItem> ContentItems { get; set; }

    public virtual DbSet<ContentSection> ContentSections { get; set; }

    public virtual DbSet<ContentSingleComparison> ContentSingleComparisons { get; set; }

    public virtual DbSet<ContentSingleImage> ContentSingleImages { get; set; }

    public virtual DbSet<ContentStaffList> ContentStaffLists { get; set; }

    public virtual DbSet<ContentTechList> ContentTechLists { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectTab> ProjectTabs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ComparisonItemInfo>(entity =>
        {
            entity.HasKey(e => new { e.Pid, e.Tid, e.Sid, e.Iid, e.Lr, e.OrderNo });

            entity.ToTable("ComparisonItemInfo");

            entity.Property(e => e.Pid).HasColumnName("PID");
            entity.Property(e => e.Tid).HasColumnName("TID");
            entity.Property(e => e.Sid).HasColumnName("SID");
            entity.Property(e => e.Iid).HasColumnName("IID");
            entity.Property(e => e.Lr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("LR");
            entity.Property(e => e.Info)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.ContentSingleComparison).WithMany(p => p.ComparisonItemInfos)
                .HasForeignKey(d => new { d.Pid, d.Tid, d.Sid, d.Iid, d.Lr })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ComparisonItemInfo_ContentSingleComparison");
        });

        modelBuilder.Entity<ContentBudgetList>(entity =>
        {
            entity.HasKey(e => new { e.Pid, e.Tid, e.Sid, e.Iid, e.OrderNo });

            entity.ToTable("ContentBudgetList");

            entity.Property(e => e.Pid).HasColumnName("PID");
            entity.Property(e => e.Tid).HasColumnName("TID");
            entity.Property(e => e.Sid).HasColumnName("SID");
            entity.Property(e => e.Iid).HasColumnName("IID");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Item)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Value).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.ContentItem).WithMany(p => p.ContentBudgetLists)
                .HasForeignKey(d => new { d.Pid, d.Tid, d.Sid, d.Iid })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContentBudgetList_ContentItem");
        });

        modelBuilder.Entity<ContentCarrousel>(entity =>
        {
            entity.HasKey(e => new { e.Pid, e.Tid, e.Sid, e.Iid, e.OrderNo });

            entity.ToTable("ContentCarrousel");

            entity.Property(e => e.Pid).HasColumnName("PID");
            entity.Property(e => e.Tid).HasColumnName("TID");
            entity.Property(e => e.Sid).HasColumnName("SID");
            entity.Property(e => e.Iid).HasColumnName("IID");
            entity.Property(e => e.ImageRef)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ImageText)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.ContentItem).WithMany(p => p.ContentCarrousels)
                .HasForeignKey(d => new { d.Pid, d.Tid, d.Sid, d.Iid })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContentCarrousel_ContentItem");
        });

        modelBuilder.Entity<ContentItem>(entity =>
        {
            entity.HasKey(e => new { e.Pid, e.Tid, e.Sid, e.Iid });

            entity.ToTable("ContentItem");

            entity.Property(e => e.Pid).HasColumnName("PID");
            entity.Property(e => e.Tid).HasColumnName("TID");
            entity.Property(e => e.Sid).HasColumnName("SID");
            entity.Property(e => e.Iid).HasColumnName("IID");
            entity.Property(e => e.Text)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.ContentSection).WithMany(p => p.ContentItems)
                .HasForeignKey(d => new { d.Pid, d.Tid, d.Sid })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContentItem_ContentSection");
        });

        modelBuilder.Entity<ContentSection>(entity =>
        {
            entity.HasKey(e => new { e.Pid, e.Tid, e.Sid });

            entity.ToTable("ContentSection");

            entity.Property(e => e.Pid).HasColumnName("PID");
            entity.Property(e => e.Tid).HasColumnName("TID");
            entity.Property(e => e.Sid).HasColumnName("SID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Tooltip)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.ProjectTab).WithMany(p => p.ContentSections)
                .HasForeignKey(d => new { d.Pid, d.Tid })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContentSection_ProjectTab");
        });

        modelBuilder.Entity<ContentSingleComparison>(entity =>
        {
            entity.HasKey(e => new { e.Pid, e.Tid, e.Sid, e.Iid, e.Lr });

            entity.ToTable("ContentSingleComparison");

            entity.Property(e => e.Pid).HasColumnName("PID");
            entity.Property(e => e.Tid).HasColumnName("TID");
            entity.Property(e => e.Sid).HasColumnName("SID");
            entity.Property(e => e.Iid).HasColumnName("IID");
            entity.Property(e => e.Lr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("LR");
            entity.Property(e => e.Detail)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.ContentItem).WithMany(p => p.ContentSingleComparisons)
                .HasForeignKey(d => new { d.Pid, d.Tid, d.Sid, d.Iid })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContentSingleComparison_ContentItem");
        });

        modelBuilder.Entity<ContentSingleImage>(entity =>
        {
            entity.HasKey(e => new { e.Pid, e.Tid, e.Sid, e.Iid, e.ImageRef });

            entity.ToTable("ContentSingleImage");

            entity.Property(e => e.Pid).HasColumnName("PID");
            entity.Property(e => e.Tid).HasColumnName("TID");
            entity.Property(e => e.Sid).HasColumnName("SID");
            entity.Property(e => e.Iid).HasColumnName("IID");
            entity.Property(e => e.ImageRef)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.ContentItem).WithMany(p => p.ContentSingleImages)
                .HasForeignKey(d => new { d.Pid, d.Tid, d.Sid, d.Iid })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContentSingleImage_ContentItem");
        });

        modelBuilder.Entity<ContentStaffList>(entity =>
        {
            entity.HasKey(e => new { e.Pid, e.Tid, e.Sid, e.Iid, e.OrderNo });

            entity.ToTable("ContentStaffList");

            entity.Property(e => e.Pid).HasColumnName("PID");
            entity.Property(e => e.Tid).HasColumnName("TID");
            entity.Property(e => e.Sid).HasColumnName("SID");
            entity.Property(e => e.Iid).HasColumnName("IID");
            entity.Property(e => e.ImageRef)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.ContentItem).WithMany(p => p.ContentStaffLists)
                .HasForeignKey(d => new { d.Pid, d.Tid, d.Sid, d.Iid })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContentStaffList_ContentItem");
        });

        modelBuilder.Entity<ContentTechList>(entity =>
        {
            entity.HasKey(e => new { e.Pid, e.Tid, e.Sid, e.Iid, e.OrderNo });

            entity.ToTable("ContentTechList");

            entity.Property(e => e.Pid).HasColumnName("PID");
            entity.Property(e => e.Tid).HasColumnName("TID");
            entity.Property(e => e.Sid).HasColumnName("SID");
            entity.Property(e => e.Iid).HasColumnName("IID");
            entity.Property(e => e.IconRef)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TechName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TechUses)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.ContentItem).WithMany(p => p.ContentTechLists)
                .HasForeignKey(d => new { d.Pid, d.Tid, d.Sid, d.Iid })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContentTechList_ContentItem");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Pid);

            entity.ToTable("Project");

            entity.Property(e => e.Pid)
                .ValueGeneratedNever()
                .HasColumnName("PID");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ImgRef)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Tooltip)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProjectTab>(entity =>
        {
            entity.HasKey(e => new { e.Pid, e.Tid });

            entity.ToTable("ProjectTab");

            entity.Property(e => e.Pid).HasColumnName("PID");
            entity.Property(e => e.Tid).HasColumnName("TID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.PidNavigation).WithMany(p => p.ProjectTabs)
                .HasForeignKey(d => d.Pid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProjectTab_Project");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

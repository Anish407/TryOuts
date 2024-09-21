using System;
using System.Collections.Generic;
using EFCoreQuerying.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreQuerying.Context;

public partial class MyDbContext : DbContext
{
    // Generated using Rider
    // ef dbcontext scaffold --project EFCoreQuerying\EFCoreQuerying.csproj --startup-project EFCoreQuerying\EFCoreQuerying.csproj
    // --configuration Debug --framework net8.0 "Data Source=LAPTOP-FSA8LOOJ\SQLEXPRESS;Initial Catalog=EfCoreQuerying;Trusted_Connection=true;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer
    // --context MyDbContext --context-dir Context --force --output-dir EF


    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//         => optionsBuilder.UseSqlServer("Data Source=LAPTOP-FSA8LOOJ\\SQLEXPRESS;Initial Catalog=EfCoreQuerying;Trusted_Connection=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Authors__70DAFC3494CF579E");

            entity.Property(e => e.AuthorId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C207441B6313");

            entity.Property(e => e.BookId).ValueGeneratedNever();
            entity.Property(e => e.Genre).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__Books__AuthorId__267ABA7A");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79CEF5C71551");

            entity.Property(e => e.ReviewId).ValueGeneratedNever();
            entity.Property(e => e.ReviewerName).HasMaxLength(100);

            entity.HasOne(d => d.Book).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Reviews__BookId__29572725");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK__Sales__1EE3C3FF3D933248");

            entity.Property(e => e.SaleId).ValueGeneratedNever();

            entity.HasOne(d => d.Book).WithMany(p => p.Sales)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Sales__BookId__2C3393D0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

public class SaleConfigurations : EntityTypeConfigBase, IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> entity)
    {
        entity.ToTable(TableName);
        entity.HasKey(e => e.SaleId).HasName("PK__Sales__1EE3C3FF3D933248");

        entity.Property(e => e.SaleId).ValueGeneratedNever();

        entity.HasOne(d => d.Book).WithMany(p => p.Sales)
            .HasForeignKey(d => d.BookId)
            .HasConstraintName("FK__Sales__BookId__2C3393D0");
    }

    public override string TableName { get; }= nameof(Sale);
}

public abstract class EntityTypeConfigBase{
    public abstract string TableName { get; }
    
};

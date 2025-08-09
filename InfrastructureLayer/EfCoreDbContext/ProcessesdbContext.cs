using InfotecsInternTask.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfotecsInternTask.Infrastructure.EfCoreDbContext;

public partial class ProcessesdbContext : DbContext
{
    public ProcessesdbContext()
    {
    }

    public ProcessesdbContext(DbContextOptions<ProcessesdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<Value> Values { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=processesdb;Username=postgres;Password=123456");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.Resultid).HasName("results_pkey");

            entity.ToTable("results");

            entity.HasIndex(e => e.Filename, "results_filename_key").IsUnique();

            entity.Property(e => e.Resultid).HasColumnName("resultid");
            entity.Property(e => e.Averageexecutiontime).HasColumnName("averageexecutiontime");
            entity.Property(e => e.Averagevalue).HasColumnName("averagevalue");
            entity.Property(e => e.Deltatime).HasColumnName("deltatime");
            entity.Property(e => e.Filename)
                .HasMaxLength(50)
                .HasColumnName("filename");
            entity.Property(e => e.Maxvalue).HasColumnName("maxvalue");
            entity.Property(e => e.Medianvalue).HasColumnName("medianvalue");
            entity.Property(e => e.Mindate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("mindate");
            entity.Property(e => e.Minvalue).HasColumnName("minvalue");
        });

        modelBuilder.Entity<Value>(entity =>
        {
            entity.HasKey(e => e.Valueid).HasName("values_pkey");

            entity.ToTable("values");

            entity.Property(e => e.Valueid).HasColumnName("valueid");
            entity.Property(e => e.Date)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date");
            entity.Property(e => e.Executiontime).HasColumnName("executiontime");
            entity.Property(e => e.Resultid).HasColumnName("resultid");
            entity.Property(e => e.Value1).HasColumnName("value");

            entity.HasOne(d => d.Result).WithMany(p => p.Values)
                .HasForeignKey(d => d.Resultid)
                .HasConstraintName("values_resultid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

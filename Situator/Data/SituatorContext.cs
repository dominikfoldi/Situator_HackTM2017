using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Situator.Model;

namespace Situator.Models
{
    public class SituatorContext : DbContext
    {
        public SituatorContext(DbContextOptions<SituatorContext> options)
            : base(options)
        {
        }

        public DbSet<Node> Nodes { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<NodeRelation> NodeRelations { get; set; }
        public DbSet<Situator.Model.Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<NodeRelation>().HasOne(i => i.Parent).WithMany(u => u.Children).Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Entity<NodeRelation>().HasOne(i => i.Child).WithMany(u => u.Parents).Metadata.DeleteBehavior = DeleteBehavior.Cascade;

            modelBuilder.Entity<NodeRelation>()
             .HasKey(e => new { e.ParentId, e.ChildId });

            modelBuilder.Entity<NodeRelation>()
                .HasOne(e => e.Parent)
                .WithMany(e => e.Children)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<NodeRelation>()
                .HasOne(e => e.Child)
                .WithMany(e => e.Parents)
                .HasForeignKey(e => e.ChildId);
        }
    }
}
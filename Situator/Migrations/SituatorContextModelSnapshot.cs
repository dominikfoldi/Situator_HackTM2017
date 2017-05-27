using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Situator.Models;

namespace Situator.Migrations
{
    [DbContext(typeof(SituatorContext))]
    partial class SituatorContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Situator.Model.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category");

                    b.Property<string>("Description");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Situator.Model.Node", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CourseId");

                    b.Property<bool>("IsLeaf");

                    b.Property<bool>("IsRoot");

                    b.Property<string>("Text");

                    b.Property<string>("VideoUrl");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Nodes");
                });

            modelBuilder.Entity("Situator.Model.NodeRelation", b =>
                {
                    b.Property<int>("ParentId");

                    b.Property<int>("ChildId");

                    b.HasKey("ParentId", "ChildId");

                    b.HasIndex("ChildId");

                    b.ToTable("NodeRelations");
                });

            modelBuilder.Entity("Situator.Model.Score", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("NodeId");

                    b.Property<int>("Point");

                    b.Property<int>("SkillId");

                    b.HasKey("Id");

                    b.HasIndex("NodeId");

                    b.HasIndex("SkillId");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("Situator.Model.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("Situator.Model.Node", b =>
                {
                    b.HasOne("Situator.Model.Course", "Course")
                        .WithMany("Nodes")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Situator.Model.NodeRelation", b =>
                {
                    b.HasOne("Situator.Model.Node", "Child")
                        .WithMany("Parents")
                        .HasForeignKey("ChildId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Situator.Model.Node", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("Situator.Model.Score", b =>
                {
                    b.HasOne("Situator.Model.Node", "Node")
                        .WithMany("Scores")
                        .HasForeignKey("NodeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Situator.Model.Skill", "Skill")
                        .WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
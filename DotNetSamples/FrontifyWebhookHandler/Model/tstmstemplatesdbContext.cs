using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FrontifyWebhookHandler.Model
{
    public partial class tstmstemplatesdbContext : DbContext
    {
        public tstmstemplatesdbContext()
        {
        }

        public tstmstemplatesdbContext(DbContextOptions<tstmstemplatesdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FrontifyWebhookEvent> FrontifyWebhookEvents { get; set; }
        public virtual DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FrontifyWebhookEvent>(entity =>
            {
                entity.Property(e => e.Action)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OccurredAt).HasColumnType("datetime");

                entity.Property(e => e.ProcessedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Todo>(entity =>
            {
                entity.ToTable("Todo");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

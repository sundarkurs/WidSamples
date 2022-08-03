using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DomainEventsUpdate.Model
{
    public partial class tstmsplmwsasdbContext : DbContext
    {
        public tstmsplmwsasdbContext()
        {
        }

        public tstmsplmwsasdbContext(DbContextOptions<tstmsplmwsasdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AgileUploadRequest> AgileUploadRequests { get; set; }
        public virtual DbSet<AgileUploadStatus> AgileUploadStatuses { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<BrandParent> BrandParents { get; set; }
        public virtual DbSet<BrandType> BrandTypes { get; set; }
        public virtual DbSet<Deltum> Delta { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<DomainEvent> DomainEvents { get; set; }
        public virtual DbSet<Family> Families { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductEndItem> ProductEndItems { get; set; }
        public virtual DbSet<ProductSparePart> ProductSpareParts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgileUploadRequest>(entity =>
            {
                entity.ToTable("AgileUploadRequest");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserName).HasMaxLength(100);
            });

            modelBuilder.Entity<AgileUploadStatus>(entity =>
            {
                entity.ToTable("AgileUploadStatus");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brand");

                entity.HasIndex(e => new { e.ExternalProjectId, e.Name }, "UC_Brand")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.ExternalProjectId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Logo).HasMaxLength(256);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.PermanentKey)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<BrandParent>(entity =>
            {
                entity.HasIndex(e => new { e.BrandId, e.ParentBrandId }, "UC_Related_Brands")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.BrandId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ParentBrandId)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<BrandType>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("BrandType");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Deltum>(entity =>
            {
                entity.HasIndex(e => new { e.Executed, e.BrandName }, "IDX_Delta_Executed_BrandName");

                entity.Property(e => e.BrandName).HasMaxLength(50);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Executed).HasColumnType("datetime");

                entity.Property(e => e.Payload).IsRequired();
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("Document");

                entity.HasIndex(e => e.PermanentKey, "IDX_Document_PermanantKey");

                entity.HasIndex(e => new { e.ExternalProjectId, e.ParentId }, "UC_Document")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.DocumentId).HasMaxLength(256);

                entity.Property(e => e.DocumentInternalId)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ExternalProjectId)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.FilePositionNo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Language)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.PermanentKey).HasMaxLength(255);

                entity.Property(e => e.Revision)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SheetNo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TypeId)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Version)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Document__Product");
            });

            modelBuilder.Entity<DomainEvent>(entity =>
            {
                entity.ToTable("DomainEvent");

                entity.HasIndex(e => e.ItemId, "IDX_DomainEvent_ItemId");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BrandId)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.EntityVersion).HasMaxLength(50);

                entity.Property(e => e.PermanentKey)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TimestampUtc).HasColumnType("datetime");
            });

            modelBuilder.Entity<Family>(entity =>
            {
                entity.ToTable("Family");

                entity.HasIndex(e => e.PermanentKey, "IDX_Family_PermanentKey");

                entity.HasIndex(e => new { e.ExternalProjectId, e.ParentId }, "UC_Family")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.BrandLabelId).HasMaxLength(50);

                entity.Property(e => e.BrandLableName).HasMaxLength(150);

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.ExternalProjectId)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.PermanentKey).HasMaxLength(64);

                entity.Property(e => e.PrivateLabel).HasMaxLength(50);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.Families)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Family__Brand");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.HasIndex(e => e.PermanentKey, "IDX_Product_PermanentKey");

                entity.HasIndex(e => new { e.ExternalProjectId, e.ParentId }, "UC_Product")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Category).HasMaxLength(150);

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.ExternalProjectId)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.LaunchDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.PermanentKey).HasMaxLength(64);

                entity.Property(e => e.Platform).HasMaxLength(150);

                entity.Property(e => e.Pricepoint).HasMaxLength(150);

                entity.Property(e => e.ProductFamilyId)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Segment).HasMaxLength(150);

                entity.Property(e => e.Status).HasMaxLength(150);

                entity.Property(e => e.Subsegment).HasMaxLength(150);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Family");
            });

            modelBuilder.Entity<ProductEndItem>(entity =>
            {
                entity.ToTable("ProductEndItem");

                entity.HasIndex(e => e.PermanentKey, "IDX_ProductEndItem_PermanantKey");

                entity.HasIndex(e => new { e.ParentId, e.ExternalProjectId }, "UK_ProductEndItem")
                    .IsUnique();

                entity.HasIndex(e => new { e.Skunumber, e.ParentId, e.Version, e.Revision }, "Uk_ProductEndItem_SkuVerRev")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Alnumber)
                    .HasMaxLength(100)
                    .HasColumnName("ALNumber");

                entity.Property(e => e.BatterySize).HasMaxLength(100);

                entity.Property(e => e.Class).HasMaxLength(50);

                entity.Property(e => e.Color).HasMaxLength(50);

                entity.Property(e => e.ColorCode).HasMaxLength(50);

                entity.Property(e => e.ColorHexCode).HasMaxLength(50);

                entity.Property(e => e.Eccn)
                    .HasMaxLength(100)
                    .HasColumnName("ECCN");

                entity.Property(e => e.Erpname)
                    .HasMaxLength(256)
                    .HasColumnName("ERPName");

                entity.Property(e => e.ExternalCode).HasMaxLength(200);

                entity.Property(e => e.ExternalProjectId)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ImageName).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PermanentKey).HasMaxLength(64);

                entity.Property(e => e.PrivateLabel).HasMaxLength(50);

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProductType).HasMaxLength(50);

                entity.Property(e => e.Revision)
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.Property(e => e.Rmstatus)
                    .HasMaxLength(10)
                    .HasColumnName("RMStatus");

                entity.Property(e => e.Skunumber)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("SKUNumber");

                entity.Property(e => e.State).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(100);

                entity.Property(e => e.Version).HasMaxLength(50);

                entity.Property(e => e.Weight).HasMaxLength(50);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.ProductEndItems)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductEndItem_Product");
            });

            modelBuilder.Entity<ProductSparePart>(entity =>
            {
                entity.ToTable("ProductSparePart");

                entity.HasIndex(e => e.PermanentKey, "IDX_ProductSparePart_PermanantKey");

                entity.HasIndex(e => new { e.ExternalProjectId, e.ParentId }, "UK_ProductSparePart")
                    .IsUnique();

                entity.HasIndex(e => new { e.Skunumber, e.ParentId, e.Version, e.Revision }, "Uk_ProductSparePart_SkuVerRev")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Alnumber)
                    .HasMaxLength(100)
                    .HasColumnName("ALNumber");

                entity.Property(e => e.BatterySize).HasMaxLength(100);

                entity.Property(e => e.Class).HasMaxLength(50);

                entity.Property(e => e.Color).HasMaxLength(50);

                entity.Property(e => e.ColorCode).HasMaxLength(50);

                entity.Property(e => e.ColorHexCode).HasMaxLength(50);

                entity.Property(e => e.Eccn)
                    .HasMaxLength(100)
                    .HasColumnName("ECCN");

                entity.Property(e => e.Erpname)
                    .HasMaxLength(256)
                    .HasColumnName("ERPName");

                entity.Property(e => e.ExternalCode).HasMaxLength(200);

                entity.Property(e => e.ExternalProjectId)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ImageName).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.PermanentKey).HasMaxLength(64);

                entity.Property(e => e.PrivateLabel).HasMaxLength(50);

                entity.Property(e => e.ProductId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProductType).HasMaxLength(50);

                entity.Property(e => e.Revision)
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.Property(e => e.Rmstatus)
                    .HasMaxLength(50)
                    .HasColumnName("RMStatus");

                entity.Property(e => e.Skunumber)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("SKUNumber");

                entity.Property(e => e.State).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(100);

                entity.Property(e => e.Version).HasMaxLength(50);

                entity.Property(e => e.Weight).HasMaxLength(50);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.ProductSpareParts)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductSparePart_Product");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

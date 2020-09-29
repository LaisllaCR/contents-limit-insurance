using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ContentsLimitInsurance.App.Models
{
    public partial class dbContext : DbContext
    {
        public dbContext()
        {
        }

        public dbContext(DbContextOptions<dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<HighValueItem> HighValueItem { get; set; }
        public virtual DbSet<ItemCategory> ItemCategory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var appSettings = "appsettings.json";
#if DEBUG
                appSettings = "appsettings.Development.json";
#endif
                IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(appSettings).Build();
                string connectionString = configuration.GetConnectionString("sqlserver");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HighValueItem>(entity =>
            {
                entity.ToTable("high_value_item");

                entity.Property(e => e.HighValueItemId).HasColumnName("high_value_item_id");

                entity.Property(e => e.HighValueItemKey)
                    .HasColumnName("high_value_item_key")
                    .HasMaxLength(255);

                entity.Property(e => e.ItemCategoryId).HasColumnName("item_category_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(150);

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.ItemCategory)
                    .WithMany(p => p.HighValueItem)
                    .HasForeignKey(d => d.ItemCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("item_category_id");
            });

            modelBuilder.Entity<ItemCategory>(entity =>
            {
                entity.ToTable("item_category");

                entity.Property(e => e.ItemCategoryId).HasColumnName("item_category_id");

                entity.Property(e => e.ItemCategoryKey)
                    .HasColumnName("item_category_key")
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(150);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

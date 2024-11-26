using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Currency_Converter;

public partial class CurrencyMasterDataContext : DbContext
{
    public CurrencyMasterDataContext()
    {
    }

    public CurrencyMasterDataContext(DbContextOptions<CurrencyMasterDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CurrencyMaster> CurrencyMasters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Computer-Science\\WPF\\Currency Converter\\Database\\Currency Converter.mdf;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CurrencyMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Currency__3214EC07BABAC767");

            entity.ToTable("Currency-Master");

            entity.Property(e => e.CurrencyName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Currency_Converter;

public partial class CurrencyMasterDataContext : DbContext
{
    public CurrencyMasterDataContext()
    {
        Database.EnsureCreated();
    }

    public CurrencyMasterDataContext(DbContextOptions<CurrencyMasterDataContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public virtual DbSet<CurrencyMaster> CurrencyMasters { get; set; }

    #if DEBUG
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Computer-Science\\WPF\\Currency Converter\\Database\\Currency Converter.mdf;Integrated Security=True");
    #else
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        string path = Environment.ExpandEnvironmentVariables(@"%appdata%\DarkDB\CurrencyConverter.db");
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        optionsBuilder.UseSqlite("Data Source="+path);
    }
    #endif

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

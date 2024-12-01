using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Windows;

namespace Currency_Converter;

public partial class CurrencyMasterDataContext : DbContext
{


    public virtual DbSet<CurrencyMaster> CurrencyMasters { get; set; }

#if DEBUG

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database", "CurrencyConverter.db");
        Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }

#else
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string path = Environment.ExpandEnvironmentVariables(@"%appdata%\DarkDB\CurrencyConverter.db");

        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        string sourceFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database", "CurrencyConverter.db");

        if (File.Exists(sourceFile) && !File.Exists(path))
        {
            File.Copy(sourceFile, path, true);
        }
        else if (!File.Exists(path))
        {
            MessageBox.Show("Error : Can not create DataBase.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        optionsBuilder.UseSqlite($"Data Source={path}");
    }
#endif

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CurrencyMaster>().ToTable("Currency-Master");
        modelBuilder.Entity<CurrencyMaster>().HasKey(x => x.Id);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}


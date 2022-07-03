using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TRAM;

/// <summary>
/// Класс контекст для базы данных.
/// </summary>
public class TestDbContext : DbContext
{
    /// <summary>
    /// Студенты с ответами в базе данных.
    /// </summary>
    public DbSet<Test> Tests { get; set; }

    /// <summary>
    /// Конструктор, создающий базу данных при первом обращении.
    /// </summary>
    /// <param name="options">Опции конекста.</param>
    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
    {
        base.Database.EnsureCreated();
    }

    /// <summary>
    /// Создание конфигурации базы данных.
    /// </summary>
    /// <param name="optionsBuilder"><see cref="DbContextOptionsBuilder{TContext}"/></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();

        var connectionString = configuration.GetConnectionString("AppDb");
        optionsBuilder.UseSqlServer(connectionString);
    }

    /// <summary>
    /// Добавление моделей в базу данных при создании.
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Test>().Property(t => t.TestAnswers).HasConversion(
            v => JsonConvert.SerializeObject(v),
            v => JsonConvert.DeserializeObject<List<string>>(v));
        
        modelBuilder.Entity<Test>().Property(t => t.TestTasks).HasConversion(
            v => JsonConvert.SerializeObject(v),
            v => JsonConvert.DeserializeObject<List<string>>(v));
        
        modelBuilder.Entity<Test>().Property(t => t.TestThemes).HasConversion(
            v => JsonConvert.SerializeObject(v),
            v => JsonConvert.DeserializeObject<List<string>>(v));
        
        modelBuilder.Entity<Test>().Property(t => t.StudentAnswers).HasConversion(
            v => JsonConvert.SerializeObject(v),
            v => JsonConvert.DeserializeObject<List<string>>(v));
        
        modelBuilder.Entity<Test>().HasKey(t => t.Id);
    }
}
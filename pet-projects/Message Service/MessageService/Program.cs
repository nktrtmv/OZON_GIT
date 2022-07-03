using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MessageService
{
    /// <summary>
    /// Основной класс.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Точка входа.
        /// </summary>
        /// <param name="args">Аргументы.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Метод инициализирующий создание <see cref="MessageService"/>.
        /// </summary>
        /// <param name="args">Аргументы для настройки.</param>
        /// <returns>Обьект <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
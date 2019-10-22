using System;
using System.IO;
using MongoMigrations;
using Newtonsoft.Json.Linq;
using Serilog;
using TopDecks.Api.Core.Mongo;
using TopDecks.MongoDB.Migrations.Migrations;

namespace TopDecks.MongoDB.Migrations
{
    class Program
    {
        private static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .CreateLogger();

            WriteHeader();
            RunMigrations();
        }

        private static void RunMigrations()
        {
            var settings = JObject.Parse(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.json")));

            var connectionString = (string)settings["mongo"];
            var mongoDatabase = MongoDatabaseConfigurator.Configure(connectionString);
            var runner = new MigrationRunner(mongoDatabase);

            try
            {
                runner.MigrationLocator.LookForMigrationsInAssemblyOfType<InitialIndexes>();
                runner.UpdateToLatest();

                WriteSuccessFooter();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to run migrations.");
                WriteFailedFooter();

                throw;
            }
        }

        private static void WriteHeader()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("============================================================================");
            Console.WriteLine("                       Running MongoDB Migrations                           ");
            Console.WriteLine("============================================================================");
            Console.ResetColor();
        }

        private static void WriteSuccessFooter()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"         
                                         __
                                     _.-~  )
                          _..--~~~~,'   ,-/     _
                       .-'. . . .'   ,-','    ,' )
                     ,'. . . _   ,--~,-'__..-'  ,'
                   ,'. . .  (@)' ---~~~~      ,'
                  /. . . . '~~             ,-'
                 /. . . . .             ,-'
                ; . . . .  - .        ,'
               : . . . .       _     /
              . . . . .          `-.:
             . . . ./  - .          )
            .  . . |  _____..---.._/ ____ Success! _
      ~---~~~~----~~~~             ~~
");
            Console.ResetColor();
        }

        private static void WriteFailedFooter()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"
         .
       "":""
     ___: ____    |""\/""|
   , '        `.    \  /
   | O        \___ /  |
 ~^ ~^ ~^ ~^ ~^ ~^ ~^ ~^ ~^ ~^ ~^ ~^ ~

        Migrations Failed
");
            Console.ResetColor();
        }
    }
}

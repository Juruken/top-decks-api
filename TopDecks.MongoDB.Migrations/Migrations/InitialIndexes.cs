using MongoDB.Driver;
using MongoMigrations;
using TopDecks.Api.Core.Extensions;

namespace TopDecks.MongoDB.Migrations.Migrations
{
    public class InitialIndexes : Migration
    {
        public InitialIndexes() : base("1.0.0")
        {
        }

        public override void Update()
        {
            /* Add any indexes you wish to add here e.g. Users
            Database.GetCollection<User>().Indexes.CreateOne(Builders<User>.IndexKeys
                .Ascending(x => x.Email), new CreateIndexOptions { Unique = true });*/
        }
    }
}
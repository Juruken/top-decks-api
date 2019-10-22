using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using Serilog;
using TopDecks.Api.Domain;

namespace TopDecks.Api.Core.Mongo
{
    public static class MongoDatabaseConfigurator
    {
        static MongoDatabaseConfigurator()
        {
            RegisterConventions();
            RegisterClassMaps();
        }

        public static IMongoDatabase Configure(string connectionString)
        {
            var mongoConnectionString = MongoUrl.Create(connectionString);
            var settings = MongoClientSettings.FromUrl(mongoConnectionString);
            settings.ClusterConfigurator = cb =>
            {
                cb.Subscribe<CommandStartedEvent>(e =>
                {
                    if (e.OperationId == null)
                        return;
                    Log.Debug("MongoDB command {commandName}: {command}", e.CommandName, e.Command);
                });
            };

            var client = new MongoClient(settings);
            return client.GetDatabase(mongoConnectionString.DatabaseName);
        }

        private static void RegisterConventions()
        {
            BsonSerializer.RegisterIdGenerator(typeof(string), StringObjectIdGenerator.Instance);
            ConventionRegistry.Register("camel case", new ConventionPack { new CamelCaseElementNameConvention() },
                t => true);
        }

        private static void RegisterClassMaps()
        {
            BsonClassMap.RegisterClassMap<Deck>(cm =>
            {
                cm.AutoMap();
                cm.SetIdMember(cm.GetMemberMap(c => c.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId)));
                cm.GetMemberMap(c => c.Id).SetSerializer(new StringSerializer(BsonType.ObjectId));
            });
            BsonClassMap.RegisterClassMap<PlayerCard>(cm =>
            {
                cm.AutoMap();
            });
        }
    }
}

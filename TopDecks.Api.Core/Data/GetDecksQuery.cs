using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using TopDecks.Api.Domain;
using TopDecks.Api.Core.Extensions;

namespace TopDecks.Api.Core.Data
{
    public class GetDecksQuery : IGetDecksQuery
    {
        private readonly IMongoDatabase _database;

        public GetDecksQuery(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<List<Deck>> Execute(string search)
        {
            var decks = (await _database.GetCollection<Deck>()
                    .FindAsync(d => string.IsNullOrEmpty(search) 
                                    || d.Main.Any(c => c.Name.Contains(search))
                                    || d.Extra.Any(c => c.Name.Contains(search))
                                    || d.Side.Any(c => c.Name.Contains(search))
                                    ))
                .ToList();
            return decks;
        }
    }
}

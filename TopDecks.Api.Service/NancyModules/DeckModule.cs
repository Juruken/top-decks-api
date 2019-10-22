using System.Threading.Tasks;
using Nancy;
using TopDecks.Api.Core.Data;

namespace TopDecks.Api.Service.NancyModules
{
    public class DeckModule : NancyModule
    {
        private readonly IGetDecksQuery _getDecksQuery;
        public DeckModule(IGetDecksQuery getDecksQuery) : base("/decks")
        {
            _getDecksQuery = getDecksQuery;

            Get("/", async _ => await GetDecks());
        }

        private async Task<dynamic> GetDecks()
        {
            var search = Request.Query["search"] != null ? (string) Request.Query["search"] : "";
            var decks = await _getDecksQuery.Execute(search);

            return Negotiate.WithStatusCode(HttpStatusCode.OK).WithModel(decks);
        }
    }
}

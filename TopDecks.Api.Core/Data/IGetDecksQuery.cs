using System.Collections.Generic;
using System.Threading.Tasks;
using TopDecks.Api.Domain;

namespace TopDecks.Api.Core.Data
{
    public interface IGetDecksQuery
    {
        Task<List<Deck>> Execute(string search);
    }
}

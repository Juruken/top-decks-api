using System;

namespace TopDecks.Api.Domain
{
    public class Deck
    {
        public string Id { get; set; }
        public string Author { get; set; }
        public bool TopPlayerCouncil { get; set; }
        public DateTime Created { get; set; }
        public string Skill { get; set; }
        public PlayerCard[] Main { get; set; }
        public PlayerCard[] Side { get; set; }
        public PlayerCard[] Extra { get; set; }
        public string Notes { get; set; }
        public string DeckType { get; set; }
        public string TournamentType { get; set; }
        public string TournamentPlacement { get; set; }
        public string TournamentNumber { get; set; }
        public string DeckUrl { get; set; }
    }
}

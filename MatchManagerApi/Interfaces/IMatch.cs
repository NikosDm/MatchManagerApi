using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatchManagerApi.Entities;

namespace MatchManagerApi.Interface
{
    public interface IMatch
    {
        Task<IEnumerable<Match>> RetrieveAllMatches();
        Task<Match> RetrieveMatch(int matchID);
        Task<bool> AddNewMatch(Match match);
        Task<bool> UpdateMatch(int matchID, Match match);
        Task<bool> DeleteMatch(int matchID);
        Task<IEnumerable<MatchOdds>> RetrieveMatchOdds();
        Task<MatchOdds> RetrieveMatchOdd(int matchOddID);
        Task<IEnumerable<MatchOdds>> RetrieveMatchOddsForMatch(int matchID);
        Task<bool> AddNewMatchOdd(MatchOdds matchOdds);
        Task<bool> UpdateMatchOdd(int matchOddsID, MatchOdds matchOdds);
        Task<bool> DeleteMatchOdd(int matchOddID);
    }
}
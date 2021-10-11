using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatchManagerApi.Data;
using MatchManagerApi.Entities;
using MatchManagerApi.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MatchManagerApi.Services
{
    public class MatchService : IMatch
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;
        public MatchService(DataContext context, ILogger<MatchService> logger) 
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> AddNewMatch(Match match)
        {
            try 
            {
                if ((int)match.Sport != 1 && (int)match.Sport != 2)
                    return false;

                await _context.Matches.AddAsync(match);

                return await SaveAllAsync();
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<bool> AddNewMatchOdd(MatchOdds matchOdds)
        {
            try 
            {
                var match = await RetrieveMatch(matchOdds.MatchId);

                if (match == null) return false;

                await _context.MatchesOdds.AddAsync(matchOdds);
                
                return await SaveAllAsync();
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<bool> DeleteMatch(int matchID)
        {
            try 
            {
                var match = await RetrieveMatch(matchID);

                if (match == null) return false; 

                _context.Matches.Remove(match);

                return await SaveAllAsync();
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<bool> DeleteMatchOdd(int matchOddID)
        {
            try 
            {
                var matchOdd = await RetrieveMatchOdd(matchOddID);

                if (matchOdd == null) return false; 

                _context.MatchesOdds.Remove(matchOdd);

                return await SaveAllAsync();
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<IEnumerable<Match>> RetrieveAllMatches()
        {
            return await _context.Matches.ToListAsync();
        }

        public async Task<Match> RetrieveMatch(int matchID)
        {
            return await _context.Matches.FindAsync(matchID);
        }

        public async Task<MatchOdds> RetrieveMatchOdd(int matchOddID)
        {
            return await _context.MatchesOdds.FindAsync(matchOddID);
        }

        public async Task<IEnumerable<MatchOdds>> RetrieveMatchOdds()
        {
            return await _context.MatchesOdds.ToListAsync();
        }

        public async Task<IEnumerable<MatchOdds>> RetrieveMatchOddsForMatch(int matchID)
        {
            return await _context.MatchesOdds.Where(x => x.MatchId == matchID).ToListAsync();
        }

        public async Task<bool> UpdateMatch(int matchID, Match match)
        {
            try 
            {
                var curMatch = await RetrieveMatch(matchID);

                if (curMatch == null || match.ID != matchID) return false;

                curMatch.Description = match.Description;
                curMatch.MatchDate = match.MatchDate ?? curMatch.MatchDate;
                curMatch.MatchTime = match.MatchTime ?? curMatch.MatchTime;
                curMatch.TeamA = string.IsNullOrWhiteSpace(match.TeamA) ? curMatch.TeamA : match.TeamA;
                curMatch.TeamB = string.IsNullOrWhiteSpace(match.TeamB) ? curMatch.TeamB : match.TeamB;
                curMatch.Sport = (int)match.Sport != 1 && (int)match.Sport != 2 ? curMatch.Sport : match.Sport; 

                _context.Matches.Update(curMatch);

                return await SaveAllAsync();
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        public async Task<bool> UpdateMatchOdd(int matchOddsID, MatchOdds matchOdds)
        {
            try 
            {
                var curMatchOdd = await RetrieveMatchOdd(matchOddsID);

                if (curMatchOdd == null || matchOdds.ID != matchOddsID) return false;

                curMatchOdd.Odd = matchOdds.Odd;
                curMatchOdd.Specifier = matchOdds.Specifier;

                _context.MatchesOdds.Update(curMatchOdd);

                return await SaveAllAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        private async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
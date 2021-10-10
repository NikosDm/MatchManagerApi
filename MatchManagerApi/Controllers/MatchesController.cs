using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MatchManagerApi.Entities;
using MatchManagerApi.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MatchManagerApi.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class MatchesController : ControllerBase
    {
        private readonly IMatch _matchService;

        public MatchesController(IMatch matchService) 
        {
            _matchService = matchService; 
        }

        #region MATCH CRUD Operations

        /// <summary>
        /// Returns all the available matches
        /// </summary>
        /// <returns>A list of matches</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatches()
        {
            return Ok(await _matchService.RetrieveAllMatches());
        }

        /// <summary>
        /// Returns a match
        /// </summary>
        /// <returns>A match</returns>
        /// <response code="200">Returns the requested match</response>
        /// <response code="404">In case the match does not exist</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Match>> GetMatch(int id)
        {
            var match = await _matchService.RetrieveMatch(id);

            if (match == null) return NotFound("Requested match does not exist");

            return match;
        }

        /// <summary>
        /// Adds a new match
        /// </summary>
        /// <param name="match">Match data</param>
        /// <returns>Response code</returns>
        /// <remarks>
        ///
        /// Sample request data: 
        /// {   
        ///     "description": "Real Madrid Barcelona Basketball Match", 
        ///     "matchDate" : "2021-10-01", 
        ///     "matchTime": "21:30:00", 
        ///     "teamA": "Real Madrid",
        ///     "teamB": "Barcelona",
        ///     "sport": 2 
        /// }
        ///
        /// </remarks>
        /// <response code="200">Returns success message</response>
        /// <response code="400">In case something went wrong during insertion</response>
        [HttpPost("add-new-match")]
        public async Task<ActionResult> AddNewMatch(Match match)
        {
            try 
            {
                if (await _matchService.AddNewMatch(match)) return Ok("Match added successfully");

                return BadRequest("Error during adding new match");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update an existing match
        /// </summary>
        /// <returns>Response code</returns>
        /// <remarks>
        ///
        /// Sample request data: 
        /// {   
        ///     "id": 2,
        ///     "description": "Real Madrid Barcelona Basketball Match", 
        ///     "matchDate" : "2021-10-01", 
        ///     "matchTime": "21:30:00", 
        ///     "teamA": "Real Madrid",
        ///     "teamB": "Barcelona",
        ///     "sport": 2 
        /// }
        ///
        /// </remarks>
        /// <response code="200">Returns success message</response>
        /// <response code="400">In case something went wrong during insertion</response>
        [HttpPut("update-match/{id}")]
        public async Task<ActionResult> UpdateMatch(int id, Match match)
        {
            try 
            {
                if (await _matchService.UpdateMatch(id, match)) return Ok("Match was updated");

                return BadRequest("Error during updating match");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        
        /// <summary>
        /// Deletes an existing match
        /// </summary>
        /// <returns>Response code</returns>
        /// <response code="200">Returns success message</response>
        /// <response code="400">In case something went wrong during delete</response>
        [HttpDelete("delete-match/{id}")]
        public async Task<ActionResult> DeleteMatch(int id)
        {
            try 
            {
                if (await _matchService.DeleteMatch(id)) return Ok("Match deleted successfully");
            
                return BadRequest(string.Concat("Error during deleting match with id = ", id));
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion MATCH CRUD Operations

        #region MATCH ODD CRUD Operations

        /// <summary>
        /// Returns all the available odds of all matches
        /// </summary>
        /// <returns>A list of all odds of all matches</returns>
        [HttpGet("matches-odds")]
        public async Task<ActionResult<IEnumerable<MatchOdds>>> GetMatchesOdds()
        {
            return Ok(await _matchService.RetrieveMatchOdds());
        }

        /// <summary>
        /// Returns a match odd
        /// </summary>
        /// <returns>A match</returns>
        /// <response code="200">Returns the requested match odd</response>
        /// <response code="404">In case the match odd does not exist</response>
        [HttpGet("matches-odds/{id}")]
        public async Task<ActionResult<MatchOdds>> GetMatchOdd(int id)
        {
            var matchOdd = await _matchService.RetrieveMatchOdd(id);

            if (matchOdd == null) return NotFound("Requested match odd does not exist");

            return matchOdd;
        }

        /// <summary>
        /// Returns the match odds of a match
        /// </summary>
        /// <returns>A match</returns>
        /// <response code="200">Returns the match odds of a match</response>
        /// <response code="404">In case the match does not exist</response>
        [HttpGet("{id}/match-odds")]
        public async Task<ActionResult<IEnumerable<MatchOdds>>> GetMatchOdds(int id)
        {
            var matchOdds = await _matchService.RetrieveMatchOddsForMatch(id);

            if (matchOdds == null) return NotFound(string.Concat("Requested match odds for match with id = ", id, " do not exist"));

            return Ok(matchOdds);
        }

        /// <summary>
        /// Adds a new match odd
        /// </summary>
        /// <param name="matchOdd">Match Odd data</param>
        /// <returns>Response code</returns>
        /// <remarks>
        ///
        /// Sample request data: 
        /// {   
        ///     "matchId": 1, 
        ///     "specifier" : "1", 
        ///     "odd": 1.25
        /// }
        ///
        /// </remarks>
        /// <response code="200">Returns the requested match</response>
        /// <response code="400">In case something went wrong during insertion</response>
        [HttpPost("add-new-match-odd")]
        public async Task<ActionResult> AddNewMatchOdd(MatchOdds matchOdd)
        {
            try 
            {
                if (await _matchService.AddNewMatchOdd(matchOdd)) return Ok("Match odd added successfully");

                return BadRequest("Error during adding new match odd");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update an existing match odd
        /// </summary>
        /// <returns>Response code</returns>
        /// <remarks>
        ///
        /// Sample request data: 
        /// 
        /// URI Parameter: 1 (Match Odd Id)
        ///
        /// {   
        ///     "id": 1
        ///     "matchId": 1, 
        ///     "specifier" : "1", 
        ///     "odd": 1.25
        /// }
        ///
        /// </remarks>
        /// <response code="200">Returns success message</response>
        /// <response code="400">In case something went wrong during update</response>
        [HttpPut("update-match-odd/{id}")]
        public async Task<ActionResult> UpdateMatchOdd(int id, MatchOdds matchOdd)
        {
            try 
            { 
                if (await _matchService.UpdateMatchOdd(id, matchOdd)) return Ok("Match odd updated successfully");

                return BadRequest("Error during updating match");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        
        /// <summary>
        /// Deletes an existing match odd
        /// </summary>
        /// <returns>Response code</returns>
        /// <response code="200">Returns success message</response>
        /// <response code="400">In case something went wrong during delete</response>
        [HttpDelete("delete-match-odd/{id}")]
        public async Task<ActionResult> DeleteMatchOdd(int id)
        {
            try 
            {
                if (await _matchService.DeleteMatchOdd(id)) return Ok("Match odd deleted successfully");

                return BadRequest(string.Concat("Error during deleting match odd with id = ", id));
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion MATCH ODD CRUD Operations
    }
}
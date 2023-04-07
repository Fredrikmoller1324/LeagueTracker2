using LeagueTracker2.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;

namespace LeagueTracker2.Controllers
{
    public class SummonerController : ControllerBase
    {
        private readonly RiotApiService _riotApiService;

        public SummonerController(RiotApiService riotApiService)
        {
            _riotApiService = riotApiService;
        }

        

        [HttpGet("summonerName")]
        public async Task<IActionResult> GetPuuid(string summonerName)
        {
            // Call the service to get the PUUID
            try
            {
                string puuId = await _riotApiService.GetPuuidBySummonerName(summonerName);
               
                return Ok(puuId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("summonerName")]
        public async Task<IActionResult> GetMatchHistory(string puuId)
        {
            // Call the service to get the PUUID
            try
            {
                var matchIds = await _riotApiService.GetMatchIdsByPuuid(puuId);

                if (matchIds.IsNullOrEmpty())
                {
                    throw new Exception("No matches found");
                }

                return Ok(matchIds);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

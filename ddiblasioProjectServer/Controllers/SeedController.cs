using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using champsModel;
using CsvHelper.Configuration;
using System.Globalization;
using CsvHelper;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.Metrics;
using champsProjectServer.Dtos;

namespace champsProjectServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController(ChampsDBContext context, IHostEnvironment environment,
        UserManager<ChampsUser> userManager) : ControllerBase
    {
        private readonly string _game_pathName = Path.Combine(environment.ContentRootPath, "Data/champsgames.csv");
        private readonly string _tournaments_pathName = Path.Combine(environment.ContentRootPath, "Data/champstournaments.csv");
        private readonly string _teams_pathName = Path.Combine(environment.ContentRootPath, "Data/champsteams.csv");
        private readonly string _users_pathName = Path.Combine(environment.ContentRootPath, "Data/champsusers.csv");

        [HttpPost("ChampsUsers")]
        public async Task ImportUsersAsync()
        {
            ChampsUser user = new()
            {
                UserName = "user",
                Email = "user@email.com",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult x = await userManager.CreateAsync(user, "Passw0rd!");

            int y = await context.SaveChangesAsync();

        }


        [HttpPost("Games")]
        public async Task<ActionResult> ImportGamesAsync()
        {
            try
            {
                Dictionary<string, Game> itemsByName = context.Games
                    .AsNoTracking().ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

                CsvConfiguration config = new(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    HeaderValidated = null
                };

                // Update to corresponding _pathName!
                using StreamReader reader = new(_game_pathName);
                using CsvReader csv = new(reader, config);

                List<GamesDTO> records = csv.GetRecords<GamesDTO>().ToList();
                foreach (GamesDTO record in records)
                {
                    // Make sure no duplicates of names
                    if (itemsByName.ContainsKey(record.name))
                    {
                        continue;
                    }

                    // Add items here (See Corresponding stores item for reference
                    Game newItem = new()
                    {
                        Name = record.name,
                        Genre = record.genre,
                        TeamSize = record.teamsize
                    };
                    await context.Games.AddAsync(newItem);
                    itemsByName.Add(record.name, newItem);
                }

                await context.SaveChangesAsync();

                return new JsonResult(itemsByName.Count);

            }
            catch (Exception ex)
            {
                // Return the error to the client (or log it)
                return StatusCode(500, new { message = ex.Message, stackTrace = ex.StackTrace });
            }
        }

        [HttpPost("PlayerUsers")]
        public async Task<ActionResult> ImportPlayerUsersAsync()
        {
            Dictionary<string, User> itemsByName = context.Users
                .AsNoTracking().ToDictionary(x => x.Username, StringComparer.OrdinalIgnoreCase);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };

            // Update to corresponding _pathName!
            using StreamReader reader = new(_users_pathName);
            using CsvReader csv = new(reader, config);

            List<UsersDTO> records = csv.GetRecords<UsersDTO>().ToList();
            foreach (UsersDTO record in records)
            {
                // Make sure no duplicates of names
                // This one specifically uses username
                if (itemsByName.ContainsKey(record.username))
                {
                    continue;
                }

                // Add items here (See Corresponding stores item for reference
                User newItem = new()
                {
                    Username = record.username,
                    Password = record.password,
                    Name = record.name,
                    Region = record.region,
                    TeamId = record.teamid,
                    MatchPlays = record.matchplays,
                    MatchWins = record.matchwins,
                    TournamentPlays = record.tournamentplays,
                    TournamentWins = record.tournamentwins
                };
                await context.Users.AddAsync(newItem);
                itemsByName.Add(record.username, newItem);
            }

            await context.SaveChangesAsync();

            return new JsonResult(itemsByName.Count);
        }

        [HttpPost("DeletePlayerUsers")]
        public async Task<ActionResult> DeletePlayerUsersAsync()
        {
            try
            {
                // Load existing users into a dictionary for fast lookup
                Dictionary<string, User> itemsByUsername = context.Users
                    .ToDictionary(x => x.Username, StringComparer.OrdinalIgnoreCase);

                CsvConfiguration config = new(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    HeaderValidated = null
                };

                using StreamReader reader = new(_users_pathName);
                using CsvReader csv = new(reader, config);

                List<UsersDTO> records = csv.GetRecords<UsersDTO>().ToList();

                int deleteCount = 0;

                foreach (UsersDTO record in records)
                {
                    // Check if the user exists in DB
                    if (itemsByUsername.TryGetValue(record.username, out var existingUser))
                    {
                        context.Users.Remove(existingUser);
                        deleteCount++;
                    }
                }

                await context.SaveChangesAsync();

                return new JsonResult(new { deleted = deleteCount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, stackTrace = ex.StackTrace });
            }
        }

        [HttpPost("Teams")]
        public async Task<ActionResult> ImportTeamsAsync()
        {
            Dictionary<string, Team> itemsByName = context.Teams
                .AsNoTracking().ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };

            // Update to corresponding _pathName!
            using StreamReader reader = new(_teams_pathName);
            using CsvReader csv = new(reader, config);

            List<TeamsDTO> records = csv.GetRecords<TeamsDTO>().ToList();
            foreach (TeamsDTO record in records)
            {
                // Make sure no duplicates of names
                if (itemsByName.ContainsKey(record.name))
                {
                    continue;
                }

                // Add items here (See Corresponding stores item for reference
                Team newItem = new()
                {
                    Name = record.name,
                    Region = record.region,
                    MaxMembers = record.maxmembers,
                    TournamentsEntered = record.tournamentsentered,
                    TournamentWins = record.tournamentwins
                };
                await context.Teams.AddAsync(newItem);
                itemsByName.Add(record.name, newItem);
            }

            await context.SaveChangesAsync();

            return new JsonResult(itemsByName.Count);
        }

        [HttpPost("Tournaments")]
        public async Task<ActionResult> ImportTournamentsAsync()
        {
            Dictionary<string, Tournament> itemsByName = context.Tournaments
                .AsNoTracking().ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };

            // Update to corresponding _pathName!
            using StreamReader reader = new(_tournaments_pathName);
            using CsvReader csv = new(reader, config);

            List<TournamentDTO> records = csv.GetRecords<TournamentDTO>().ToList();
            foreach (TournamentDTO record in records)
            {
                // Make sure no duplicates of names
                if (itemsByName.ContainsKey(record.name))
                {
                    continue;
                }

                // Add items here (See Corresponding stores item for reference
                Tournament newItem = new()
                {
                    Name = record.name,
                    ParticipatingTeams = record.participatingteams,
                    MaxTeams = record.maxteams,
                    GameId = record.gameid,
                    PrizePool = record.prizepool,
                    StartTime = record.starttime,
                    EndTime = record.endtime,
                };
                await context.Tournaments.AddAsync(newItem);
                itemsByName.Add(record.name, newItem);
            }

            await context.SaveChangesAsync();

            return new JsonResult(itemsByName.Count);
        }
    }
}
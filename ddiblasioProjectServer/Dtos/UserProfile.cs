using champsModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace champsProjectServer.Dtos
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Region { get; set; } = null!;
        public int MatchPlays { get; set; }
        public int MatchWins { get; set; }
        public int TournamentPlays { get; set; }
        public int TournamentWins { get; set; }
    }
}
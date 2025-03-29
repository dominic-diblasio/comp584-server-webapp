using champsModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace champsProjectServer.Dtos
{
    public class TeamProfile
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Region { get; set; } = null!;
        public int MaxMembers { get; set; }
        public int TournamentsEntered { get; set; }
        public int TournamentWins { get; set; }
    }
}
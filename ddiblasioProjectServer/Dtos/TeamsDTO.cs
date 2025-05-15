using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace champsProjectServer.Dtos
{
    public class TeamsDTO
    {
        public string name { get; set; } = null!;
        public string region { get; set; } = null!;
        public int maxmembers { get; set; }
        public int tournamentsentered { get; set; }
        public int tournamentwins { get; set; }
    }
}